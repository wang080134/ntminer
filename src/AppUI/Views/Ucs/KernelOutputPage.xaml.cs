﻿using NTMiner.Vms;
using System.Windows.Controls;
using System.Windows.Input;

namespace NTMiner.Views.Ucs {
    public partial class KernelOutputPage : UserControl {
        public static void ShowWindow() {
            ContainerWindow.ShowWindow(new ContainerWindowViewModel {
                IconName = "Icon_KernelOutput",
                CloseVisible = System.Windows.Visibility.Visible,
                FooterVisible = System.Windows.Visibility.Collapsed,
                Width = 960,
                Height = 720
            }, 
            ucFactory: (window) => new KernelOutputPage());
        }

        private KernelOutputPageViewModel Vm {
            get {
                return (KernelOutputPageViewModel)this.DataContext;
            }
        }

        public KernelOutputPage() {
            InitializeComponent();
            ResourceDictionarySet.Instance.FillResourceDic(this, this.Resources);
        }

        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            DataGrid dg = (DataGrid)sender;
            if (Vm.CurrentKernelOutputVm != null) {
                Vm.CurrentKernelOutputVm.Edit.Execute(null);
            }
        }

        private void KernelOutputFilterDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            DataGrid dg = (DataGrid)sender;
            if (dg.SelectedItem != null) {
                KernelOutputFilterViewModel kernelOutputFilterVm = (KernelOutputFilterViewModel)dg.SelectedItem;
                kernelOutputFilterVm.Edit.Execute(null);
            }
        }

        private void KernelOutputTranslaterDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            DataGrid dg = (DataGrid)sender;
            if (dg.SelectedItem != null) {
                KernelOutputTranslaterViewModel kernelOutputTranslaterVm = (KernelOutputTranslaterViewModel)dg.SelectedItem;
                kernelOutputTranslaterVm.Edit.Execute(null);
            }
        }
    }
}
