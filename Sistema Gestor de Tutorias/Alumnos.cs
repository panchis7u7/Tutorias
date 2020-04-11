using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Gestor_de_Tutorias
{
    public class Alumnos : INotifyPropertyChanged
    {
        public int id_alumno { get; set; }
        public int matricula { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public int semestre { get; set; }
        public string carrera { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
