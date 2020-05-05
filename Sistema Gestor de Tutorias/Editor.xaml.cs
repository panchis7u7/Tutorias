using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime; 
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;
using System.Threading.Tasks;
using Sistema_Gestor_de_Tutorias.Modelos;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using Windows.UI.Popups;
using iText.Layout;
using Syncfusion.DocIO;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;

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
        private List<ComboBox> comboBoxes;
        private List<CheckBox> checkBoxes;
        private List<string> textAreas;
        private GridView checkb_grid;
        private GridView combob_grid;
        
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            formato_seleccionado = (e.Parameter) as Formato;
            string sFilePath = "Formatos/" + formato_seleccionado.formato_id + ".pdf";
            var uri = new Uri("ms-appx:///Formatos/" + formato_seleccionado.formato_id + ".pdf");
            Source = uri;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Source)));

            checkBoxes = new List<CheckBox>();
            comboBoxes = new List<ComboBox>();

            #region CheckBox_GridView
            checkb_grid = new GridView();
            checkb_grid.Width = double.NaN;
            checkb_grid.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            checkb_grid.HorizontalContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            #endregion

            #region ComboBox_GridView
            combob_grid = new GridView();
            combob_grid.Width = double.NaN;
            combob_grid.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            combob_grid.HorizontalContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            #endregion

            string texto = await pdfTextExtract(sFilePath);
            //int contador = charCounter(ref texto);
            textAreas = GetWordBasedOn(ref texto, "<[", "]>");

            int k = 0; int j = 0;
            for (int i = 0; i < textAreas.Count; i++)
            {
                if (textAreas[i].Length <= 1)
                {
                    checkBoxes.Add(new CheckBox());
                    checkBoxes[j].Width = double.NaN;
                    checkBoxes[j].Height = double.NaN;
                    checkBoxes[j].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                    checkBoxes[j].HorizontalContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                    checkBoxes[j].Margin = new Thickness(10,0,0,0);
                    checkBoxes[j].FlowDirection = FlowDirection.LeftToRight;
                    checkBoxes[j].Content = textAreas[i] + ")";
                    checkb_grid.Items.Add(checkBoxes[j]);
                    j += 1;
                }
                else
                {
                    switch (textAreas[i])
                    {
                        case "No Oficio":
                            TextBox oficio = new TextBox();
                            oficio.Text = "1";
                            oficio.Margin = new Thickness(10, 3, 10, 3);
                            oficio.Height = double.NaN;
                            oficio.Width = 200;
                            oficio.Header = textAreas[i];
                            oficio.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                            oficio.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                            combob_grid.Items.Add(oficio);
                            break;
                        case "Periodo":

                            break;
                        default:
                            comboBoxes.Add(new ComboBox());
                            comboBoxes[k].Width = 200;
                            comboBoxes[k].Margin = new Thickness(10, 3, 10, 3);
                            comboBoxes[k].Height = double.NaN;
                            comboBoxes[k].Header = textAreas[i];
                            comboBoxes[k].PlaceholderText = "Seleccione un item";
                            comboBoxes[k].VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                            comboBoxes[k].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                            comboBoxes[k].Text = textAreas[i];
                            switch (comboBoxes[k].Text)
                            {
                                case "Nombre Docente":
                                    var resultados = new ObservableCollection<Profesores>();
                                    try
                                    {

                                        if ((App.Current as App).conexionBD.State == System.Data.ConnectionState.Open)
                                        {
                                            String Query = "SELECT * FROM Profesores";
                                            using (SqlCommand cmd = (App.Current as App).conexionBD.CreateCommand())
                                            {
                                                cmd.CommandText = Query;
                                                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                                                {
                                                    while (await reader.ReadAsync())
                                                    {
                                                        Profesores p = new Profesores();
                                                        p.id_profesor = reader.GetInt32(0);
                                                        if (!await reader.IsDBNullAsync(1))
                                                            p.nombre = reader.GetString(1);
                                                        if (!await reader.IsDBNullAsync(2))
                                                            p.apellidos = reader.GetString(2);
                                                        if (!await reader.IsDBNullAsync(3))
                                                            p.departamento = reader.GetString(3);
                                                        resultados.Add(p);
                                                        comboBoxes[k].Items.Add(p.nombre.Trim(' ') + " " + p.apellidos.Trim(' '));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception eSql)
                                    {
                                        Debug.WriteLine("Resultados: " + eSql.Message);
                                    }
                                    break;

                            }
                            combob_grid.Items.Add(comboBoxes[k]);
                            k += 1;
                            break;
                    }
                }
            }
            primary.Children.Add(combob_grid);
            primary.Children.Add(checkb_grid);
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
                var err = new MessageDialog("Unable to open File!: " + Ex.Message);
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