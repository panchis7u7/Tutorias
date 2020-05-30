
namespace Sistema_Gestor_de_Tutorias.Database_Assets
{
    public class InfoGruposGS
    {
        public string grupo { get; set; }
        public int semestre { get; set; }
        public override string ToString()
        {
            return grupo + ", " + semestre + " Semestre.";
        }
    }
}
