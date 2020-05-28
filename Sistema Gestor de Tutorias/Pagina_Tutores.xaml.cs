using Sistema_Gestor_de_Tutorias.Modelos;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pagina_Tutores : Page
    {
        private ObservableCollection<TutoresItem> TutoresItems;
        private Tutores tutorAgregar;
        public Pagina_Tutores()
        {
            this.InitializeComponent();
            TutoresItems = new ObservableCollection<TutoresItem>();
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            TutoresFactory.getTutores("Tutores", TutoresItems);
        }

        private void GruposGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            AgregarTutoresPopup.IsOpen = true;
        }

        private void AgregarTutoresPopup_LayoutUpdated(object sender, object e)
        {
            if (relativeChild.ActualWidth == 0 && relativeChild.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.AgregarTutoresPopup.HorizontalOffset;
            double ActualVerticalOffset = this.AgregarTutoresPopup.VerticalOffset;

            relativeChild.Height = (int)(Window.Current.Bounds.Height / 2);

            double NewHorizontalOffset = (int)(Window.Current.Bounds.Width - relativeChild.ActualWidth) / 2;
            double NewVerticalOffset = (int)(Window.Current.Bounds.Height - relativeChild.ActualHeight) / 2;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.AgregarTutoresPopup.HorizontalOffset = NewHorizontalOffset;
                this.AgregarTutoresPopup.VerticalOffset = NewVerticalOffset;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cmbbx_profesores.IsEnabled = true;
            txtbx_nombre.IsEnabled = false;
            txtbx_apellidos.IsEnabled = false;
            txtbx_departamento.IsEnabled = false;
        }

        private void chkbx_Profesor_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbbx_profesores.IsEnabled = false;
            txtbx_nombre.IsEnabled = true;
            txtbx_apellidos.IsEnabled = true;
            txtbx_departamento.IsEnabled = true;
        }

        private async void btn_Agregar_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (tutorAgregar == null)
            tutorAgregar = new Tutores();
            tutorAgregar.id_tutor = 5;
            if(chkbx_Profesor.IsChecked == true)
            {

            } else
            {
                try
                {
                    string Query = "INSERT INTO Tutores (id_tutor, nombre, apellidos, departamento) VALUES (@id_t, @n, @a, @d)";
                    this.DataContextChanged += (s, x) => Bindings.Update();
                    var conexion = (App.Current as App).conexionBD;
                    SqlCommand cmd = conexion.CreateCommand();
                    cmd.CommandText = Query;
                    cmd.Parameters.AddWithValue("@id_t", tutorAgregar.id_tutor);
                    cmd.Parameters.AddWithValue("@n", tutorAgregar.nombre);
                    cmd.Parameters.AddWithValue("@a", tutorAgregar.apellidos);
                    cmd.Parameters.AddWithValue("@d", tutorAgregar.departamento);
                    if (await cmd.ExecuteNonQueryAsync() < 0)
                        await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();

                } catch (Exception eSql)
                {
                    await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
                }
            }
        }
    }
}
