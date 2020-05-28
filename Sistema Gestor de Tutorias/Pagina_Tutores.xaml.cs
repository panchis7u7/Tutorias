using iText.Layout.Element;
using Sistema_Gestor_de_Tutorias.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
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
        private List<Profesores> profesores;
        private Tutores tutorAgregar;
        public Pagina_Tutores()
        {
            this.InitializeComponent();
            TutoresItems = new ObservableCollection<TutoresItem>();
            profesores = new List<Profesores>();
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            TutoresFactory.getTutores("Tutores", TutoresItems);
        }

        private async void GruposGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            AgregarTutoresPopup.IsOpen = true;
            try
            {
                if ((App.Current as App).conexionBD.State == System.Data.ConnectionState.Open)
                {
                    String Query = "SELECT * FROM Profesores";
                    using (SqlCommand cmd = (App.Current as App).conexionBD.CreateCommand())
                    {
                        cmd.CommandText = Query;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Profesores p = new Profesores();
                                p.id_profesor = reader.GetInt32(0);
                                if (!await reader.IsDBNullAsync(1))
                                    p.nombre = reader.GetString(1).Trim(' ');
                                if (!await reader.IsDBNullAsync(2))
                                    p.apellidos = reader.GetString(2).Trim(' ');
                                if (!await reader.IsDBNullAsync(3))
                                    p.departamento = reader.GetString(3).Trim(' ');
                                profesores.Add(p);
                                cmbbx_profesores.Items.Add(p.nombre.Trim(' ') + " " + p.apellidos.Trim(' '));
                            }
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Resultados: " + eSql.Message);
            }
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

        private async void btn_Agregar_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            try
            {
                Tutores tutor = new Tutores();
                tutor.id_tutor = await GetId((App.Current as App).conexionBD, "SELECT (MAX(id_tutor) + 1) FROM Tutores");
                tutor.id_Profesor = profesores[cmbbx_profesores.SelectedIndex].id_profesor;
                string Query = "INSERT INTO Tutores (id_tutor, id_profesor) VALUES (@id_t, @id_p)";
                var conexion = (App.Current as App).conexionBD;
                SqlCommand cmd = conexion.CreateCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@id_t", tutor.id_tutor);
                cmd.Parameters.AddWithValue("@id_p", tutor.id_Profesor);
                if (await cmd.ExecuteNonQueryAsync() < 0)
                    await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();
                TutoresItems.Add(new TutoresItem()
                {
                    Categoria = "Tutores",
                    HeadLine = profesores[cmbbx_profesores.SelectedIndex].nombre + " " + profesores[cmbbx_profesores.SelectedIndex].nombre,
                    Subhead = profesores[cmbbx_profesores.SelectedIndex].departamento,
                    Imagen = "Assets/Usuario.png"
                });
            }
            catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
        }
        private void chkbx_Profesor_Checked(object sender, RoutedEventArgs e)
        {
            cmbbx_psicologosTutores.IsEnabled = false;
            cmbbx_profesores.IsEnabled = true;
        }
        private void chkbx_Profesor_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbbx_profesores.IsEnabled = false;
            cmbbx_psicologosTutores.IsEnabled = true;
        }

        public async Task<int> GetId(SqlConnection conexion, string Query)
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
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return 0;
        }
    }
}
