using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Editor : Page
    {
        private FileOpenPicker picker;
        public Editor()
        {
            //this.InitializeComponent();
            //picker = new Windows.Storage.Pickers.FileOpenPicker();
            //picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            //picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            //picker.FileTypeFilter.Add(".jpg");
            //picker.FileTypeFilter.Add(".jpeg");
            //picker.FileTypeFilter.Add(".png");
        }

        private async void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
        }
    }
}