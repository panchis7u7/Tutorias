using Sistema_Gestor_de_Tutorias;
using Sistema_Gestor_de_Tutorias.Database_Assets;
using Windows.UI.Xaml;
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

        private void MainPanelGrid_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            //if (this.GruposItem.Categoria == "Agregar")
            //{
            //    flyout_menu.Hide();
            //} else
            //{
            //    flyout_menu.ShowAt(sender as UIElement, e.GetPosition(sender as UIElement));
            //}
            flyout_menu.ShowAt(sender as UIElement, e.GetPosition(sender as UIElement));
        }

        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            await DBAssets.setBajaGrupoAsync((App.Current as App).ConnectionString, this.GruposItem.grupo.id_tutor);
            this.Visibility = Visibility.Collapsed;
            this.IsEnabled = false;
        }
    }
}
