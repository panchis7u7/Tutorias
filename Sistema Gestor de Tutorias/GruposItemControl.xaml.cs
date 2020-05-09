using Sistema_Gestor_de_Tutorias;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Sistema_Gestor_de_Tutorias
{
    public sealed partial class GruposItemControl : UserControl
    {
        public Modelos.GruposItem GruposItem { get { return this.DataContext as Modelos.GruposItem; } }
        public GruposItemControl()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }
    }
}
