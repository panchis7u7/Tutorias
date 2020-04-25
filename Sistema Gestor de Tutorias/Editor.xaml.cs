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
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Data.Pdf;
using Windows.Storage.Streams;
using System.Runtime.CompilerServices;
using Sistema_Gestor_de_Tutorias.Modelos;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Forms.Xfdf;
using System.Text;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Editor : Page, INotifyPropertyChanged
    {
        private Formato formato_seleccionado;
        public Editor()
        {
            this.InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public Uri Source { get; set; }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            formato_seleccionado = (e.Parameter) as Formato;
            string sfilePath = "Formatos/" + formato_seleccionado.formato_id + ".pdf";
            var uri = new Uri("ms-appx:///Formatos/" + formato_seleccionado.formato_id + ".pdf");
            Source = uri;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Source)));
            try
            {
                PdfReader reader = new PdfReader(sfilePath);
                iText.Kernel.Pdf.PdfDocument pdf = new iText.Kernel.Pdf.PdfDocument(reader);
                string texto = string.Empty;
                for (int page = 1; page <= pdf.GetNumberOfPages(); page++)
                {
                    ITextExtractionStrategy its = new LocationTextExtractionStrategy();
                    String s = PdfTextExtractor.GetTextFromPage(pdf.GetPage(page), its);
                    //s = System.Text.Encoding.UTF8.GetString(ASCIIEncoding.Convert(System.Text.Encoding.Default, System.Text.Encoding.UTF8, System.Text.Encoding.Default.GetBytes(s)));
                    texto = texto + s;
                }
                int cuenta = char_counter(ref texto);
                reader.Close();
            } catch (Exception Ex)
            {
                var err = new MessageDialog("Unable to open File!");
                await err.ShowAsync();
            }
        }

        private int char_counter(ref string sInput)
        {
            int nCount = 0;
            for (int i = 0; i < sInput.Length; i++)
            {
                if (sInput[i] == '<')
                    nCount+=1;
            }
            return nCount;
        }
    }
}