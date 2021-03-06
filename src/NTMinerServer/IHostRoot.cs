﻿using NTMiner.Data;
using System;

namespace NTMiner {
    public interface IHostRoot {
        DateTime StartedOn { get; }

        void Start();

        void Stop();

        IUserSet UserSet { get; }
        ICalcConfigSet CalcConfigSet { get; }
        IClientCoinSnapshotSet ClientCoinSnapshotSet { get; }
        IClientSet ClientSet { get; }
        ICoinSnapshotSet CoinSnapshotSet { get; }
        IMineWorkSet MineWorkSet { get; }
        IMinerGroupSet MinerGroupSet { get; }
        IWalletSet WalletSet { get; }
        IMineProfileManager MineProfileManager { get; }
        INTMinerFileSet NTMinerFileSet { get; }
    }
}
