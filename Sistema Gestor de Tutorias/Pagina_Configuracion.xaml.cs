using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pagina_Configuracion : Page
    {
        private Page main_page_obj;
        private NavigationView nav_view_obj;
        private bool toggled = false;

        public Pagina_Configuracion()
        {
            this.InitializeComponent();
        }

        public Pagina_Configuracion(ref Page main_page_obj, ref NavigationView nav_view_obj)
        {
            this.nav_view_obj = nav_view_obj;
            this.main_page_obj = main_page_obj;
            this.InitializeComponent();
        }

        private void Modo_Oscuro_Toggled(object sender, RoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.XamlCompositionBrushBase"))
            {
                if (toggled == false)
                {
                    Windows.UI.Xaml.Media.AcrylicBrush acrilicoOscuro = new Windows.UI.Xaml.Media.AcrylicBrush();
                    acrilicoOscuro.BackgroundSource = Windows.UI.Xaml.Media.AcrylicBackgroundSource.HostBackdrop;
                    acrilicoOscuro.TintColor = Color.FromArgb(0, 0, 0, 0);
                    acrilicoOscuro.FallbackColor = Color.FromArgb(0, 0, 0, 0);
                    acrilicoOscuro.TintOpacity = 0.1;
                    main_page_obj.Background = acrilicoOscuro;
                    nav_view_obj.Background = acrilicoOscuro;
                    toggled = true;
                }
                else
                {
                    Windows.UI.Xaml.Media.AcrylicBrush acrilicoOscuro = new Windows.UI.Xaml.Media.AcrylicBrush();
                    acrilicoOscuro.BackgroundSource = Windows.UI.Xaml.Media.AcrylicBackgroundSource.HostBackdrop;
                    acrilicoOscuro.TintColor = Color.FromArgb(0, 255, 255, 255);
                    acrilicoOscuro.FallbackColor = Color.FromArgb(0, 255, 255, 255);
                    acrilicoOscuro.TintOpacity = 0.1;
                    main_page_obj.Background = acrilicoOscuro;
                    nav_view_obj.Background = acrilicoOscuro;
                    toggled = false;
                }
            }
            else
            {
                SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(255, 202, 24, 37));
                main_page_obj.Foreground = myBrush;
                main_page_obj.Background = myBrush;
            }
        }
    }
}
