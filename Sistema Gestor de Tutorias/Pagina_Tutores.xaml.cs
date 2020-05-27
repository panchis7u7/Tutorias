using Sistema_Gestor_de_Tutorias.Modelos;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pagina_Tutores : Page
    {
        private ObservableCollection<TutoresItem> TutoresItems;
        public Pagina_Tutores()
        {
            this.InitializeComponent();
            TutoresItems = new ObservableCollection<TutoresItem>();
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            TutoresFactory.getTutores("Tutores", TutoresItems);
        }

        private void AgregarGruposPopup_LayoutUpdated(object sender, object e)
        {

        }

        private void GruposGrid_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
