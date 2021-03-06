﻿using NTMiner.Vms;
using System;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace NTMiner.Views.Ucs {
    public partial class Console : UserControl {
        private MinerProfileViewModel Vm {
            get {
                return (MinerProfileViewModel)this.DataContext;
            }
        }

        public Console() {
            InitializeComponent();
            ResourceDictionarySet.Instance.FillResourceDic(this, this.Resources);
            Global.WriteLineMethod = WriteLine;
        }

        private void InnerWrite(string text, ConsoleColor foreground) {
            InlineCollection list = this.ConsoleParagraph.Inlines;
            // 满1000行删除500行
            if (list.Count > 1000) {
                int delLines = 500;
                while (delLines-- > 0) {
                    ((IList)list).RemoveAt(0);
                }
            }
            Run run = new Run(text) {
                Foreground = new SolidColorBrush(foreground.ToMediaColor())
            };
            list.Add(run);

            if (ChkbIsConsoleAutoScrollToEnd.IsChecked.HasValue && ChkbIsConsoleAutoScrollToEnd.IsChecked.Value) {
                this.RichTextBox.ScrollToEnd();
            }
        }

        public void WriteLine(string text, ConsoleColor foreground) {
            Dispatcher.Invoke((Action)(() => {
                if (this.ConsoleParagraph.Inlines.Count > 0) {
                    text = "\n" + text;
                }
                InnerWrite(text, foreground);
            }));
        }
    }
}
