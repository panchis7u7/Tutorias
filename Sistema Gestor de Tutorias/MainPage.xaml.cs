using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Pagina_Configuracion config_page;
        public MainPage()
        {
            this.InitializeComponent();
            config_page = new Pagina_Configuracion(ref mainPageRef, ref navMenu);
        }

        private void navMenu_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {

        }

        private void navMenu_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                main_frame.Content = config_page;
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;
                switch (item.Tag.ToString())
                {
                    case "1":
                        main_frame.Navigate(typeof(Pagina_Configuracion));
                        break;
                    case "2":
                        main_frame.Navigate(typeof(Pagina_Configuracion));
                        break;
                    case "3":
                        main_frame.Navigate(typeof(Pagina_Configuracion));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}