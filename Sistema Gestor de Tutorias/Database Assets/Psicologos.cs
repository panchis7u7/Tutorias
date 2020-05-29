
namespace Sistema_Gestor_de_Tutorias.Database_Assets
{
    public class Psicologos
    {
        public int id_psicologo { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public override string ToString()
        {
            return (this.nombre + " " + this.apellidos);
        }
    }
}
