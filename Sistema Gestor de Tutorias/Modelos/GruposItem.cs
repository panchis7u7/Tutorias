using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Gestor_de_Tutorias.Modelos
{
    public class GruposItem
    {
        public int Id { get; set; }
        public string Categoria { get; set; }
        public string HeadLine { get; set; }
        public string Subhead { get; set; }
        public string DateLine { get; set; }
        public string Imagen { get; set; }
    }
    
    //Factorys o Clases, te permiten crear instancias de objetos nuevos.
    public class GruposFactory
    {
        public static void GetGrupos(string categoria, ObservableCollection<GruposItem> gruposItems)
        {
            var allItems = getGruposItems();
            gruposItems.Clear();
            allItems.ForEach(p => gruposItems.Add(p));
        }

        private static List<GruposItem> getGruposItems()
        {
            var items = new List<GruposItem>();
            items.Add(new GruposItem() { Id = 1, Categoria = "Grupos", HeadLine = "Lorem Ipsum", DateLine = "Nunc tristique nec", Subhead = "doro sit amet", Imagen = "Assets/Polygon.png" });
            items.Add(new GruposItem() { Id = 2, Categoria = "Grupos", HeadLine = "Lorem Ipsum", DateLine = "Nunc tristique nec", Subhead = "doro sit amet", Imagen = "Assets/Polygon.png" });
            items.Add(new GruposItem() { Id = 3, Categoria = "Grupos", HeadLine = "Lorem Ipsum", DateLine = "Nunc tristique nec", Subhead = "doro sit amet", Imagen = "Assets/Polygon.png" });
            return items;
        }
    }
}
