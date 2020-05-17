using System.ComponentModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>}
    /// 
    public sealed partial class Pagina_Configuracion : Page
    {
        private string JefeTutoria;

        public string jefe_tutoria
        {
            get { return JefeTutoria; }
            set { JefeTutoria = value; }
        }

        private string JefeDepartamento;

        public string jefe_departamento
        {
            get { return JefeDepartamento; }
            set { JefeDepartamento = value; }
        }

        public Page main_page_obj;
        //public NavigationView nav_view_obj;
        public Pagina_Configuracion()
        {
            this.InitializeComponent();
        }

        public Pagina_Configuracion(ref Page main_page_obj)
        {
            //this.nav_view_obj = nav_view_obj;
            this.main_page_obj = main_page_obj;
            this.InitializeComponent();
        }

        private void Modo_Oscuro_Toggled(object sender, RoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.XamlCompositionBrushBase"))
            {
                if (this.ts_Modo_Oscuro.IsOn == true)
                {
                    Windows.UI.Xaml.Media.AcrylicBrush acrilicoOscuro = new Windows.UI.Xaml.Media.AcrylicBrush();
                    acrilicoOscuro.BackgroundSource = Windows.UI.Xaml.Media.AcrylicBackgroundSource.HostBackdrop;
                    acrilicoOscuro.TintColor = Colors.Black;
                    acrilicoOscuro.FallbackColor = Colors.DimGray;
                    acrilicoOscuro.TintOpacity = 0.4;
                    main_page_obj.Background = acrilicoOscuro;
                    //nav_view_obj.Background = acrilicoOscuro;
                }
                else
                {
                    Windows.UI.Xaml.Media.AcrylicBrush acrilicoBlanco = new Windows.UI.Xaml.Media.AcrylicBrush();
                    acrilicoBlanco.BackgroundSource = Windows.UI.Xaml.Media.AcrylicBackgroundSource.HostBackdrop;
                    acrilicoBlanco.TintColor = Colors.WhiteSmoke;
                    acrilicoBlanco.FallbackColor = Colors.DimGray;
                    acrilicoBlanco.TintOpacity = 0.4;
                    main_page_obj.Background = acrilicoBlanco;
                    //nav_view_obj.Background = acrilicoBlanco;
                }
            }
            else
            {
                SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(255, 202, 24, 37));
                main_page_obj.Foreground = myBrush;
                main_page_obj.Background = myBrush;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.main_page_obj = e.Parameter as Page;
        }
    }
}
