using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime; 
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Sistema_Gestor_de_Tutorias.Modelos;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using Windows.UI.Popups;
using iText.Layout;
using Syncfusion.DocIO;
using System.Data.SqlClient;
using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.ComponentModel;
using Sistema_Gestor_de_Tutorias.Database_Assets;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Editor : Page, INotifyPropertyChanged
    {
        private Formato formato_seleccionado;

        public Uri Source { get; set; }

        private WordDocument document;
        private Stream docStream;
        private string sFilePathWord;

        private Button guardarBtn;

        private List<DatePicker> datePickers;
        private List<TextBox> textBoxes;
        private List<ComboBox> comboBoxes;
        private List<CheckBox> checkBoxes;
        private List<RichEditBox> richEditBoxes;
        private List<CalendarDatePicker> calendarDatePickers;
        private List<string> textAreas;

        private GridView checkb_grid;
        private GridView combob_grid;
        private StackPanel textbox_sp;

        public event PropertyChangedEventHandler PropertyChanged;

        public Editor()
        {
            this.InitializeComponent();
            datePickers = new List<DatePicker>();
            textBoxes = new List<TextBox>();
            checkBoxes = new List<CheckBox>();
            comboBoxes = new List<ComboBox>();
            richEditBoxes = new List<RichEditBox>();
            calendarDatePickers = new List<CalendarDatePicker>();
            guardarBtn = new Button() { Width = 100, Height = 40, Content = "Guardar"};
            guardarBtn.Tapped += btnGuardar_Tapped;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            formato_seleccionado = (e.Parameter) as Formato;
            string sFilePath = "Formatos/" + formato_seleccionado.formato_id + ".pdf";
            sFilePathWord = "Formatos/" + formato_seleccionado.formato_id + ".docx";
            var uri = new Uri("ms-appx:///Formatos/" + formato_seleccionado.formato_id + ".pdf");

            Source = uri;

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

            textbox_sp = new StackPanel();
            textbox_sp.Width = double.NaN;

            string texto = await pdfTextExtract(sFilePath);
            //int contador = charCounter(ref texto);
            textAreas = GetWordBasedOn(ref texto, "<[", "]>");
            comboBoxes[0].Items.Clear();
            comboBoxes[1].Items.Clear();
            comboBoxes[2].Items.Clear();
            comboBoxes[0].SelectionChanged += async (sender, x) => {
                comboBoxes[1].ItemsSource = await DBAssets.getGruposOnCarreraAsync((App.Current as App).ConnectionString, comboBoxes[0].SelectedItem.ToString());
            };
            comboBoxes[1].SelectionChanged += async (sender, x) => {
                comboBoxes[2].ItemsSource = await DBAssets.GetAlumnosBasedOnGrupoAsync((App.Current as App).ConnectionString, 
                    (comboBoxes[1].SelectedItem as InfoGruposGS).grupo, (comboBoxes[1].SelectedItem as InfoGruposGS).semestre);
            };
            for (int i = 0; i < textAreas.Count; i++)
            {
                if (textAreas[i].Length <= 1)
                {
                    CheckBox incisos = new CheckBox();
                    incisos.Name = textAreas[i];
                    incisos.Width = double.NaN;
                    incisos.Height = double.NaN;
                    incisos.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                    incisos.HorizontalContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                    incisos.Margin = new Thickness(10, 3, 10, 3);
                    incisos.FlowDirection = FlowDirection.LeftToRight;
                    incisos.Content = textAreas[i] + ")";
                    incisos.Checked += (sender, args) => {
                        if (incisos.IsChecked == true)
                            replace(incisos.Name, "x", sFilePathWord);
                        else
                            replace(incisos.Name, " ", sFilePathWord);
                    };
                    checkBoxes.Add(incisos);
                    checkb_grid.Items.Add(incisos);
                }
                else
                {
                    if (textAreas[i].ToLower().Contains("no oficio") || textAreas[i].ToLower().Contains("no semestre"))
                    {
                        TextBox oficio = new TextBox();
                        textBoxes.Add(oficio);
                        oficio.Name = textAreas[i].Replace(' ', '_');
                        oficio.IsReadOnly = false;
                        oficio.Text = "1";
                        oficio.Margin = new Thickness(10, 3, 10, 3);
                        oficio.Height = double.NaN;
                        oficio.Width = 200;
                        oficio.Header = textAreas[i];
                        oficio.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                        oficio.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                        combob_grid.Items.Add(oficio);
                    }
                    else if (textAreas[i].ToLower().Contains("año") || textAreas[i].ToLower().Contains("periodo"))
                    {
                        DatePicker fecha = new DatePicker();
                        if (textAreas[i].ToLower().Contains("año")) {
                            fecha.DayVisible = false;
                            fecha.MonthVisible = false;
                        } else {
                            fecha.DayVisible = false;
                            fecha.YearVisible = false;
                        }
                        fecha.Name = textAreas[i].Replace(' ', '_');
                        fecha.CharacterSpacing = 0;
                        fecha.Header = textAreas[i];
                        fecha.Margin = new Thickness(10, 3, 10, 3);
                        fecha.Height = double.NaN;
                        fecha.HorizontalContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                        datePickers.Add(fecha);
                        combob_grid.Items.Add(fecha);
                    }
                    else if (textAreas[i].ToLower().Contains("fecha"))
                    {
                        CalendarDatePicker dp = new CalendarDatePicker();
                        dp.Name = textAreas[i].Replace(' ', '_');
                        dp.Header = textAreas[i];
                        dp.Height = double.NaN;
                        dp.Margin = new Thickness(10, 3, 10, 3);
                        dp.Width = 200;
                        dp.PlaceholderText = "Seleccione la fecha";
                        dp.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                        dp.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                        calendarDatePickers.Add(dp);
                        combob_grid.Items.Add(dp);
                    }
                    else if (textAreas[i].ToLower().Contains("desarrollo") || textAreas[i].ToLower().Contains("asunto") || textAreas[i].ToLower().Contains("cargo")) {
                        RichEditBox info = new RichEditBox();
                        info.Name = textAreas[i].Replace(' ', '_');
                        info.Width = double.NaN;
                        info.Height = 100;
                        info.Header = textAreas[i];
                        info.Margin = new Thickness(10, 3, 10, 3);
                        richEditBoxes.Add(info);
                        textbox_sp.Children.Add(info);
                    }
                    else if (textAreas[i].ToLower().ToLower().Contains("jefe"))
                    {
                        //if (textAreas[i].ToLower().Contains("Jefe de Tutorias"));
                        //replace(Pagina_Configuracion.);
                    }
                    else if (textAreas[i] != "Carrera") {
                        ComboBox desplegables = new ComboBox();
                        desplegables.Name = textAreas[i].Replace(' ', '_');
                        desplegables.Width = 200;
                        desplegables.Margin = new Thickness(10, 3, 10, 3);
                        desplegables.Height = double.NaN;
                        desplegables.Header = textAreas[i];
                        desplegables.PlaceholderText = "Seleccione un item";
                        desplegables.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                        desplegables.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                        desplegables.Text = textAreas[i];
                        comboBoxes.Add(desplegables);
                        switch (desplegables.Name)
                        {
                            case "Nombre_Docente":
                                desplegables.ItemsSource = await DBAssets.getProfesoresAsync((App.Current as App).ConnectionString);
                                break;

                            case "Ciudad_Estado":
                                CiudadesEstados.getCiudadesEstados().ForEach(p => desplegables.Items.Add(p.Capital +", "+ p.Estado));
                                break;

                            case "Nombre_Tutor":
                                desplegables.ItemsSource = await DBAssets.getNombreTutoresAsync((App.Current as App).ConnectionString);
                                break;
                        }
                        combob_grid.Items.Add(desplegables);
                    } else
                    {
                        comboBoxes[0].ItemsSource = await DBAssets.getCarrerasAsync((App.Current as App).ConnectionString);
                    }
                }
            }
            primary.Children.Add(combob_grid);
            primary.Children.Add(checkb_grid);
            primary.Children.Add(textbox_sp);
            primary.Children.Add(guardarBtn);
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
                await new MessageDialog("Error al abrir archivo: " + Ex.Message).ShowAsync();
                return null;
            }
            return texto;
        }

        private List<string> GetWordBasedOn(ref string sInput, string sTarget1, string sTarget2)
        {
            List<string> palabras = new List<string>();
            try
            {
                string[] text = sInput.Split(' ');
                palabras.Add("Carrera");
                comboBoxes.Add(new ComboBox() { Name = "Carrera", Width = 200, Margin = new Thickness(10, 3, 10, 3), Header = "Carrera", PlaceholderText = "Seleccione un item" });
                comboBoxes.Add(new ComboBox() { Name = "Grupo", Width = 200, Margin = new Thickness(10, 3, 10, 3), Header = "Grupo", PlaceholderText = "Seleccione un item" });
                comboBoxes.Add(new ComboBox() { Name = "Alumno", Width = 200, Margin = new Thickness(10, 3, 10, 3), Header = "Alumnos", PlaceholderText = "Seleccione un item" });
                combob_grid.Items.Add(comboBoxes[0]);
                combob_grid.Items.Add(comboBoxes[1]);
                combob_grid.Items.Add(comboBoxes[2]);
                foreach (var word in text)
                {
                    if ((word.Contains(sTarget1) && word.Contains(sTarget2)) && !(word.ToLower().Contains("carrera") ||
                        word.ToLower().Contains("grupo") || 
                        word.ToLower().Contains("alumno")))
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
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
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

        private async void replace(string textToBeReplaced, string text, string formato)
        {
            try
            {
                document.Replace("<[" + textToBeReplaced + "]>", text, true, true);
            }
            catch (Exception ex)
            {
                var err = new MessageDialog("Unable to open File! " + ex.Message);
                await err.ShowAsync();
            }
        }

        private async void btnGuardar_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            try
            {
                if (document != null)
                {
                    document.Close();
                    document = null;
                }
                if (document == null)
                {
                    document = new WordDocument();
                    docStream = File.OpenRead(Path.GetFullPath(sFilePathWord));
                    await document.OpenAsync(docStream, FormatType.Docx);
                    docStream.Dispose();
                }
                foreach (var x in textBoxes)
                {
                    replace(x.Name, x.Text, sFilePathWord);
                }
                foreach (var z in richEditBoxes)
                {
                    string valor;
                    z.TextDocument.GetText(Windows.UI.Text.TextGetOptions.None, out valor);
                    replace(z.Name, valor, sFilePathWord);
                }
                foreach (var x in comboBoxes)
                {
                    if (x.SelectedItem == null)
                        replace(x.Name, "", sFilePathWord);
                    else
                        replace(x.Name, x.SelectedItem.ToString(), sFilePathWord);
                }
                foreach (var fecha in datePickers)
                {
                    if (fecha.SelectedDate == null)
                        fecha.SelectedDate = DateTime.UtcNow;
                    if (fecha.Name.ToLower().Contains("año"))
                    {
                        replace(fecha.Name, fecha.Date.DateTime.ToString("yyyy"), sFilePathWord);
                    } else
                    {
                        replace(fecha.Name, fecha.Date.DateTime.ToString("MMMM"), sFilePathWord);
                    }
                }
                foreach (var dp in calendarDatePickers) 
                {
                    if (dp.Date.Value.DateTime == null)
                        dp.Date = DateTime.UtcNow;
                    replace(dp.Name, dp.Date.Value.DateTime.ToString("dd/MMMM/yyyy"), sFilePathWord);
                }
                replace("Jefe_Tutorias", await getJefes((App.Current as App).conexionBD, "SELECT nombre FROM CoordinadoresTutorias;"), sFilePathWord);
                replace("Jefe_Departamento", await getJefes((App.Current as App).conexionBD, "SELECT nombre FROM JefesDepartamentos;"), sFilePathWord);
                replace("Coordinador_Tutorias_Institucionales", await getJefes((App.Current as App).conexionBD, "SELECT nombre FROM CoordinadoresTutoriasInstitucionales;"), sFilePathWord);

                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.SuggestedFileName = "Resultado";
                savePicker.FileTypeChoices.Add("Word Documents", new List<string>() { ".docx" });
                StorageFile outputStorageFile = await savePicker.PickSaveFileAsync();
                await document.SaveAsync(outputStorageFile, FormatType.Docx);
            }
            catch (Exception ex)
            {
                var err = new MessageDialog("Unable to open File!" + ex.Message);
                await err.ShowAsync();
            }
        }

        private async Task<string> getJefes(SqlConnection conexion, string Query)
        {
            try
            {
                string nombre = string.Empty;
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            await reader.ReadAsync();
                            nombre = reader.GetString(0).Trim(' ');
                        }
                    }
                }
                return nombre;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine(eSql.Message);
            }
            return string.Empty;
        }
    }
}