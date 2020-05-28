using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Sistema_Gestor_de_Tutorias
{
    public sealed partial class ProfesoresItemControl : UserControl
    {
        public Modelos.ProfesoresItem profesoresItem { get { return this.DataContext as Modelos.ProfesoresItem; } }
        public ProfesoresItemControl()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }
    }
}
