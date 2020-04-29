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
using iText.Layout;
using System.Reflection;
using Syncfusion.DocIO;

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

        private List<TextBox> textBoxes;
        private List<TextBlock> textBlocks;
        private List<string> textAreas;
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            formato_seleccionado = (e.Parameter) as Formato;
            string sFilePath = "Formatos/" + formato_seleccionado.formato_id + ".pdf";
            var uri = new Uri("ms-appx:///Formatos/" + formato_seleccionado.formato_id + ".pdf");
            Source = uri;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Source)));
            textBlocks = new List<TextBlock>();
            textBoxes = new List<TextBox>();
            string texto = await pdfTextExtract(sFilePath);
            int contador = charCounter(ref texto);
            textAreas = GetWordBasedOn(ref texto, "<[", "]>");
            for (int i = 0; i < contador; i++)
            {
                textBlocks.Add(new TextBlock());
                textBoxes.Add(new TextBox());
                textBlocks[i].Width = 300;
                textBoxes[i].Width = 300;
                textBlocks[i].Height = 30;
                textBoxes[i].Height = 40;
                textBlocks[i].Margin = new Thickness(0, 0, 0, 0);
                textBoxes[i].Margin = new Thickness(0, 0, 0, 10);
                textBlocks[i].Text = textAreas[i];
                textBoxes[i].Text = "Hola a todos";
                textBlocks[i].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                textBoxes[i].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                textBlocks[i].VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom;
                textBoxes[i].VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                primary.Children.Add(textBlocks[i]);
                primary.Children.Add(textBoxes[i]);
            }
        }

        private async Task<string> pdfTextExtract(string sFilePath)
        {
            string texto;
            try
            {
                PdfReader reader = new PdfReader(sFilePath);
                iText.Kernel.Pdf.PdfDocument pdf = new iText.Kernel.Pdf.PdfDocument(reader);
                texto = string.Empty;
                for (int page = 1; page <= pdf.GetNumberOfPages(); page++)
                {
                    ITextExtractionStrategy its = new LocationTextExtractionStrategy();
                    String s = PdfTextExtractor.GetTextFromPage(pdf.GetPage(page), its);
                    //s = System.Text.Encoding.UTF8.GetString(ASCIIEncoding.Convert(System.Text.Encoding.Default, System.Text.Encoding.UTF8, System.Text.Encoding.Default.GetBytes(s)));
                    texto = texto + s;
                }
                reader.Close();
            }
            catch (Exception Ex)
            {
                var err = new MessageDialog("Unable to open File!");
                await err.ShowAsync();
                return null;
            }
            return texto;
        }

        private List<string> GetWordBasedOn(ref string sInput, string sTarget1, string sTarget2)
        {
            List<string> palabras = new List<string>();
            string[] text = sInput.Split(' ');
            foreach (var word in text)
            {
                if (word.Contains(sTarget1) && word.Contains(sTarget2))
                {
                    string result = word.Replace("_", " ");
                    result = new string((from c in result
                                         where char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)
                                         select c).ToArray());
                    result = result.Replace("\n", "").Replace("\r", "");
                    palabras.Add(result);
                }
            }
            return palabras;
        }

        private int charCounter(ref string sInput)
        {
            int nCount = 0;
            for (int i = 0; i < sInput.Length; i++)
            {
                if (sInput[i] == '<' && sInput[i+1] == '[')
                    nCount+=1;
            }
            return nCount;
        }

        private int wordCounter(ref string sInput, string sSearchTerm)
        {
            //Convert the string into an array of words  
            string[] source = sInput.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
            // Create the query.  Use ToLowerInvariant to match "data" and "Data"
            var matchQuery = from word in source
                             where word.ToLowerInvariant() == sSearchTerm.ToLowerInvariant()
                             select word;
            // Count the matches, which executes the query.  
            int wordCount = matchQuery.Count();
            return wordCount;
        }

        private async void replace()
        {
            using (WordDocument document = new WordDocument())
            {
                try
                {
                    Stream docStream = File.OpenRead(Path.GetFullPath(@"Formatos/2.docx"));
                    await document.OpenAsync(docStream, FormatType.Docx);
                    docStream.Dispose();
                    //Finds all occurrences of a word and replaces with properly spelled word.
                    //document.Replace("Cyles", "Cycles", true, true);
                    ////Saves the resultant file in the given path.
                    //docStream = File.Create(Path.GetFullPath(@"Formatos/resultado.docx"));
                    //await document.SaveAsync(docStream, FormatType.Docx);
                    //docStream.Dispose();
                }
                catch (Exception ex)
                {
                    var err = new MessageDialog("Unable to open File!");
                    await err.ShowAsync();
                }
            }
        }
    }
}