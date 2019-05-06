using Caliburn.Micro;
using System.Collections.Generic;
using System.Windows;
using StarrOnline.Models;

namespace StarrOnline.Utilities
{
    public class MyWindowManager : WindowManager
    {
        protected override Window CreateWindow(object rootModel, bool isDialog, object context, IDictionary<string, object> settings)
        {
            if (settings == null)
                settings = new Dictionary<string, object>();
                //settings.Add("Title", GlobalConfig.AppName);
            return base.CreateWindow(rootModel, isDialog, context, settings);
        }
    }
}
