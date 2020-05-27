using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Gestor_de_Tutorias.Database_Assets
{
    public class InfoGruposTutor
    {
        public int id_grupo { get; set; }
        public char grupo { get; set; }
        public int id_tutor { get; set; }
        public string tutor { get; set; }
        public string departamento { get; set; }
    }
}
