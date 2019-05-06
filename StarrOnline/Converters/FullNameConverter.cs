﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using StarrOnline.Common;
using StarrOnline.Models;

namespace StarrOnline.Converters
{
    class FullNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Person)
            {
                return (value as Person).FullName;
            }
            else
            {
                return App.GetString("NotAvailable");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
