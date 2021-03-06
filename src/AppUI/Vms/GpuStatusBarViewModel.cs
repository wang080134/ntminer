﻿using System.Linq;
using System.Windows.Media;

namespace NTMiner.Vms {
    public class GpuStatusBarViewModel : ViewModelBase {
        public static readonly GpuStatusBarViewModel Current = new GpuStatusBarViewModel();

        public GpuStatusBarViewModel() {
            this.GpuAllVm = GpuViewModels.Current.FirstOrDefault(a => a.Index == NTMinerRoot.GpuAllId);
        }

        public GpuViewModel GpuAllVm {
            get; set;
        }

        private Geometry _icon;
        public Geometry Icon {
            get {
                if (_icon == null) {
                    string iconName;
                    switch (NTMinerRoot.Current.GpuSet.GpuType) {
                        case Core.Gpus.GpuType.NVIDIA:
                            iconName = "Icon_Nvidia";
                            break;
                        case Core.Gpus.GpuType.AMD:
                            iconName = "Icon_AMD";
                            break;
                        case Core.Gpus.GpuType.Empty:
                            iconName = "Icom_GpuEmpty";
                            break;
                        default:
                            throw new System.InvalidProgramException();
                    }
                    _icon = (Geometry)System.Windows.Application.Current.Resources[iconName];
                }
                return _icon;
            }
        }

        public string IconFill {
            get {
                string iconFill;
                switch (NTMinerRoot.Current.GpuSet.GpuType) {
                    case Core.Gpus.GpuType.NVIDIA:
                        iconFill = "Green";
                        break;
                    case Core.Gpus.GpuType.AMD:
                        iconFill = "Red";
                        break;
                    case Core.Gpus.GpuType.Empty:
                        iconFill = "Gray";
                        break;
                    default:
                        throw new System.InvalidProgramException();
                }
                return iconFill;
            }
        }

        public string GpuSetName {
            get {
                return NTMinerRoot.Current.GpuSet.GpuType.GetDescription();
            }
        }

        public string GpuCountText {
            get {
                return $"({NTMinerRoot.Current.GpuSet.Count} GPU)";
            }
        }
    }
}
