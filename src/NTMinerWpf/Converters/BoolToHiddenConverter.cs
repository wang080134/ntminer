﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NTMiner.Converters {

    public class BoolToHiddenConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is bool b && b) {
                return ConvertFun(Visibility.Visible, parameter);
            }
            return ConvertFun(Visibility.Hidden, parameter);
        }

        public object ConvertFun(Visibility visibility, object parameter) {
            if (parameter is string p) {
                if (visibility == Visibility.Visible) {
                    return Visibility.Hidden;
                }
                return Visibility.Visible;
            }
            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value != null && value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
