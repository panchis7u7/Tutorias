using Sistema_Gestor_de_Tutorias.Database_Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Sistema_Gestor_de_Tutorias.Modelos
{
    public class GruposItem
    {
        public int Id { get; set; }
        public string Categoria { get; set; }
        public string Grupo { get; set; }
        public string HeadLine { get; set; }
        public string Subhead { get; set; }
        public string Semestre { get; set; }
        public string DateLine { get; set; }
        public string Imagen { get; set; }
    }
    
    //Factorys o Clases, te permiten crear instancias de objetos nuevos.
    public class GruposFactory
    {
        public async static void GetGrupos(string categoria, ObservableCollection<GruposItem> gruposItems)
        {
            var allItems = await getGruposItems();
            gruposItems.Clear();
            allItems.ForEach(p => gruposItems.Add(p));
        }
        public async static Task<GridView> GetGrupos(ObservableCollection<GruposItem> gruposItems)
        {
            try
            {
                List<string> carreras = new List<string>();
                List<int> semestres = new List<int>();
                var conexion = (App.Current as App).conexionBD;
                string[] Query = { "SELECT DISTINCT carrera FROM Alumnos",
                                   "SELECT DISTINCT semestre FROM Alumnos WHERE carrera = ",
                                   "SELECT DISTINCT grupo FROM Grupos " +
                                   "INNER JOIN Alumnos ON Alumnos.id_alumno = Grupos.id_alumno " +
                                   "WHERE Alumnos.carrera = 'Ing. TICs' AND Alumnos.semestre = "};
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query[0];
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                carreras.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                foreach(var carrera in carreras)
                {
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conexion.CreateCommand())
                        {
                            cmd.CommandText = Query[1] + "'" + carrera + "'";
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    carreras.Add(reader.GetString(0));
                                }
                            }
                        }
                    }
                }
                foreach (var semestre in semestres)
                {
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conexion.CreateCommand())
                        {
                            cmd.CommandText = Query[2] + semestre;
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    semestres.Add(reader.GetInt32(0));
                                }
                            }
                        }
                    }
                }


            } catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
            var allItems = await getGruposItems();
            gruposItems.Clear();
            allItems.ForEach(p => gruposItems.Add(p));
            return new GridView();
        }

        private async static Task<List<GruposItem>> getGruposItems()
        {
            try
            {
                var items = new List<GruposItem>();
                string Query = "SELECT DISTINCT grupo, Tutores.nombre, Tutores.apellidos, Alumnos.semestre, Alumnos.carrera FROM Grupos " +
                               "INNER JOIN Tutores ON Tutores.id_tutor = Grupos.id_tutor " +
                               "INNER JOIN Alumnos ON Alumnos.id_alumno = Grupos.id_alumno";
                var grupos = await GetGruposAsync((App.Current as App).conexionBD, Query);
                //items.Add(new GruposItem() { Id = 1, Categoria = "Grupos", HeadLine = "Lorem Ipsum", DateLine = "Nunc tristique nec", Subhead = "doro sit amet", Imagen = "Assets/Cerdito.png" });
                //items.Add(new GruposItem() { Id = 2, Categoria = "Grupos", HeadLine = "Lorem Ipsum", DateLine = "Nunc tristique nec", Subhead = "doro sit amet", Imagen = "Assets/Antena.png" });
                //items.Add(new GruposItem() { Id = 3, Categoria = "Grupos", HeadLine = "Lorem Ipsum", DateLine = "Nunc tristique nec", Subhead = "doro sit amet", Imagen = "Assets/Social.png" });
                grupos.ForEach(p => items.Add(new GruposItem()
                {
                    Id = p.id,
                    Categoria = "Grupos",
                    Grupo = p.grupo.Trim(' '),
                    HeadLine = "Grupo " + p.grupo.Trim(' '),
                    DateLine = p.carrera.Trim(' '),
                    Subhead = p.nombre.Trim(' ') + " " + p.apellidos.Trim(' '),
                    Semestre = p.semestre + " Semestre.",
                    Imagen = "Assets/Antena.png"
                }));
                return items;
            } catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
            return null;
        }

        private async static Task<List<InfoGrupos>> GetGruposAsync(SqlConnection conexion, string Query)
        {
            int i = 0;
            var grupos = new List<InfoGrupos>();
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
                                InfoGrupos grupo = new InfoGrupos();
                                grupo.id = i;
                                grupo.grupo = reader.GetString(0);
                                if (!await reader.IsDBNullAsync(1))
                                    grupo.nombre = reader.GetString(1);
                                if (!await reader.IsDBNullAsync(2))
                                    grupo.apellidos = reader.GetString(2);
                                if (!await reader.IsDBNullAsync(3))
                                    grupo.semestre = reader.GetInt32(3);
                                if (!await reader.IsDBNullAsync(4))
                                    grupo.carrera = reader.GetString(4);
                                grupos.Add(grupo);
                                i += 1;
                            }
                        }
                    }
                }
                return grupos;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }
    }
}
