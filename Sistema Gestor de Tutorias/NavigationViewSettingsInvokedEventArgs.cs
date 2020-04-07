using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Gestor_de_Tutorias
{
    public class NavigationViewSettingsInvokedEventArgs
    {
        public bool selected { get; private set; }
        public NavigationViewSettingsInvokedEventArgs()
        {
            selected = true;
        }
    }
}
