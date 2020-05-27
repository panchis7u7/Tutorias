using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Sistema_Gestor_de_Tutorias.Modelos
{
    public class TutoresItem
    {
        public int Id { get; set; }
        public string Categoria { get; set; }
        public string HeadLine { get; set; }
        public string Subhead { get; set; }
        //public string DateLine { get; set; }
        public string Imagen { get; set; }
    }

    public class TutoresFactory
    {
        public async static void getTutores(string categoria, ObservableCollection<TutoresItem> tutoresItems)
        {
            var allItems = await GetTutoresItems();
            tutoresItems.Clear();
            allItems.ForEach(p => tutoresItems.Add(p));
        }

        private async static Task<List<TutoresItem>> GetTutoresItems()
        {
            try
            {
                var items = new List<TutoresItem>();
                string Query = "SELECT DISTINCT * FROM TUTORES";
                var tutores = await GetTutoresAsync((App.Current as App).conexionBD, Query);
                tutores.ForEach(p => items.Add(new TutoresItem()
                {
                    Id = p.id_tutor,
                    Categoria = "Tutores",
                    HeadLine = p.nombre + " " + p.apellidos,
                    Subhead = p.departamento,
                    Imagen = "Assets/Usuario.png"
                }));
                return items;
            } catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
            return null;
        }

        private async static Task<List<Tutores>> GetTutoresAsync(SqlConnection conexion, string Query)
        {
            int i = 0;
            var tutores = new List<Tutores>();
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
                                Tutores tutor = new Tutores();;
                                tutor.id_tutor = reader.GetInt32(0);
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
