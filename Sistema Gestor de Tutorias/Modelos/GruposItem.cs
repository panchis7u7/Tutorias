using Sistema_Gestor_de_Tutorias.Database_Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Popups;

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
        public InfoGruposTutor grupo { get; set; }
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

        private async static Task<List<GruposItem>> getGruposItems()
        {
            try
            {
                var items = new List<GruposItem>();           
                var grupos = await DBAssets.GetInfoGruposAsync((App.Current as App).ConnectionString);
                if (grupos != null)
                    grupos.ForEach(p => items.Add(new GruposItem()
                    {
                        Id = p.id_tutor,
                        Categoria = "Grupos",
                        Grupo = p.grupo.Trim(' '),
                        HeadLine = "Grupo " + p.grupo.Trim(' '),
                        DateLine = p.carrera.Trim(' '),
                        Subhead = p.nombre.Trim(' ') + " " + p.apellidos.Trim(' '),
                        Semestre = p.semestre + " Semestre.",
                        grupo = p,
                        Imagen = "Assets/Antena.png"
                    }));
                items.Add(new GruposItem() { Id = 4, Categoria = "Agregar", HeadLine = "Agregar Grupo", DateLine = " ", Subhead = " ", Imagen = "Assets/Add.png" });
                return items;
            } catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
            return null;
        }
    }
}
