using Sistema_Gestor_de_Tutorias.Modelos;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using System;
using Windows.UI.Core;
using Sistema_Gestor_de_Tutorias.Database_Assets;
using Windows.UI.Xaml.Navigation;
using System.Data.SqlClient;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pagina_Grupos : Page
    {
        private ObservableCollection<GruposItem> GruposItems;
        private ObservableCollection<Profesores> profesores;
        private ObservableCollection<Psicologos> psicologos;

        public InfoGruposTutor InfoGruposTutores;
        public Pagina_Grupos()
        {
            this.InitializeComponent();
            GruposItems = new ObservableCollection<GruposItem>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            GruposFactory.GetGrupos("Grupos", GruposItems);
            profesores = await DBAssets.getProfesoresAsync((App.Current as App).ConnectionString);
            psicologos = await DBAssets.getPsicologosAsync((App.Current as App).ConnectionString);
            cmbbx_carrera.ItemsSource = await DBAssets.getCarrerasAsync((App.Current as App).ConnectionString);
            cmbbx_Profesores.ItemsSource = profesores;
            cmbbx_Psicologo.ItemsSource = psicologos;
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

        private async void btn_Agregar_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            try
            {
                Tutores tutor = new Tutores();
                tutor.id_tutor = await DBAssets.GetId((App.Current as App).ConnectionString, "SELECT (MAX(id_tutor) + 1) FROM Tutores");
                tutor.id_Profesor = (cmbbx_Profesores.SelectedItem as Profesores).id_profesor;
                Grupos grupo = new Grupos();
                grupo.id_tutor = await DBAssets.GetId((App.Current as App).ConnectionString, "SELECT (MAX(id_tutor) + 1) FROM Tutores");
                grupo.id_Profesor = profesores[cmbbx_profesores.SelectedIndex].id_profesor;
                string Query = "INSERT INTO Tutores (id_tutor, id_profesor) VALUES (@id_t, @id_p)";
                var conexion = (App.Current as App).conexionBD;
                SqlCommand cmd = conexion.CreateCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@id_t", tutor.id_tutor);
                cmd.Parameters.AddWithValue("@id_p", tutor.id_Profesor);
                if (await cmd.ExecuteNonQueryAsync() < 0)
                    await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();
                GruposItems.Add(new GruposItem()
                {
                    Id = 
                    Categoria = "Grupos",
                    Grupo = p.grupo.Trim(' '),
                    HeadLine = "Grupo " + p.grupo.Trim(' '),
                    DateLine = p.carrera.Trim(' '),
                    Subhead = p.nombre.Trim(' ') + " " + p.apellidos.Trim(' '),
                    Semestre = p.semestre + " Semestre.",
                    Imagen = "Assets/Antena.png"
                });
            }
            catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
        }

        private void AgregarGruposPopup_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void chkbx_Profesor_Checked(object sender, RoutedEventArgs e)
        {
            cmbbx_Profesores.IsEnabled = true;
            cmbbx_Psicologo.IsEnabled = false;
        }

        private void chkbx_Profesor_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbbx_Profesores.IsEnabled = false;
            cmbbx_Psicologo.IsEnabled = true;
        }
    }
}
