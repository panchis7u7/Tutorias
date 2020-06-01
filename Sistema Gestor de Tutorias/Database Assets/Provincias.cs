using System.ComponentModel;

namespace Sistema_Gestor_de_Tutorias
{
    public class Provincias : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int id_provincia { get; set; }
        public int cod_postal { get; set; }
        public string provincia { get; set; }
        private void OnNotifyPropertyChanged(string propertyName) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
