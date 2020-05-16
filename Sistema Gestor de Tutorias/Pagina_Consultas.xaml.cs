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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pagina_Consultas : Page
    {
        private int ultimoIdAlumnos;
        private int ultimoIdProfesores;
        private int ultimoIdProvincias;
        private int ultimoIdTutores;
        private int idTutorSeleccionado;
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
        public async Task<ObservableCollection<InfoAlumnos>> GetAlumnos(SqlConnection conexion, string Query)
        {
            var alumnos = new ObservableCollection<InfoAlumnos>();
            try
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                info_alumnos = new InfoAlumnos();
                                info_alumnos.id_alumno = reader.GetInt32(0);
                                if (!await reader.IsDBNullAsync(1))
                                    info_alumnos.matricula = reader.GetInt32(1);
                                if (!await reader.IsDBNullAsync(2))
                                    info_alumnos.nombre = reader.GetString(2);
                                if (!await reader.IsDBNullAsync(3))
                                    info_alumnos.apellidos = reader.GetString(3);
                                if (!await reader.IsDBNullAsync(4))
                                    info_alumnos.semestre = reader.GetInt32(4);
                                if (!await reader.IsDBNullAsync(5))
                                    info_alumnos.carrera = reader.GetString(5);
                                if (!await reader.IsDBNullAsync(6))
                                    info_alumnos.id_provincia = reader.GetInt32(6);
                                if (!await reader.IsDBNullAsync(7))
                                    info_alumnos.cod_postal = reader.GetInt32(7);
                                if (!await reader.IsDBNullAsync(8))
                                    info_alumnos.provincia= reader.GetString(8);
                                alumnos.Add(info_alumnos);
                            }
                        }
                    }
                }
            return alumnos;
        }
        catch (Exception eSql)
        {
            Debug.WriteLine("Exception: " + eSql.Message);
        }
        return null;
    }

        private void TextBlock_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            tx.Foreground = new SolidColorBrush(Colors.White);
        }

        private void TextBlock_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            tx.Foreground = new SolidColorBrush(Colors.OrangeRed);
        }

        private async void TextBlock_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            string GetAlumnosOrderByQuery = "SELECT DISTINCT Alumnos.id_alumno, Alumnos.matricula, Alumnos.nombre, Alumnos.apellidos, Alumnos.semestre, Alumnos.carrera, Provincias.id_provincia, Provincias.cod_postal, Provincias.provincia, Tutores.id_tutor FROM Grupos " +
                                            "INNER JOIN Alumnos ON Alumnos.id_alumno = Grupos.id_alumno " +
                                            "INNER JOIN Tutores ON Tutores.id_tutor = Grupos.id_tutor " +
                                            "INNER JOIN ResidenciasAlumnos ON ResidenciasAlumnos.id_alumno = Alumnos.id_alumno " +
                                            "INNER JOIN Provincias ON Provincias.id_provincia = ResidenciasAlumnos.id_provincia " +
                                            "AND CONCAT(TRIM(Tutores.nombre),' ', TRIM(Tutores.apellidos)) LIKE ('%" + grupo_seleccionado.Subhead + "%') " +
                                            "ORDER BY " + tx.Name;
            InventoryList.ItemsSource = await GetAlumnos((App.Current as App).conexionBD, GetAlumnosOrderByQuery);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            grupo_seleccionado = (e.Parameter) as GruposItem;
            string Query = "SELECT DISTINCT Alumnos.id_alumno, Alumnos.matricula, Alumnos.nombre, Alumnos.apellidos, Alumnos.semestre, Alumnos.carrera, Provincias.id_provincia, Provincias.cod_postal, Provincias.provincia FROM Grupos " +
                           "INNER JOIN Alumnos ON Alumnos.id_alumno = Grupos.id_alumno " +
                           "INNER JOIN Tutores ON Tutores.id_tutor = Grupos.id_tutor " +
                           "INNER JOIN ResidenciasAlumnos ON ResidenciasAlumnos.id_alumno = Alumnos.id_alumno " +
                           "INNER JOIN Provincias ON Provincias.id_provincia = ResidenciasAlumnos.id_provincia " +
                           "AND CONCAT(TRIM(Tutores.nombre),' ', TRIM(Tutores.apellidos)) LIKE ('%" + grupo_seleccionado.Subhead + "%')";
            AlumnosPtr = await GetAlumnos((App.Current as App).conexionBD, Query);
            InventoryList.ItemsSource = AlumnosPtr;
        }

        private async void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            InsertsPopup.IsOpen = true;
            info_alumnos = new InfoAlumnos();

            ultimoIdAlumnos = await GetId((App.Current as App).conexionBD, "SELECT (MAX(id_alumno) + 1) FROM Alumnos");
            info_alumnos.id_alumno = ultimoIdAlumnos;
            ultimoIdProfesores = await GetId((App.Current as App).conexionBD, "SELECT (MAX(id_profesor) + 1) FROM Profesores");
            ultimoIdProvincias = await GetId((App.Current as App).conexionBD, "SELECT (MAX(id_provincia) + 1) FROM Provincias");
            info_alumnos.id_provincia = ultimoIdProvincias;
            ultimoIdTutores = await GetId((App.Current as App).conexionBD, "SELECT (MAX(id_tutor) + 1) FROM Tutores");
            idTutorSeleccionado = await GetId((App.Current as App).conexionBD, "SELECT DISTINCT id_tutor FROM Tutores WHERE CONCAT(TRIM(Tutores.nombre),' ', TRIM(Tutores.apellidos)) LIKE ('%" + grupo_seleccionado.Subhead + "%')");
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
        private async void Button_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            var conexion = (App.Current as App).conexionBD;
            string[] Query = {"INSERT INTO Alumnos (id_alumno, matricula, nombre, apellidos, semestre, carrera) VALUES (@id_a, @m, @n, @a, @s, @c)",
                               "INSERT INTO Provincias (id_provincia, cod_postal, provincia) VALUES (@id_p, @cp, @p)",
                               "INSERT INTO ResidenciasAlumnos (id_alumno, id_provincia) VALUES (@id_fa, @id_fp)",
                               "INSERT INTO Grupos (id_alumno, id_tutor, grupo) VALUES (@id_tfa, @id_ft, @g)"};
            {

                SqlCommand cmd = conexion.CreateCommand();
                cmd.CommandText = Query[0];
                int result;
                cmd.Parameters.AddWithValue("@id_a", ultimoIdAlumnos);
                cmd.Parameters.AddWithValue("@m", info_alumnos.matricula);
                cmd.Parameters.AddWithValue("@n", info_alumnos.nombre);
                cmd.Parameters.AddWithValue("@a", info_alumnos.apellidos);
                cmd.Parameters.AddWithValue("@s", info_alumnos.semestre);
                cmd.Parameters.AddWithValue("@c", info_alumnos.carrera);
                result = await cmd.ExecuteNonQueryAsync();

                cmd.CommandText = Query[1];
                cmd.Parameters.AddWithValue("@id_p", ultimoIdProvincias);
                cmd.Parameters.AddWithValue("@cp", info_alumnos.cod_postal);
                cmd.Parameters.AddWithValue("@p", info_alumnos.provincia);
                result = await cmd.ExecuteNonQueryAsync();

                cmd.CommandText = Query[2];
                cmd.Parameters.AddWithValue("@id_fa", ultimoIdAlumnos);
                cmd.Parameters.AddWithValue("@id_fp", ultimoIdProvincias);
                result = await cmd.ExecuteNonQueryAsync();

                cmd.CommandText = Query[3];
                cmd.Parameters.AddWithValue("@id_tfa", ultimoIdAlumnos);
                cmd.Parameters.AddWithValue("@id_ft", idTutorSeleccionado);
                cmd.Parameters.AddWithValue("@g", grupo_seleccionado.Grupo);
                result = await cmd.ExecuteNonQueryAsync();

                AlumnosPtr.Add(info_alumnos);

                // Check Error
                if (result < 0)
                    await new MessageDialog("Error insertando informacion a la base de datos!").ShowAsync();
            }
                            var err = new MessageDialog(info_alumnos.matricula + info_alumnos.nombre + info_alumnos.apellidos);
            await err.ShowAsync();
        }
    }
}