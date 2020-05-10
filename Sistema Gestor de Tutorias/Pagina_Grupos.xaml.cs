using Sistema_Gestor_de_Tutorias.Modelos;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using System;
using Windows.UI.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pagina_Grupos : Page
    {
        private ObservableCollection<GruposItem> GruposItems;
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
}
