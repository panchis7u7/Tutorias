﻿using System.IO;
namespace Sistema_Gestor_de_Tutorias.Database_Assets
{
    public class InfoProfesores
    {
        public int id_profesor { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string departamento { get; set; }
        public string correo { get; set; }
        public Stream imagen { get; set; }
        public int  id_provincia { get; set; }
        public int cod_postal { get; set; }
        public string provincia { get; set; }

        public override string ToString()
        {
            return (this.nombre + " " + this.apellidos);
        }
    }
}
