using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Sistema_Gestor_de_Tutorias.Modelos;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pagina_Formatos : Page
    {
        private List<Formato> formatos;
        public Pagina_Formatos()
        {
            this.InitializeComponent();
            formatos = FormatoManager.GetFormatos();
        }

        private async void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var myView = CoreApplication.CreateNewView();
            int newViewId= 0;
            await myView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame newFrame = new Frame();
                newFrame.Navigate(typeof(Editor), null);
                Window.Current.Content = newFrame;
                Window.Current.Activate();
                newViewId = ApplicationView.GetForCurrentView().Id;
            });
            await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId,ViewSizePreference.UseHalf);
        }
    }
}
