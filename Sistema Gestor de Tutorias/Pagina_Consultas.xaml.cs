using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Pagina_Consultas : Page
    {
        public Pagina_Consultas()
        {
            this.InitializeComponent();
        }

        public async Task<ObservableCollection<Alumnos>> GetAlumnos(string connectionString)
        {
            const string GetAlumnosQuery = "select * from Alumnos;";
            var alumnos = new ObservableCollection<Alumnos>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetAlumnosQuery;
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var alumno = new Alumnos();
                                    alumno.id_alumno = reader.GetInt32(0);
                                    if (!await reader.IsDBNullAsync(1))
                                        alumno.matricula = reader.GetInt32(1);
                                    if (!await reader.IsDBNullAsync(2))
                                        alumno.nombre = reader.GetString(2);
                                    if (!await reader.IsDBNullAsync(3))
                                        alumno.apellidos = reader.GetString(3);
                                    if (!await reader.IsDBNullAsync(4))
                                        alumno.semestre = reader.GetInt32(4);
                                    if (!await reader.IsDBNullAsync(5))
                                        alumno.carrera = reader.GetString(5);
                                    alumnos.Add(alumno);
                                }
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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InventoryList.ItemsSource = await GetAlumnos((App.Current as App).ConnectionString);
        }
    }
}
