﻿using NTMiner.ServiceContracts.DataObjects;
using System;

namespace NTMiner.Data {
    public interface IMineProfileManager {
        MinerProfileData GetMinerProfile(Guid workId);
        void SetMinerProfileProperty(Guid workId, string propertyName, object value);

        CoinProfileData GetCoinProfile(Guid workId, Guid coinId);
        void SetCoinProfileProperty(Guid workId, Guid coinId, string propertyName, object value);

        CoinKernelProfileData GetCoinKernelProfile(Guid workId, Guid coinKernelId);
        void SetCoinKernelProfileProperty(Guid workId, Guid coinKernelId, string propertyName, object value);
    }
}
