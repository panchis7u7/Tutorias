using Sistema_Gestor_de_Tutorias.Database_Assets;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Popups;
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

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.main_page_obj = e.Parameter as Page;
            string[] Query = { "SELECT nombre FROM CoordinadoresTutorias",
                               "SELECT nombre FROM CoordinadoresTutoriasInstitucionales",
                               "SELECT nombre FROM JefesDepartamentos WHERE id_jefe = 1",
                               "SELECT nombre FROM JefesDepartamentos WHERE id_jefe = 2"};
            txtbx_jefeTut.Text = await DBAssets.getStringAsync((App.Current as App).ConnectionString, Query[0]);
            txtbx_jefeTutIns.Text = await DBAssets.getStringAsync((App.Current as App).ConnectionString, Query[1]);
            txtbx_jefeDep.Text = await DBAssets.getStringAsync((App.Current as App).ConnectionString, Query[2]);  
            txtbx_jefeDesAcadem.Text = await DBAssets.getStringAsync((App.Current as App).ConnectionString, Query[3]);  
        }

        private async void btn_guardar_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            try
            {
                if (await DBAssets.setActualizarJefeAsync((App.Current as App).ConnectionString, txtbx_jefeTut.Text, txtbx_jefeTutIns.Text, txtbx_jefeDep.Text) >= 0)
                    await new MessageDialog("Actualizacion Exitosa!").ShowAsync();
            }
            catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
        }

        private void ts_habilitar_Toggled(object sender, RoutedEventArgs e)
        {
            if (this.ts_habilitar.IsOn)
            {
                txtbx_jefeTut.IsReadOnly = false;
                txtbx_jefeTutIns.IsReadOnly = false;
                txtbx_jefeDep.IsReadOnly = false;
                btn_guardar.IsEnabled = true;
            } else
            {
                txtbx_jefeTut.IsReadOnly = true;
                txtbx_jefeTutIns.IsReadOnly = true;
                txtbx_jefeDep.IsReadOnly = true;
                btn_guardar.IsEnabled = false;
            }
        }
    }
}
