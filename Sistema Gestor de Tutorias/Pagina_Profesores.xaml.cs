using Sistema_Gestor_de_Tutorias.Database_Assets;
using Sistema_Gestor_de_Tutorias.Modelos;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
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
        private ProfesoresItem profesor_seleccionado;
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
            InfoProfesores profesor = new InfoProfesores();
            profesor.nombre = txtbx_Nombre.Text;
            profesor.apellidos = txtbx_Apellidos.Text;
            profesor.correo = txtbx_correo.Text;
            profesor.departamento = txtbx_Departamento.Text;
            profesor.cod_postal = int.Parse(txtbx_codigo_postal.Text);
            if (await DBAssets.setProfesoresAsync((App.Current as App).ConnectionString, profesor) >= 0)
                ProfesoresItems.Add(new ProfesoresItem() {
                    Id = profesor.id_profesor,
                    Categoria = "Tutores",
                    HeadLine = profesor.nombre + " " + profesor.apellidos,
                    Subhead = profesor.departamento,
                    profesor = profesor,
                    Imagen = "Assets/Usuario.png"
                });
        }

        private async void ProfesoresGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            ProfesoresItem item = (e.ClickedItem) as ProfesoresItem;
            profesor_seleccionado = item;
            MemoryStream stream;
            BitmapImage bitmap;
            if (item.Categoria == "Agregar")
            {
                btn_Agregar.IsEnabled = true;
                btn_Agregar.Visibility = Visibility.Visible;
                btn_Actualizar.IsEnabled = false;
                btn_Actualizar.Visibility = Visibility.Collapsed;
                btn_Eliminar.IsEnabled = false;
                btn_Eliminar.Visibility = Visibility.Collapsed;
                btn_AgregarImagen.IsEnabled = false;
                btn_AgregarImagen.Visibility = Visibility.Collapsed;
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
                txtbx_codigo_postal.Text = item.profesor.cod_postal.ToString();
                txtbx_provincia.Text = item.profesor.provincia;
                txtbx_correo.Text = item.profesor.correo;
                if (item.profesor.imagen != null)
                {
                    stream = new MemoryStream();
                    await item.profesor.imagen.CopyToAsync(stream);
                    stream.Position = 0;
                    bitmap = new BitmapImage();
                    await bitmap.SetSourceAsync(stream.AsRandomAccessStream());
                    img_profesor.Source = bitmap;
                }
            }
            AgregarProfesoresPopup.IsOpen = true;
        }

        private async void btn_Actualizar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            profesor_seleccionado.profesor.nombre = txtbx_Nombre.Text;
            profesor_seleccionado.profesor.apellidos = txtbx_Apellidos.Text;
            if (cmbbx_Departamento.IsEnabled)
                profesor_seleccionado.profesor.departamento = cmbbx_Departamento.SelectedItem.ToString();
            else
                profesor_seleccionado.profesor.departamento = txtbx_Departamento.Text;
            profesor_seleccionado.profesor.correo = txtbx_correo.Text;
            profesor_seleccionado.profesor.provincia = txtbx_provincia.Text;
            await DBAssets.setActualizarProfesorAsync((App.Current as App).ConnectionString, profesor_seleccionado.profesor);
        }

        private async void btn_Eliminar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await DBAssets.setBajaProfesorAsync((App.Current as App).ConnectionString, profesor_seleccionado.profesor);
            foreach(var profesor in ProfesoresItems)
            {
                if(profesor.profesor.id_profesor == profesor_seleccionado.profesor.id_profesor)
                {
                    ProfesoresItems.Remove(profesor);
                }
            }
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

        private async void btn_AgregarImagen_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                var image = new BitmapImage();
                image.SetSource(stream);
                img_profesor.Source = image;
            }
        }
    }
}
