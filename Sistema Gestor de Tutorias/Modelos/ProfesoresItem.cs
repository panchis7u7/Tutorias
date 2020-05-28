
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Sistema_Gestor_de_Tutorias.Modelos
{
    public class ProfesoresItem
    {
        public int Id { get; set; }
        public string Categoria { get; set; }
        public string HeadLine { get; set; }
        public string Subhead { get; set; }
        //public string DateLine { get; set; }
        public string Imagen { get; set; }
    }

    public class ProfesoresFactory
    {
        public async static void getProfesores(string categoria, ObservableCollection<ProfesoresItem> tutoresItems)
        {
            var allItems = await GetProfesoresItems();
            tutoresItems.Clear();
            allItems.ForEach(p => tutoresItems.Add(p));
        }

        private async static Task<List<ProfesoresItem>> GetProfesoresItems()
        {
            try
            {
                var items = new List<ProfesoresItem>();
                string Query = "SELECT Profesores.id_profesor, Profesores.nombre, Profesores.apellidos, Profesores.departamento FROM Profesores;";
                var tutores = await GetProfesoresAsync((App.Current as App).conexionBD, Query);
                tutores.ForEach(p => items.Add(new ProfesoresItem()
                {
                    Id = p.id_profesor,
                    Categoria = "Tutores",
                    HeadLine = p.nombre + " " + p.apellidos,
                    Subhead = p.departamento,
                    Imagen = "Assets/Usuario.png"
                }));
                items.Add(new ProfesoresItem() { Id = 0, Categoria = "Agregar", HeadLine = "Agregar Profesor", Subhead = " ", Imagen = "Assets/Add.png" });
                return items;
            }
            catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
            return null;
        }

        private async static Task<List<Profesores>> GetProfesoresAsync(SqlConnection conexion, string Query)
        {
            int i = 0;
            var tutores = new List<Profesores>();
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
                                Profesores tutor = new Profesores(); ;
                                tutor.id_profesor = reader.GetInt32(0);
                                if (!await reader.IsDBNullAsync(1))
                                    tutor.nombre = reader.GetString(1).Trim(' ');
                                if (!await reader.IsDBNullAsync(2))
                                    tutor.apellidos = reader.GetString(2).Trim(' ');
                                if (!await reader.IsDBNullAsync(3))
                                    tutor.departamento = reader.GetString(3).Trim(' ');
                                tutores.Add(tutor);
                            }
                        }
                    }
                }
                return tutores;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }
    }
}
