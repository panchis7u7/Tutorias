using Sistema_Gestor_de_Tutorias.Database_Assets;
using Sistema_Gestor_de_Tutorias.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pagina_Tutores : Page
    {
        private ObservableCollection<TutoresItem> TutoresItems;
        private List<Profesores> profesores;
        private TutoresItem itemTutorSeleccionado;
        private NavigationView navigationView;
        public Pagina_Tutores()
        {
            this.InitializeComponent();
            TutoresItems = new ObservableCollection<TutoresItem>();
            profesores = new List<Profesores>();
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            TutoresFactory.getTutores("Tutores", TutoresItems);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationView = e.Parameter as NavigationView;
        }

        private void TutoresGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            TutoresItem item = e.ClickedItem as TutoresItem;
            itemTutorSeleccionado = item;
            InfoTutoresPopup.IsOpen = true;
            txtbx_Nombre.Text = item.tutor.nombre;
            txtbx_Apellidos.Text = item.tutor.apellidos;
        }

        private void InfoTutoresPopup_LayoutUpdated(object sender, object e)
        {
            if (relativeChild.ActualWidth == 0 && relativeChild.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.InfoTutoresPopup.HorizontalOffset;
            double ActualVerticalOffset = this.InfoTutoresPopup.VerticalOffset;

            relativeChild.Height = (int)(Window.Current.Bounds.Height / 2);

            double NewHorizontalOffset;
            if (navigationView.IsPaneOpen)
                NewHorizontalOffset = (int)((Window.Current.Bounds.Width - relativeChild.ActualWidth) / 2) - 250;
            else
                NewHorizontalOffset = (int)((Window.Current.Bounds.Width - relativeChild.ActualWidth) / 2) - 30;

            double NewVerticalOffset = (int)((Window.Current.Bounds.Height - relativeChild.ActualHeight) / 2) - 30;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.InfoTutoresPopup.HorizontalOffset = NewHorizontalOffset;
                this.InfoTutoresPopup.VerticalOffset = NewVerticalOffset;
            }
        }

        private async void btn_Actualizar_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (itemTutorSeleccionado != null)
            {
                try
                {
                        foreach (var tutor in TutoresItems)
                        {
                            if (tutor.tutor.id_tutor == itemTutorSeleccionado.tutor.id_tutor)
                            {
                                tutor.tutor.nombre = txtbx_Nombre.Text;
                                tutor.tutor.apellidos = txtbx_Apellidos.Text;
                                tutor.HeadLine = txtbx_Nombre + " " + txtbx_Apellidos;
                                if (await DBAssets.setActualizarTutorAsync((App.Current as App).ConnectionString, tutor.tutor) < 0)
                                    await new MessageDialog("Error actualizando la fila de la base de datos!").ShowAsync();
                            break;
                            }
                        }
                }
                catch (Exception eSql)
                {
                    await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
                }
            }
        }
    }
}
