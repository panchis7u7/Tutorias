using System;
using System.Collections.ObjectModel;
using Sistema_Gestor_de_Tutorias.Modelos;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pagina_Consultas : Page
    {
        private InfoAlumnos info_alumnos;
        public GruposItem grupo_seleccionado;
        public Pagina_Consultas()
        {
            this.InitializeComponent();
        }

        public async Task<ObservableCollection<InfoAlumnos>> GetAlumnos(SqlConnection conexion, string Query)
        {
            int id = 1;
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
                                info_alumnos.id_alumno = id;
                                if (!await reader.IsDBNullAsync(0))
                                    info_alumnos.matricula = reader.GetInt32(0);
                                if (!await reader.IsDBNullAsync(1))
                                    info_alumnos.nombre = reader.GetString(1);
                                if (!await reader.IsDBNullAsync(2))
                                    info_alumnos.apellidos = reader.GetString(2);
                                if (!await reader.IsDBNullAsync(3))
                                    info_alumnos.semestre = reader.GetInt32(3);
                                if (!await reader.IsDBNullAsync(4))
                                    info_alumnos.carrera = reader.GetString(4);
                                if (!await reader.IsDBNullAsync(5))
                                    info_alumnos.cod_postal = reader.GetInt32(5);
                                if (!await reader.IsDBNullAsync(6))
                                    info_alumnos.provincia= reader.GetString(6);
                                alumnos.Add(info_alumnos);
                                id += 1;
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

        //private async void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    const string GetAlumnosQuery = "SELECT Provincias.id_provincia, Alumnos.matricula, Alumnos.nombre, Alumnos.apellidos, Alumnos.semestre, Alumnos.carrera, Provincias.cod_postal, Provincias.provincia FROM Alumnos" +
        //                                   " INNER JOIN ResidenciasAlumnos ON Alumnos.id_alumno = ResidenciasAlumnos.id_alumno" +
        //                                   " INNER JOIN Provincias ON ResidenciasAlumnos.id_provincia = Provincias.id_provincia";
        //    InventoryList.ItemsSource = await GetAlumnos((App.Current as App).conexionBD, GetAlumnosQuery);
        //}

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
            string GetAlumnosOrderByQuery = "SELECT Alumnos.matricula, Alumnos.nombre, Alumnos.apellidos, Alumnos.semestre, Alumnos.carrera, Provincias.cod_postal, Provincias.provincia FROM Grupos " +
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
            grupo_seleccionado = (e.Parameter) as GruposItem;
            string Query = "SELECT Alumnos.matricula, Alumnos.nombre, Alumnos.apellidos, Alumnos.semestre, Alumnos.carrera, Provincias.cod_postal, Provincias.provincia FROM Grupos " +
                           "INNER JOIN Alumnos ON Alumnos.id_alumno = Grupos.id_alumno " +
                           "INNER JOIN Tutores ON Tutores.id_tutor = Grupos.id_tutor " +
                           "INNER JOIN ResidenciasAlumnos ON ResidenciasAlumnos.id_alumno = Alumnos.id_alumno " +
                           "INNER JOIN Provincias ON Provincias.id_provincia = ResidenciasAlumnos.id_provincia " +
                           "AND CONCAT(TRIM(Tutores.nombre),' ', TRIM(Tutores.apellidos)) LIKE ('%" + grupo_seleccionado.Subhead + "%')";
            InventoryList.ItemsSource = await GetAlumnos((App.Current as App).conexionBD, Query);
        }
    }
}
