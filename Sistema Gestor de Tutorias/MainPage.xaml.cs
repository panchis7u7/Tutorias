﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Pagina_Configuracion config_page;

        public MainPage()
        {
            this.InitializeComponent();
            config_page = new Pagina_Configuracion(ref mainPageRef, ref navMenu);
        }

        private void navMenu_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
           
        }
        
        private void navMenu_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                main_frame.Content = config_page;
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;
                switch (item.Tag.ToString())
                {
                    case "1":
                        main_frame.Navigate(typeof(Pagina_Configuracion));
                        break;
                    case "2":
                        main_frame.Navigate(typeof(Pagina_Consultas));
                        break;
                    case "3":
                        main_frame.Navigate(typeof(Pagina_Configuracion));
                        break;
                    default:
                        break;
                }
            }
        }

        private void navMenu_Loaded(object sender, RoutedEventArgs e)
        {
            var navView = sender as NavigationView;
            var rootGrid = VisualTreeHelper.GetChild(navView, 0) as Grid;

            // SDK 18362 (1903)
            // SDK 17763 (1809)
            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7))
            {
                // Find the back button.
                var paneToggleButtonGrid = VisualTreeHelper.GetChild(rootGrid, 0) as Grid;
                var buttonHolderGrid = VisualTreeHelper.GetChild(paneToggleButtonGrid, 1) as Grid;
                var navigationViewBackButton = VisualTreeHelper.GetChild(buttonHolderGrid, 0) as Button;
                navigationViewBackButton.AccessKey = "A";
                //SolidColorBrush back_black = new SolidColorBrush();
                //back_black.Color = Color.FromArgb(255, 0, 0, 0);
                //navigationViewBackButton.Foreground = back_black;
            }
        }
    }
}