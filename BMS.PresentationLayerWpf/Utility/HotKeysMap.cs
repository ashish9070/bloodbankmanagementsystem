using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BMS.PresentationLayerWpf.Utility
{
    public class HotKeysMap
    {
        public Key Key { get; set; }
        public ModifierKeys Modifiers { get; set; }
        public ExecutedRoutedEventHandler Events { get; set; }

    }
}
