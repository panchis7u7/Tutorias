using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using static Windows.Data.Pdf.PdfDocument;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Sistema_Gestor_de_Tutorias.Controles;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Data.Pdf;
using Windows.Storage.Streams;
using System.Runtime.CompilerServices;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Editor : Page, INotifyPropertyChanged
    {
        private FileOpenPicker picker;
        public Editor()
        {
            this.InitializeComponent();
            //picker = new Windows.Storage.Pickers.FileOpenPicker();
            //picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            //picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            //picker.FileTypeFilter.Add(".jpg");
            //picker.FileTypeFilter.Add(".jpeg");
            //picker.FileTypeFilter.Add(".png");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public Uri Source { get; set; }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //var googleDocsViewer = "http://drive.google.com/viewerng/viewer?embedded=true&url=";
            //var pdf = "http://www.uma.es/precarios/images/pdfs/wd-spectools-word-sample-04.doc";
            //myWebView.Navigate(new Uri(googleDocsViewer + pdf));
        }
        public async void OpenLocal()
        {
            var uri = new Uri("ms-appx:///Assets/pdffile.pdf");

            Source = uri;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Source)));
        }
    }
}