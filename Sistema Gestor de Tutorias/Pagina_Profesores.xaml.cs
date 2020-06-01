using Sistema_Gestor_de_Tutorias.Database_Assets;
using Sistema_Gestor_de_Tutorias.Modelos;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pagina_Profesores : Page
    {
        private ObservableCollection<ProfesoresItem> ProfesoresItems;
        private NavigationView navigationView;
        public Pagina_Profesores()
        {
            this.InitializeComponent();
            ProfesoresItems = new ObservableCollection<ProfesoresItem>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProfesoresFactory.getProfesores("Profesores", ProfesoresItems);
        }

        protected override async void OnNavigatedTo (NavigationEventArgs e)
        {
            this.navigationView = e.Parameter as NavigationView;
            cmbbx_Departamento.ItemsSource = await DBAssets.getStringsAsync((App.Current as App).ConnectionString, "SELECT DISTINCT departamento from Profesores;");
        }

        private void AgregarProfesoresPopup_LayoutUpdated(object sender, object e)
        {
            if (relativeChild.ActualWidth == 0 && relativeChild.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.AgregarProfesoresPopup.HorizontalOffset;
            double ActualVerticalOffset = this.AgregarProfesoresPopup.VerticalOffset;

            relativeChild.Height = (int)(Window.Current.Bounds.Height / 2);

            double NewHorizontalOffset;
            if (navigationView.IsPaneOpen)
                NewHorizontalOffset = (int)((Window.Current.Bounds.Width - relativeChild.ActualWidth) / 2) - 250;
            else
                NewHorizontalOffset = (int)((Window.Current.Bounds.Width - relativeChild.ActualWidth) / 2) - 30;

            double NewVerticalOffset = (int)((Window.Current.Bounds.Height - relativeChild.ActualHeight) / 2) - 30;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.AgregarProfesoresPopup.HorizontalOffset = NewHorizontalOffset;
                this.AgregarProfesoresPopup.VerticalOffset = NewVerticalOffset;
            }
        }

        private async void btn_Agregar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Profesores profesor = new Profesores();
            if (txtbx_provincia.Text == "" || txtbx_codigo_postal.Text == "")
                await DBAssets.setProfesoresAsync((App.Current as App).ConnectionString, profesor, null, 0);
            else {
                Provincias provincia = new Provincias() { provincia = txtbx_provincia.Text, cod_postal = int.Parse(txtbx_codigo_postal.Text)};
                if (await DBAssets.setProfesoresAsync((App.Current as App).ConnectionString, profesor, provincia, 1) >= 0)
                    ProfesoresItems.Add(new ProfesoresItem() { 
                    });
            }
        }

        private void ProfesoresGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            ProfesoresItem item = (e.ClickedItem) as ProfesoresItem;
            if (item.Categoria == "Agregar")
            {
                btn_Agregar.IsEnabled = true;
                btn_Agregar.Visibility = Visibility.Visible;
                btn_Actualizar.IsEnabled = false;
                btn_Actualizar.Visibility = Visibility.Collapsed;
                btn_Eliminar.IsEnabled = false;
                btn_Eliminar.Visibility = Visibility.Collapsed;
            }
            else
            {
                btn_Agregar.IsEnabled = false;
                btn_Agregar.Visibility = Visibility.Collapsed;
                btn_Actualizar.IsEnabled = true;
                btn_Actualizar.Visibility = Visibility.Visible;
                btn_Eliminar.IsEnabled = true;
                btn_Eliminar.Visibility = Visibility.Visible;
                txtbx_Nombre.Text = item.profesor.nombre;
                txtbx_Apellidos.Text = item.profesor.apellidos;
                txtbx_Departamento.Text = item.profesor.departamento;
            }
            AgregarProfesoresPopup.IsOpen = true;
        }

        private void btn_Actualizar_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void btn_Eliminar_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void chkbx_Departamento_Unchecked(object sender, RoutedEventArgs e)
        {
            txtbx_Departamento.IsEnabled = false;
            txtbx_Departamento.Visibility = Visibility.Collapsed;
            cmbbx_Departamento.IsEnabled = true;
            cmbbx_Departamento.Visibility = Visibility.Visible;
        }

        private void chkbx_Departamento_Checked(object sender, RoutedEventArgs e)
        {
            txtbx_Departamento.IsEnabled = true;
            txtbx_Departamento.Visibility = Visibility.Visible;
            cmbbx_Departamento.IsEnabled = false;
            cmbbx_Departamento.Visibility = Visibility.Collapsed;
        }
    }
}
