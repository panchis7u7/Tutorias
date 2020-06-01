using Sistema_Gestor_de_Tutorias.Database_Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public InfoProfesores profesor { get; set; }
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
                List<InfoProfesores> profesores = await DBAssets.getProfesoresAsync((App.Current as App).ConnectionString);
                profesores.ForEach(p => items.Add(new ProfesoresItem()
                {
                    Id = p.id_profesor,
                    Categoria = "Tutores",
                    HeadLine = p.nombre + " " + p.apellidos,
                    Subhead = p.departamento,
                    profesor = p,
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
    }
}
