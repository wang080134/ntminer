﻿using NTMiner.Vms;
using System.Windows.Controls;

namespace NTMiner.Views.Ucs {
    public partial class SysDicItemEdit : UserControl {
        public static void ShowEditWindow(SysDicItemViewModel source) {
            ContainerWindow.ShowWindow(new ContainerWindowViewModel {
                IsDialogWindow = true,
                CloseVisible = System.Windows.Visibility.Visible,
                IconName = "Icon_SysDic"
            }, ucFactory: (window) =>
            {
                SysDicItemViewModel vm = new SysDicItemViewModel(source);
                vm.CloseWindow = () => window.Close();
                return new SysDicItemEdit(vm);
            }, fixedSize: true);
        }

        private SysDicItemViewModel Vm {
            get {
                return (SysDicItemViewModel)this.DataContext;
            }
        }

        public SysDicItemEdit(SysDicItemViewModel vm) {
            this.DataContext = vm;
            InitializeComponent();
            ResourceDictionarySet.Instance.FillResourceDic(this, this.Resources);
        }
    }
}
