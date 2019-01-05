using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BMS.PresentationLayerWpf.Utility
{
    public class MessageHandler
    {
        public static void ShowErrorMessage(string message, string title = "")
        {
            title = string.IsNullOrEmpty(title) ? ApplicationInfo.APPFULLNAME : title;
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowInfoMessage(string message, string title = "")
        {
            title = string.IsNullOrEmpty(title) ? ApplicationInfo.APPFULLNAME : title;
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
