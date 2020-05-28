using System;
using System.Collections.ObjectModel;
using Sistema_Gestor_de_Tutorias.Modelos;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sistema_Gestor_de_Tutorias
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pagina_Consultas : Page
    {
        private int idTutorSeleccionado;
        private InfoAlumnos listItemSeleccionado;
        private InfoAlumnos info_alumnos;
        private ObservableCollection<InfoAlumnos> AlumnosPtr;
        public GruposItem grupo_seleccionado;
        public Pagina_Consultas()
        {
            this.InitializeComponent();
        }

        public async Task<int> GetId (SqlConnection conexion, string Query)
        {
            int id = 0;
            try
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            await reader.ReadAsync();
                            id = reader.GetInt32(0);
                        }
                    }
                }
            return id;
            } catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return 0;
        }
        public async Task<ObservableCollection<InfoAlumnos>> GetAlumnos(SqlConnection conexion, string Query)
        {
            var alumnos = new ObservableCollection<InfoAlumnos>();
            try
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                info_alumnos = new InfoAlumnos();
                                info_alumnos.id_alumno = reader.GetInt32(0);
                                if (!await reader.IsDBNullAsync(1))
                                    info_alumnos.matricula = reader.GetInt32(1);
                                if (!await reader.IsDBNullAsync(2))
                                    info_alumnos.nombre = reader.GetString(2).Trim(' ');
                                if (!await reader.IsDBNullAsync(3))
                                    info_alumnos.apellidos = reader.GetString(3).Trim(' ');
                                if (!await reader.IsDBNullAsync(4))
                                    info_alumnos.semestre = reader.GetInt32(4);
                                if (!await reader.IsDBNullAsync(5))
                                    info_alumnos.carrera = reader.GetString(5).Trim(' ');
                                if (!await reader.IsDBNullAsync(6))
                                    info_alumnos.id_provincia = reader.GetInt32(6);
                                if (!await reader.IsDBNullAsync(7))
                                    info_alumnos.cod_postal = reader.GetInt32(7);
                                if (!await reader.IsDBNullAsync(8))
                                    info_alumnos.provincia= reader.GetString(8).Trim(' ');
                                alumnos.Add(info_alumnos);
                            }
                        }
                    }
                }
            return alumnos;
        }
        catch (Exception eSql)
        {
            Debug.WriteLine("Exception: " + eSql.Message);
        }
        return null;
    }

        private void TextBlock_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            tx.Foreground = new SolidColorBrush(Colors.White);
        }

        private void TextBlock_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            tx.Foreground = new SolidColorBrush(Colors.OrangeRed);
        }

        private async void TextBlock_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            string GetAlumnosOrderByQuery = 
            "SELECT DISTINCT Alumnos.id_alumno, Alumnos.matricula, Alumnos.nombre, Alumnos.apellidos, Alumnos.semestre, Alumnos.carrera, Provincias.id_provincia, Provincias.cod_postal, Provincias.provincia, Tutores.id_tutor FROM Grupos " +
            "INNER JOIN Alumnos ON Alumnos.id_alumno = Grupos.id_alumno " +
            "INNER JOIN Tutores ON Tutores.id_tutor = Grupos.id_tutor " +
            "INNER JOIN Profesores ON Profesores.id_profesor = Tutores.id_profesor " +
            "INNER JOIN ResidenciasAlumnos ON ResidenciasAlumnos.id_alumno = Alumnos.id_alumno " +
            "INNER JOIN Provincias ON Provincias.id_provincia = ResidenciasAlumnos.id_provincia " +
            "AND CONCAT(TRIM(Profesores.nombre),' ', TRIM(Profesores.apellidos)) LIKE('%" + grupo_seleccionado.Subhead + "%') " +
            "ORDER BY " + tx.Name; 
            InventoryList.ItemsSource = await GetAlumnos((App.Current as App).conexionBD, GetAlumnosOrderByQuery);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            grupo_seleccionado = (e.Parameter) as GruposItem;
            string Query =
            "SELECT DISTINCT Alumnos.id_alumno, Alumnos.matricula, Alumnos.nombre, Alumnos.apellidos, Alumnos.semestre, Alumnos.carrera, Provincias.id_provincia, Provincias.cod_postal, Provincias.provincia, Tutores.id_tutor FROM Grupos " +
            "INNER JOIN Alumnos ON Alumnos.id_alumno = Grupos.id_alumno " +
            "INNER JOIN Tutores ON Tutores.id_tutor = Grupos.id_tutor " +
            "INNER JOIN Profesores ON Profesores.id_profesor = Tutores.id_profesor " +
            "INNER JOIN ResidenciasAlumnos ON ResidenciasAlumnos.id_alumno = Alumnos.id_alumno " +
            "INNER JOIN Provincias ON Provincias.id_provincia = ResidenciasAlumnos.id_provincia " +
            "AND CONCAT(TRIM(Profesores.nombre),' ', TRIM(Profesores.apellidos)) LIKE('%" + grupo_seleccionado.Subhead + "%');";
            AlumnosPtr = await GetAlumnos((App.Current as App).conexionBD, Query);
            InventoryList.ItemsSource = AlumnosPtr;
        }

        private void btn_Agregar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            btn_Agregar_Popup.IsEnabled = true;
            btn_Agregar_Popup.Visibility = Visibility.Visible;
            btn_Actualizar.IsEnabled = false;
            btn_Actualizar.Visibility = Visibility.Collapsed;
            btn_Baja.IsEnabled = false;
            btn_Baja.Visibility = Visibility.Collapsed;
            InsertsPopup.IsOpen = true;
        }

        private void InsertsPopup_LayoutUpdated(object sender, object e)
        {
            if (relativeChild.ActualWidth == 0 && relativeChild.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.InsertsPopup.HorizontalOffset;
            double ActualVerticalOffset = this.InsertsPopup.VerticalOffset;

            relativeChild.Height = (int)(Window.Current.Bounds.Height / 2);

            double NewHorizontalOffset = (int) (Window.Current.Bounds.Width - relativeChild.ActualWidth) / 2;
            double NewVerticalOffset = (int) (Window.Current.Bounds.Height - relativeChild.ActualHeight) / 2;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.InsertsPopup.HorizontalOffset = NewHorizontalOffset;
                this.InsertsPopup.VerticalOffset = NewVerticalOffset;
            }
        }
        private async void Alta_Popup(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                var conexion = (App.Current as App).conexionBD;
                string[] Query = {"INSERT INTO Alumnos (id_alumno, matricula, nombre, apellidos, semestre, carrera) VALUES (@id_a, @m, @n, @a, @s, @c)",
                                  "INSERT INTO Provincias (id_provincia, cod_postal, provincia) VALUES (@id_P, @cp, @p)",
                                  "INSERT INTO ResidenciasAlumnos (id_alumno, id_provincia) VALUES (@id_fa, @id_fp)",
                                  "INSERT INTO Grupos (id_alumno, id_tutor, grupo) VALUES (@id_tfa, @id_ft, @g)"};
                
                info_alumnos = new InfoAlumnos();
                info_alumnos.id_alumno = await GetId((App.Current as App).conexionBD, "SELECT (MAX(id_alumno) + 1) FROM Alumnos");
                info_alumnos.matricula = int.Parse(txtbx_matricula.Text);
                info_alumnos.nombre = txtbx_Nombre.Text;
                info_alumnos.apellidos = txtbx_apellidos.Text;
                info_alumnos.semestre = int.Parse(txtbx_semestre.Text);
                info_alumnos.carrera = grupo_seleccionado.DateLine;
                info_alumnos.id_provincia = await GetId((App.Current as App).conexionBD, "SELECT (MAX(id_provincia) + 1) FROM Provincias");
                info_alumnos.cod_postal = int.Parse(txtbx_codigo_postal.Text);
                info_alumnos.provincia = txtbx_provincia.Text;
                //ultimoIdTutores = await GetId((App.Current as App).conexionBD, "SELECT (MAX(id_tutor) + 1) FROM Tutores");
                idTutorSeleccionado = await GetId((App.Current as App).conexionBD, "SELECT DISTINCT id_tutor FROM Tutores " +
                    "INNER JOIN Profesores ON Profesores.id_profesor = Tutores.id_profesor " +
                    "WHERE CONCAT(TRIM(Profesores.nombre), ' ', TRIM(Profesores.apellidos)) LIKE('%" + grupo_seleccionado.Subhead + "%');");

                    SqlCommand cmd = conexion.CreateCommand();
                    cmd.CommandText = Query[0];
                    cmd.Parameters.AddWithValue("@id_a", info_alumnos.id_alumno);
                    cmd.Parameters.AddWithValue("@m", info_alumnos.matricula);
                    cmd.Parameters.AddWithValue("@n", info_alumnos.nombre);
                    cmd.Parameters.AddWithValue("@a", info_alumnos.apellidos);
                    cmd.Parameters.AddWithValue("@s", info_alumnos.semestre);
                    cmd.Parameters.AddWithValue("@c", info_alumnos.carrera);
                    if (await cmd.ExecuteNonQueryAsync() < 0)
                        await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();

                    cmd.CommandText = Query[1];
                    cmd.Parameters.AddWithValue("@id_p", info_alumnos.id_provincia);
                    cmd.Parameters.AddWithValue("@cp", info_alumnos.cod_postal);
                    cmd.Parameters.AddWithValue("@p", info_alumnos.provincia);
                    if (await cmd.ExecuteNonQueryAsync() < 0)
                        await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();

                    cmd.CommandText = Query[2];
                    cmd.Parameters.AddWithValue("@id_fa", info_alumnos.id_alumno);
                    cmd.Parameters.AddWithValue("@id_fp", info_alumnos.id_provincia);
                    if (await cmd.ExecuteNonQueryAsync() < 0)
                        await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();

                    cmd.CommandText = Query[3];
                    cmd.Parameters.AddWithValue("@id_tfa", info_alumnos.id_alumno);
                    cmd.Parameters.AddWithValue("@id_ft", idTutorSeleccionado);
                    cmd.Parameters.AddWithValue("@g", grupo_seleccionado.Grupo);
                    if (await cmd.ExecuteNonQueryAsync() < 0)
                        await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();

                    AlumnosPtr.Add(info_alumnos);
            } catch(Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
        }

        private void InventoryList_ItemClick(object sender, ItemClickEventArgs e)
        {
            //await new MessageDialog("Error!").ShowAsync();
            btn_Agregar_Popup.IsEnabled = false;
            btn_Agregar_Popup.Visibility = Visibility.Collapsed;
            btn_Baja.IsEnabled = true;
            btn_Baja.Visibility = Visibility.Visible;
            btn_Actualizar.IsEnabled = true;
            btn_Actualizar.Visibility = Visibility.Visible;
            listItemSeleccionado = e.ClickedItem as InfoAlumnos;
            txtbx_matricula.Text = listItemSeleccionado.matricula + "";
            txtbx_Nombre.Text = listItemSeleccionado.nombre;
            txtbx_apellidos.Text = listItemSeleccionado.apellidos;
            txtbx_semestre.Text = listItemSeleccionado.semestre + "";
            txtbx_carrera.Text = listItemSeleccionado.carrera;
            txtbx_codigo_postal.Text = listItemSeleccionado.cod_postal + "";
            txtbx_provincia.Text = listItemSeleccionado.provincia;
            InsertsPopup.IsOpen = true;
        }

        private async void btn_Baja_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (listItemSeleccionado != null) {
                try
                {
                    var conexion = (App.Current as App).conexionBD;
                    string[] Query = { "DELETE FROM Alumnos WHERE id_alumno = " + listItemSeleccionado.id_alumno,
                                       "DELETE FROM Provincias WHERE id_provincia = " + listItemSeleccionado.id_provincia,
                                       "DELETE FROM ResidenciasAlumnos WHERE id_alumno = " + listItemSeleccionado.id_alumno,
                                       "DELETE FROM Grupos WHERE id_alumno = " + listItemSeleccionado.id_alumno};
                    SqlCommand cmd = conexion.CreateCommand();
                    cmd.CommandText = Query[0];
                    if (await cmd.ExecuteNonQueryAsync() < 0)
                        await new MessageDialog("Error borrando la fila de la base de datos!").ShowAsync();
                    cmd.CommandText = Query[1];
                    if (await cmd.ExecuteNonQueryAsync() < 0)
                        await new MessageDialog("Error borrando la fila de la base de datos!").ShowAsync();
                    cmd.CommandText = Query[2];
                    if (await cmd.ExecuteNonQueryAsync() < 0)
                        await new MessageDialog("Error borrando la fila de la base de datos!").ShowAsync();
                    cmd.CommandText = Query[3];
                    if (await cmd.ExecuteNonQueryAsync() < 0)
                        await new MessageDialog("Error borrando la fila de la base de datos!").ShowAsync();
                    foreach(var alumno in AlumnosPtr)
                    {
                        if(alumno.id_alumno == listItemSeleccionado.id_alumno)
                        {
                            AlumnosPtr.Remove(alumno);
                            break;
                        }
                    }
                } catch (Exception eSql)
                {
                    await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
                }
            }
        }

        private async void btn_Actualizar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (listItemSeleccionado != null)
            {
                try
                {
                    var conexion = (App.Current as App).conexionBD;
                    string[] Query = {"UPDATE Alumnos SET matricula = " + txtbx_matricula.Text + ", nombre = '" + txtbx_Nombre.Text + "', apellidos = '" + txtbx_apellidos.Text
                                   + "', semestre = " + txtbx_semestre.Text + ", carrera = '" + txtbx_carrera.Text + "' WHERE id_alumno = " + listItemSeleccionado.id_alumno,
                                     "UPDATE Provincias SET cod_postal = " + txtbx_codigo_postal.Text + ", provincia = '" + txtbx_provincia.Text +
                                     "' WHERE id_provincia = " + listItemSeleccionado.id_provincia};
                    SqlCommand cmd = conexion.CreateCommand();
                    cmd.CommandText = Query[0];
                    if (await cmd.ExecuteNonQueryAsync() < 0)
                        await new MessageDialog("Error actualizando la fila de la base de datos!").ShowAsync();
                    cmd.CommandText = Query[1];
                    if (await cmd.ExecuteNonQueryAsync() < 0)
                        await new MessageDialog("Error actualizando la fila de la base de datos!").ShowAsync();
                    foreach(var alumno in AlumnosPtr)
                    {
                        if (alumno.id_alumno == listItemSeleccionado.id_alumno)
                        {
                            alumno.matricula = int.Parse(txtbx_matricula.Text);
                            alumno.nombre = txtbx_Nombre.Text;
                            alumno.apellidos = txtbx_apellidos.Text;
                            alumno.semestre = int.Parse(txtbx_semestre.Text);
                            alumno.carrera = txtbx_carrera.Text;
                            alumno.cod_postal = int.Parse(txtbx_codigo_postal.Text);
                            alumno.provincia = txtbx_provincia.Text;
                            break;
                        }
                    }
                } catch (Exception eSql)
                {
                    await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
                }
            }
        }
    }
}