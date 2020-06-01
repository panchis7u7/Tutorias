
namespace Sistema_Gestor_de_Tutorias
{
    public class Profesores
    {
        public int id_profesor { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string departamento { get; set; }

        public override string ToString()
        {
            return (this.nombre + " " + this.apellidos);
        }
    }
}
