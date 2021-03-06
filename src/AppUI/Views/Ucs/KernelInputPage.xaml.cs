﻿using NTMiner.Vms;
using System.Windows.Controls;

namespace NTMiner.Views.Ucs {
    public partial class KernelInputPage : UserControl {
        public static void ShowWindow() {
            ContainerWindow.ShowWindow(new ContainerWindowViewModel {
                IconName = "Icon_KernelInput",
                CloseVisible = System.Windows.Visibility.Visible,
                FooterVisible = System.Windows.Visibility.Collapsed,
                Width = 560,
                Height = 520
            }, 
            ucFactory: (window) => new KernelInputPage());
        }

        private KernelInputPageViewModel Vm {
            get {
                return (KernelInputPageViewModel)this.DataContext;
            }
        }

        public KernelInputPage() {
            InitializeComponent();
            ResourceDictionarySet.Instance.FillResourceDic(this, this.Resources);
        }

        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            DataGrid dg = (DataGrid)sender;
            if (dg.SelectedItem != null) {
                ((KernelInputViewModel)dg.SelectedItem).Edit.Execute(null);
            }
        }
    }
}
