
namespace Sistema_Gestor_de_Tutorias
{
    public class Alumnos
    {
        public int id_alumno { get; set; }
        public int matricula { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }

        public override string ToString()
        {
            return nombre + " " + apellidos;
        }
    }
}
