﻿using Sistema_Gestor_de_Tutorias.Modelos;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using System;
using Windows.UI.Core;
using Sistema_Gestor_de_Tutorias.Database_Assets;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using System.Collections.Generic;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pagina_Grupos : Page
    {
        private ObservableCollection<GruposItem> GruposItems;
        private List<InfoProfesores> profesores;
        private ObservableCollection<Psicologos> psicologos;

        public InfoGruposTutor InfoGruposTutores;
        public NavigationView navigationView;
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
            this.navigationView = e.Parameter as NavigationView;
            GruposFactory.GetGrupos("Grupos", GruposItems);
            profesores = await DBAssets.getProfesoresAsync((App.Current as App).ConnectionString);
            psicologos = await DBAssets.getPsicologosAsync((App.Current as App).ConnectionString);
            cmbbx_carrera.ItemsSource = await DBAssets.getStringsAsync((App.Current as App).ConnectionString, "SELECT DISTINCT carrera FROM Tutores");
            cmbbx_Profesores.ItemsSource = profesores;
            cmbbx_Psicologo.ItemsSource = psicologos;
        }

            private async void GruposGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            var myView = CoreApplication.CreateNewView();
            int newViewId = 0;
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

            double NewHorizontalOffset;
            if (navigationView.IsPaneOpen)
                NewHorizontalOffset = (int)((Window.Current.Bounds.Width - relativeChild.ActualWidth) / 2) - 250;
            else
                NewHorizontalOffset = (int)((Window.Current.Bounds.Width - relativeChild.ActualWidth) / 2) - 30;

            double NewVerticalOffset = (int)((Window.Current.Bounds.Height - relativeChild.ActualHeight) / 2) - 30;

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
                tutor.id_Profesor = (cmbbx_Profesores.SelectedItem as InfoProfesores).id_profesor;
                tutor.grupo = txtbx_Grupo.Text;
                tutor.carrera = cmbbx_carrera.SelectedItem.ToString();
                tutor.semestre = int.Parse(txtbx_Semestre.Text);

                Grupos grupo = new Grupos();
                grupo.id_tutor = tutor.id_tutor;
                grupo.id_alumno = 0;
                
                if (await DBAssets.SetTutor((App.Current as App).ConnectionString, tutor) >= 0)
                if (await DBAssets.SetGrupo((App.Current as App).ConnectionString, grupo) >= 0)
                {

                    GruposItems.Add(new GruposItem()
                    {
                        Id = tutor.id_tutor,
                        Categoria = "Grupos",
                        Grupo = tutor.grupo.Trim(' '),
                        HeadLine = "Grupo " + tutor.grupo.Trim(' '),
                        DateLine = tutor.carrera.Trim(' '),
                        Subhead = (cmbbx_Profesores.SelectedItem as InfoProfesores).nombre + " " + (cmbbx_Profesores.SelectedItem as InfoProfesores).apellidos,
                        Semestre = tutor.semestre + " Semestre.",
                        Imagen = "Assets/Antena.png"
                    });
                } else
                    {
                        await new MessageDialog("Error al crear grupo!").ShowAsync();
                    }
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
            cmbbx_Profesores.IsEnabled = false;
            cmbbx_Psicologo.IsEnabled = true;
        }

        private void chkbx_Profesor_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbbx_Profesores.IsEnabled = true;
            cmbbx_Psicologo.IsEnabled = false;
        }
    }
}
