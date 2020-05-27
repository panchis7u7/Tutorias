using Sistema_Gestor_de_Tutorias.Modelos;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using System;
using Windows.UI.Core;
using Sistema_Gestor_de_Tutorias.Database_Assets;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pagina_Grupos : Page
    {
        private ObservableCollection<GruposItem> GruposItems;
        public InfoGruposTutor InfoGruposTutores;
        public Pagina_Grupos()
        {
            this.InitializeComponent();
            GruposItems = new ObservableCollection<GruposItem>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GruposFactory.GetGrupos("Grupos", GruposItems);
        }

        private async void GruposGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            var myView = CoreApplication.CreateNewView();
            int newViewId = 0;
            //int numero = (e.ClickedItem as Formato).formato_id;
            object f = e.ClickedItem;
            GruposItem item = (e.ClickedItem) as GruposItem;
            if (item.Categoria == "Agregar")
            {
                this.AgregarGruposPopup.IsOpen = true;
            }
            else
            {
                await myView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Frame newFrame = new Frame();
                    newFrame.Navigate(typeof(Pagina_Consultas), f);
                    Window.Current.Content = newFrame;
                    Window.Current.Activate();
                    newViewId = ApplicationView.GetForCurrentView().Id;
                });
                await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId, ViewSizePreference.UseHalf);
            }
        }

        private void AgregarGruposPopup_LayoutUpdated(object sender, object e)
        {
            if (relativeChild.ActualWidth == 0 && relativeChild.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.AgregarGruposPopup.HorizontalOffset;
            double ActualVerticalOffset = this.AgregarGruposPopup.VerticalOffset;

            relativeChild.Height = (int)(Window.Current.Bounds.Height / 2);

            double NewHorizontalOffset = (int)(Window.Current.Bounds.Width - relativeChild.ActualWidth) / 2;
            double NewVerticalOffset = (int)(Window.Current.Bounds.Height - relativeChild.ActualHeight) / 2;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.AgregarGruposPopup.HorizontalOffset = NewHorizontalOffset;
                this.AgregarGruposPopup.VerticalOffset = NewVerticalOffset;
            }
        }
    }
}
