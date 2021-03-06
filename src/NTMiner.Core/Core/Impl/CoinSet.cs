﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NTMiner.Core.Impl {
    internal class CoinSet : ICoinSet {
        private readonly INTMinerRoot _root;
        private readonly Dictionary<string, CoinData> _dicByCode = new Dictionary<string, CoinData>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<Guid, CoinData> _dicById = new Dictionary<Guid, CoinData>();

        public CoinSet(INTMinerRoot root) {
            _root = root;
            Global.Access<AddCoinCommand>(Guid.Parse("4CF438BB-7B59-4C56-AB8C-D01312848450"), "添加币种", LogEnum.Log, action: message => {
                InitOnece();
                if (message == null || message.Input == null || message.Input.GetId() == Guid.Empty) {
                    throw new ArgumentNullException();
                }
                if (string.IsNullOrEmpty(message.Input.Code)) {
                    throw new ValidationException("coin code can't be null or empty");
                }
                if (_dicById.ContainsKey(message.Input.GetId())) {
                    return;
                }
                if (_dicByCode.ContainsKey(message.Input.Code)) {
                    throw new DuplicateCodeException();
                }
                CoinData entity = new CoinData().Update(message.Input);
                _dicById.Add(entity.Id, entity);
                _dicByCode.Add(entity.Code, entity);
                var repository = NTMinerRoot.CreateServerRepository<CoinData>();
                repository.Add(entity);

                Global.Happened(new CoinAddedEvent(entity));
            });
            Global.Access<UpdateCoinCommand>(Guid.Parse("86EAEA27-7B7C-4A12-8F22-8F1422C6A489"), "更新币种", LogEnum.Log, action: message => {
                InitOnece();
                if (message == null || message.Input == null || message.Input.GetId() == Guid.Empty) {
                    throw new ArgumentNullException();
                }
                if (string.IsNullOrEmpty(message.Input.Code)) {
                    throw new ValidationException("coin code can't be null or empty");
                }
                if (!_dicById.ContainsKey(message.Input.GetId())) {
                    return;
                }
                CoinData entity = _dicById[message.Input.GetId()];
                entity.Update(message.Input);
                var repository = NTMinerRoot.CreateServerRepository<CoinData>();
                repository.Update(entity);

                Global.Happened(new CoinUpdatedEvent(message.Input));
            });
            Global.Access<RemoveCoinCommand>(Guid.Parse("9BB00186-9647-48D1-BF7B-4281A3FF317C"), "移除币种", LogEnum.Log, action: message => {
                InitOnece();
                if (message == null || message.EntityId == Guid.Empty) {
                    throw new ArgumentNullException();
                }
                if (!_dicById.ContainsKey(message.EntityId)) {
                    return;
                }
                CoinData entity = _dicById[message.EntityId];
                Guid[] toRemoves = root.PoolSet.Where(a => a.CoinId == entity.Id).Select(a => a.GetId()).ToArray();
                foreach (var id in toRemoves) {
                    Global.Execute(new RemovePoolCommand(id));
                }
                toRemoves = root.CoinKernelSet.Where(a => a.CoinId == entity.Id).Select(a => a.GetId()).ToArray();
                foreach (var id in toRemoves) {
                    Global.Execute(new RemoveCoinKernelCommand(id));
                }
                toRemoves = root.WalletSet.Where(a => a.CoinId == entity.Id).Select(a => a.GetId()).ToArray();
                foreach (var id in toRemoves) {
                    Global.Execute(new RemoveWalletCommand(id));
                }
                _dicById.Remove(entity.Id);
                if (_dicByCode.ContainsKey(entity.Code)) {
                    _dicByCode.Remove(entity.Code);
                }
                var repository = NTMinerRoot.CreateServerRepository<CoinData>();
                repository.Remove(entity.Id);

                Global.Happened(new CoinRemovedEvent(entity));
            });
            Global.Logger.InfoDebugLine(this.GetType().FullName + "接入总线");
        }

        private bool _isInited = false;
        private object _locker = new object();

        public int Count {
            get {
                InitOnece();
                return _dicById.Count;
            }
        }

        private void InitOnece() {
            if (_isInited) {
                return;
            }
            Init();
        }

        private void Init() {
            lock (_locker) {
                if (!_isInited) {
                    var repository = NTMinerRoot.CreateServerRepository<CoinData>();
                    foreach (var item in repository.GetAll()) {
                        if (!_dicById.ContainsKey(item.GetId())) {
                            _dicById.Add(item.GetId(), item);
                        }
                        if (!_dicByCode.ContainsKey(item.Code)) {
                            _dicByCode.Add(item.Code, item);
                        }
                    }
                    _isInited = true;
                }
            }
        }

        public bool Contains(string coinCode) {
            InitOnece();
            return _dicByCode.ContainsKey(coinCode);
        }

        public bool Contains(Guid coinId) {
            InitOnece();
            return _dicById.ContainsKey(coinId);
        }

        public bool TryGetCoin(string coinCode, out ICoin coin) {
            InitOnece();
            CoinData c;
            var r = _dicByCode.TryGetValue(coinCode, out c);
            coin = c;
            return r;
        }

        public bool TryGetCoin(Guid coinId, out ICoin coin) {
            InitOnece();
            CoinData c;
            var r = _dicById.TryGetValue(coinId, out c);
            coin = c;
            return r;
        }

        public IEnumerator<ICoin> GetEnumerator() {
            InitOnece();
            return _dicById.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            InitOnece();
            return _dicById.Values.GetEnumerator();
        }
    }
}
