﻿using System.ComponentModel;

namespace NTMiner {
    public enum LogEnum {
        [Description("不记录日志")]
        None,
        [Description("打印到控制台")]
        Console,
        [Description("写入日志文件")]
        Log
    }
}
