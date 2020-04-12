using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Gestor_de_Tutorias
{
    class Provincias : INotifyPropertyChanged
    {
        public int id_provincia { get; set; }
        public int cod_postal { get; set; }
        public string provincia { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
