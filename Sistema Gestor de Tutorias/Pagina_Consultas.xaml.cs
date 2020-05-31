using System;
using System.Collections.ObjectModel;
using Sistema_Gestor_de_Tutorias.Modelos;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using Sistema_Gestor_de_Tutorias.Database_Assets;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pagina_Consultas : Page
    {
        private int idTutorSeleccionado;
        private InfoAlumnos listItemSeleccionado;
        private InfoAlumnos info_alumnos;
        private ObservableCollection<InfoAlumnos> AlumnosPtr;
        public GruposItem grupo_seleccionado;
        public Pagina_Consultas()
        {
            this.InitializeComponent();
        }

        public async Task<int> GetId (SqlConnection conexion, string Query)
        {
            int id = 0;
            try
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            await reader.ReadAsync();
                            id = reader.GetInt32(0);
                        }
                    }
                }
            return id;
            } catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return 0;
        }

        private void TextBlock_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            tx.Foreground = new SolidColorBrush(Colors.White);
        }

        private void TextBlock_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            tx.Foreground = new SolidColorBrush(Colors.DarkOrange);
        }

        private async void TextBlock_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            InventoryList.ItemsSource = await DBAssets.GetInfoAlumnosAsync((App.Current as App).ConnectionString, grupo_seleccionado.Subhead, "ORDER BY " + tx.Name);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            grupo_seleccionado = (e.Parameter) as GruposItem;
            AlumnosPtr = await DBAssets.GetInfoAlumnosAsync((App.Current as App).ConnectionString, grupo_seleccionado.Subhead, "");
            InventoryList.ItemsSource = AlumnosPtr;
        }

        private void btn_Agregar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            btn_Agregar_Popup.IsEnabled = true;
            btn_Agregar_Popup.Visibility = Visibility.Visible;
            btn_Actualizar.IsEnabled = false;
            btn_Actualizar.Visibility = Visibility.Collapsed;
            btn_Baja.IsEnabled = false;
            btn_Baja.Visibility = Visibility.Collapsed;
            InsertsPopup.IsOpen = true;
        }

        private void InsertsPopup_LayoutUpdated(object sender, object e)
        {
            if (relativeChild.ActualWidth == 0 && relativeChild.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.InsertsPopup.HorizontalOffset;
            double ActualVerticalOffset = this.InsertsPopup.VerticalOffset;

            relativeChild.Height = (int)(Window.Current.Bounds.Height / 2);

            double NewHorizontalOffset = (int) (Window.Current.Bounds.Width - relativeChild.ActualWidth) / 2;
            double NewVerticalOffset = (int) (Window.Current.Bounds.Height - relativeChild.ActualHeight) / 2;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.InsertsPopup.HorizontalOffset = NewHorizontalOffset;
                this.InsertsPopup.VerticalOffset = NewVerticalOffset;
            }
        }
        private async void Alta_Popup(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                info_alumnos = new InfoAlumnos();
                info_alumnos.id_alumno = await GetId((App.Current as App).conexionBD, "SELECT (MAX(id_alumno) + 1) FROM Alumnos");
                info_alumnos.matricula = int.Parse(txtbx_matricula.Text);
                info_alumnos.nombre = txtbx_Nombre.Text;
                info_alumnos.apellidos = txtbx_apellidos.Text;
                info_alumnos.id_provincia = await GetId((App.Current as App).conexionBD, "SELECT (MAX(id_provincia) + 1) FROM Provincias");
                info_alumnos.cod_postal = int.Parse(txtbx_codigo_postal.Text);
                info_alumnos.provincia = txtbx_provincia.Text;
                idTutorSeleccionado = await GetId((App.Current as App).conexionBD, "SELECT DISTINCT id_tutor FROM Tutores " +
                    "INNER JOIN Profesores ON Profesores.id_profesor = Tutores.id_profesor " +
                    "WHERE CONCAT(TRIM(Profesores.nombre), ' ', TRIM(Profesores.apellidos)) LIKE('%" + grupo_seleccionado.Subhead + "%');");
                if (await DBAssets.setAlumnoAsync((App.Current as App).ConnectionString, info_alumnos, idTutorSeleccionado) >= 0)
                    AlumnosPtr.Add(info_alumnos);
            } catch(Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
        }

        private void InventoryList_ItemClick(object sender, ItemClickEventArgs e)
        {
            //await new MessageDialog("Error!").ShowAsync();
            btn_Agregar_Popup.IsEnabled = false;
            btn_Agregar_Popup.Visibility = Visibility.Collapsed;
            btn_Baja.IsEnabled = true;
            btn_Baja.Visibility = Visibility.Visible;
            btn_Actualizar.IsEnabled = true;
            btn_Actualizar.Visibility = Visibility.Visible;
            listItemSeleccionado = e.ClickedItem as InfoAlumnos;
            txtbx_matricula.Text = listItemSeleccionado.matricula + "";
            txtbx_Nombre.Text = listItemSeleccionado.nombre;
            txtbx_apellidos.Text = listItemSeleccionado.apellidos;
            txtbx_codigo_postal.Text = listItemSeleccionado.cod_postal + "";
            txtbx_provincia.Text = listItemSeleccionado.provincia;
            InsertsPopup.IsOpen = true;
        }

        private async void btn_Baja_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (listItemSeleccionado != null) {
                try
                {
                    if (await DBAssets.setBajaAlumnoAsync((App.Current as App).ConnectionString, listItemSeleccionado) >= 0)
                    foreach(var alumno in AlumnosPtr)
                    {
                        if(alumno.id_alumno == listItemSeleccionado.id_alumno)
                        {
                            AlumnosPtr.Remove(alumno);
                            break;
                        }
                    }
                } catch (Exception eSql)
                {
                    await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
                }
            }
        }

        private async void btn_Actualizar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (listItemSeleccionado != null)
            {
                try
                {
                    foreach (var alumno in AlumnosPtr)
                    {
                        if (alumno.id_alumno == listItemSeleccionado.id_alumno)
                        {
                            alumno.matricula = int.Parse(txtbx_matricula.Text);
                            alumno.nombre = txtbx_Nombre.Text;
                            alumno.apellidos = txtbx_apellidos.Text;
                            alumno.cod_postal = int.Parse(txtbx_codigo_postal.Text);
                            alumno.provincia = txtbx_provincia.Text;
                            if (await DBAssets.setActualizarAlumnoAsync((App.Current as App).ConnectionString, alumno) < 0)
                                await new MessageDialog("Error actualizando la fila de la base de datos!").ShowAsync();
                            break;
                        }
                    }
                } catch (Exception eSql)
                {
                    await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
                }
            }
        }
    }
}