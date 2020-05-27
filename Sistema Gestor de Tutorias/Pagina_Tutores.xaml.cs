using Sistema_Gestor_de_Tutorias.Modelos;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
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

        private void GruposGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            AgregarTutoresPopup.IsOpen = true;
        }

        private void AgregarTutoresPopup_LayoutUpdated(object sender, object e)
        {
            if (relativeChild.ActualWidth == 0 && relativeChild.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.AgregarTutoresPopup.HorizontalOffset;
            double ActualVerticalOffset = this.AgregarTutoresPopup.VerticalOffset;

            relativeChild.Height = (int)(Window.Current.Bounds.Height / 2);

            double NewHorizontalOffset = (int)(Window.Current.Bounds.Width - relativeChild.ActualWidth) / 2;
            double NewVerticalOffset = (int)(Window.Current.Bounds.Height - relativeChild.ActualHeight) / 2;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.AgregarTutoresPopup.HorizontalOffset = NewHorizontalOffset;
                this.AgregarTutoresPopup.VerticalOffset = NewVerticalOffset;
            }
        }
    }
}
