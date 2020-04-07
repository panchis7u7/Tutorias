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
        public event EventHandler<NavigationViewSettingsInvokedEventArgs> settings_invoked;
        public MainPage()
        {
            settings_invoked += btn_settings_Invoked;
            this.InitializeComponent();
            config_page = new Pagina_Configuracion(ref mainPageRef, ref navMenu);
        }

        private void navMenu_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                settings_invoked(this, new NavigationViewSettingsInvokedEventArgs());
            }
        }

        private void btn_settings_Invoked(object sender, NavigationViewSettingsInvokedEventArgs args)
        {
            main_frame.Content = config_page;
        }

        private void navMenu_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void navMenu_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {

        }

        private void btn_home_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void btn_Consultas_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void btn_formatos_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
