using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Gestor_de_Tutorias.Modelos
{
    public class Formato
    {
        public int formato_id { get; set; }
        public string titulo { get; set; }
        public string imagen_cubierta { get; set; }
    }

    public class FormatoManager
    {
        public static List<Formato> GetFormatos()
        {
            var formatos = new List<Formato>();
            formatos.Add(new Formato { formato_id = 1, titulo = "Nombramiento de Tutores", imagen_cubierta="Assets/Nombramiento_Tutores.png"});
            formatos.Add(new Formato { formato_id = 2, titulo = "Nombramiento de Docente", imagen_cubierta = "Assets/Nombramiento_de_Docente_Tutor.png" });
            formatos.Add(new Formato { formato_id = 3, titulo = "Canalizacion Psicologia", imagen_cubierta = "Assets/Canalizacion_Interna_Psicologia.png" });
            formatos.Add(new Formato { formato_id = 4, titulo = "Credito Tutorados", imagen_cubierta = "Assets/Tutorados_Credito_Complementario.png" });
            formatos.Add(new Formato { formato_id = 5, titulo = "Recibido de Constancias", imagen_cubierta = "Assets/Tutorados_Credito_Complementario.png" });
            formatos.Add(new Formato { formato_id = 6, titulo = "Firma Recibido Constancias", imagen_cubierta = "Assets/Recibido_Constancias_Credito.png" });
            formatos.Add(new Formato { formato_id = 7, titulo = "Firma Recibido Informes", imagen_cubierta = "Assets/Recibido_Constancias_Credito.png" });
            formatos.Add(new Formato { formato_id = 8, titulo = "Envio Anexos", imagen_cubierta = "Assets/Envio_Anexos.png" });
            formatos.Add(new Formato { formato_id = 9, titulo = "Prevencion Ausentismo", imagen_cubierta = "Assets/Prevencion_Baja_Ausentismo.png" });
            return formatos;
        }
    }
}
