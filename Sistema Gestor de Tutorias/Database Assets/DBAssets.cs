using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Sistema_Gestor_de_Tutorias.Database_Assets
{
    public class DBAssets
    {
        public SqlConnection conexion { get; private set; }
        public DBAssets(SqlConnection conexion)
        {
            this.conexion = conexion;
        }
        public async static Task<List<InfoProfesores>> getProfesoresAsync(string connectionString)
        {
            try
            {
                List<InfoProfesores> profesores = new List<InfoProfesores>();
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        //String Query = "SELECT * FROM Profesores";
                        String Query = "SELECT Profesores.id_profesor, nombre, apellidos, departamento, correo, imagen, Provincias.id_provincia, Provincias.cod_postal, Provincias.provincia FROM Profesores " +
                                       "INNER JOIN ResidenciasProfesores ON ResidenciasProfesores.id_profesor = Profesores.id_profesor " +
                                       "INNER JOIN Provincias ON provinciAS.id_provincia = ResidenciasProfesores.id_provincia; "; 
                        using (SqlCommand cmd = conexion.CreateCommand())
                        {
                            cmd.CommandText = Query;
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    InfoProfesores p = new InfoProfesores();
                                    p.id_profesor = reader.GetInt32(0);
                                    if (!await reader.IsDBNullAsync(1))
                                        p.nombre = reader.GetString(1).Trim(' ');
                                    if (!await reader.IsDBNullAsync(2))
                                        p.apellidos = reader.GetString(2).Trim(' ');
                                    if (!await reader.IsDBNullAsync(3))
                                        p.departamento = reader.GetString(3).Trim(' ');
                                    if (!await reader.IsDBNullAsync(4))
                                        p.correo = reader.GetString(4).Trim(' ');
                                    if (!await reader.IsDBNullAsync(5))
                                        p.imagen = reader.GetStream(5);
                                    if (!await reader.IsDBNullAsync(6))
                                        p.id_provincia = reader.GetInt32(6);
                                    if (!await reader.IsDBNullAsync(7))
                                        p.cod_postal = reader.GetInt32(7);
                                    if (!await reader.IsDBNullAsync(8))
                                        p.provincia = reader.GetString(8).Trim(' ');
                                    profesores.Add(p);
                                }
                            }
                        }
                    }
                }
                return profesores;
            } catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
            return null;
        }

        public async static Task<List<TutoresProfesores>> getProfesoresTutoresAsync(string connectionString)
        {
            try
            {
                List<TutoresProfesores> profesores = new List<TutoresProfesores>();
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        //String Query = "SELECT * FROM Profesores";
                        String Query = "SELECT Profesores.id_profesor, Tutores.id_tutor, nombre, apellidos, departamento, Tutores.grupo, Tutores.semestre, correo, imagen FROM Profesores " +
                                       "INNER JOIN Tutores ON Tutores.id_profesor = Profesores.id_profesor";
                        using (SqlCommand cmd = conexion.CreateCommand())
                        {
                            cmd.CommandText = Query;
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    TutoresProfesores p = new TutoresProfesores();
                                    p.id_profesor = reader.GetInt32(0);
                                    if (!await reader.IsDBNullAsync(1))
                                        p.id_tutor = reader.GetInt32(1);
                                    if (!await reader.IsDBNullAsync(2))
                                        p.nombre = reader.GetString(2).Trim(' ');
                                    if (!await reader.IsDBNullAsync(3))
                                        p.apellidos = reader.GetString(3).Trim(' ');
                                    if (!await reader.IsDBNullAsync(4))
                                        p.departamento = reader.GetString(4).Trim(' ');
                                    if (!await reader.IsDBNullAsync(5))
                                        p.grupo = reader.GetString(5).Trim(' ');
                                    if (!await reader.IsDBNullAsync(6))
                                        p.semestre = reader.GetInt32(6);
                                    if (!await reader.IsDBNullAsync(7))
                                        p.correo = reader.GetString(7).Trim(' ');
                                    if (!await reader.IsDBNullAsync(8))
                                        p.imagen = reader.GetStream(8);
                                    profesores.Add(p);
                                }
                            }
                        }
                    }
                }
                return profesores;
            }
            catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
            return null;
        }

        public async static Task<ObservableCollection<Alumnos>> GetAlumnosBasedOnGrupoAsync (string connectionString, string grupo, int semestre)
        {
            try
            {
                ObservableCollection<Alumnos> alumnos = new ObservableCollection<Alumnos>();
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        String Query = "SELECT Alumnos.id_alumno, Alumnos.matricula, Alumnos.nombre, Alumnos.apellidos FROM Grupos " +
                                       "INNER JOIN Alumnos ON Alumnos.id_alumno = Grupos.id_alumno " +
                                       "INNER JOIN Tutores ON Tutores.id_tutor = Grupos.id_tutor " +
                                       "WHERE Tutores.grupo = '" + grupo + "' AND Tutores.semestre = " + semestre + "; ";
                        using (SqlCommand cmd = conexion.CreateCommand())
                        {
                            cmd.CommandText = Query;
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    Alumnos alumno = new Alumnos();
                                    alumno.id_alumno = reader.GetInt32(0);
                                    if (!await reader.IsDBNullAsync(1))
                                        alumno.matricula = reader.GetInt32(1);
                                    if (!await reader.IsDBNullAsync(2))
                                        alumno.nombre = reader.GetString(2).Trim(' ');
                                    if (!await reader.IsDBNullAsync(3))
                                        alumno.apellidos = reader.GetString(3).Trim(' ');
                                    alumnos.Add(alumno);
                                }
                            }
                        }
                    }
                }
                return alumnos;
            }
            catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
            return null;
        }

        public async static Task<ObservableCollection<Profesores>> getNombreTutoresAsync (string connectionString)
        {
            try
            {
                ObservableCollection<Profesores> profesores = new ObservableCollection<Profesores>();
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        String Query = "select Tutores.id_tutor, Profesores.nombre, Profesores.apellidos, Profesores.departamento from Tutores " +
                                       "INNER JOIN Profesores ON Profesores.id_profesor = Tutores.id_profesor;";
                        using (SqlCommand cmd = conexion.CreateCommand())
                        {
                            cmd.CommandText = Query;
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    Profesores p = new Profesores();
                                    p.id_profesor = reader.GetInt32(0);
                                    if (!await reader.IsDBNullAsync(1))
                                        p.nombre = reader.GetString(1).Trim(' ');
                                    if (!await reader.IsDBNullAsync(2))
                                        p.apellidos = reader.GetString(2).Trim(' ');
                                    if (!await reader.IsDBNullAsync(3))
                                        p.departamento = reader.GetString(3).Trim(' ');
                                    profesores.Add(p);
                                }
                            }
                        }
                    }
                }
                return profesores;
            }
            catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
            return null;
        }

        public async static Task<ObservableCollection<TutoresProfesores>> getTutoresAsync(string connectionString)
        {
            try
            {
                ObservableCollection<TutoresProfesores> tutores = new ObservableCollection<TutoresProfesores>();
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        String Query = "SELECT Tutores.id_tutor, Tutores.id_profesor, Profesores.nombre, Profesores.apellidos, Profesores.departamento, Tutores.grupo, Tutores.semestre, Profesores.correo FROM Tutores " +
                                        "INNER JOIN Profesores ON Profesores.id_profesor = Tutores.id_profesor;";
                        using (SqlCommand cmd = conexion.CreateCommand())
                        {
                            cmd.CommandText = Query;
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    TutoresProfesores p = new TutoresProfesores();
                                    p.id_tutor = reader.GetInt32(0);
                                    if (!await reader.IsDBNullAsync(1))
                                        p.id_profesor = reader.GetInt32(1);
                                    if (!await reader.IsDBNullAsync(2))
                                        p.nombre = reader.GetString(2).Trim(' ');
                                    if (!await reader.IsDBNullAsync(3))
                                        p.apellidos = reader.GetString(3).Trim(' ');
                                    if (!await reader.IsDBNullAsync(4))
                                        p.departamento = reader.GetString(4).Trim(' ');
                                    if (!await reader.IsDBNullAsync(5))
                                        p.grupo = reader.GetString(5).Trim(' ');
                                    if (!await reader.IsDBNullAsync(6))
                                        p.semestre = reader.GetInt32(6);
                                    if (!await reader.IsDBNullAsync(7))
                                        p.correo = reader.GetString(7);
                                    tutores.Add(p);
                                }
                            }
                        }
                    }
                }
                return tutores;
            }
            catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
            return null;
        }

        public async static Task<ObservableCollection<Psicologos>> getPsicologosAsync(string connectionString)
        {
            try
            {
                ObservableCollection<Psicologos> psicologos = new ObservableCollection<Psicologos>();
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        String Query = "SELECT * FROM Psicologos";
                        using (SqlCommand cmd = conexion.CreateCommand())
                        {
                            cmd.CommandText = Query;
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    Psicologos p = new Psicologos();
                                    p.id_psicologo = reader.GetInt32(0);
                                    if (!await reader.IsDBNullAsync(1))
                                        p.nombre = reader.GetString(1).Trim(' ');
                                    if (!await reader.IsDBNullAsync(2))
                                        p.apellidos = reader.GetString(2).Trim(' ');
                                    psicologos.Add(p);
                                }
                            }
                        }
                    }
                }
                return psicologos;
            }
            catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
            return null;
        }

        public async static Task<ObservableCollection<string>> getStringsAsync (string connectionString, string Query)
        {
            try
            {
                ObservableCollection<string> textos = new ObservableCollection<string>();
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conexion.CreateCommand())
                        {
                            cmd.CommandText = Query;
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    string texto;
                                    texto = reader.GetString(0).Trim(' ');
                                    textos.Add(texto);
                                }
                            }
                        }
                    }
                }
                return textos;
            }
            catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
            return null;
        }

        public async static Task<string> getStringAsync(string connectionString, string Query)
        {
            string texto = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conexion.CreateCommand())
                        {
                            cmd.CommandText = Query;
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    texto = reader.GetString(0).Trim(' ');
                                }
                            }
                        }
                    }
                }
                return texto;
            }
            catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
            return null;
        }

        public async static Task<ObservableCollection<InfoGruposGS>> getGruposOnCarreraAsync (string connectionString, string Carrera)
        {
            try
            {
                ObservableCollection<InfoGruposGS> grupos = new ObservableCollection<InfoGruposGS>();
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string Query = "SELECT grupo, semestre FROM Tutores WHERE carrera = '" + Carrera + "';";
                    await conexion.OpenAsync();
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conexion.CreateCommand())
                        {
                            cmd.CommandText = Query;
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    InfoGruposGS grupo = new InfoGruposGS();
                                    grupo.grupo = reader.GetString(0).Trim(' ');
                                    if (!await reader.IsDBNullAsync(1))
                                        grupo.semestre = reader.GetInt32(1);
                                    grupos.Add(grupo);
                                }
                            }
                        }
                    }
                }
                return grupos;
            }
            catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
            return null;
        }

        public async static Task<string> getGruposOnIdTutorAsync(string connectionString, int id)
        {
            try
            {
                string grupo = string.Empty;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string Query = "SELECT grupo From Tutores WHERE Tutores.id_profesor = " + id + "; ";
                    await conexion.OpenAsync();
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conexion.CreateCommand())
                        {
                            cmd.CommandText = Query;
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    grupo = reader.GetString(0).Trim(' ');
                                }
                            }
                        }
                    }
                }
                return grupo;
            }
            catch (Exception eSql)
            {
                await new MessageDialog("Error!: " + eSql.Message).ShowAsync();
            }
            return "";
        }

        public async static Task<ObservableCollection<InfoAlumnos>> GetInfoAlumnosAsync(string connectionString, string tutor, string comando)
        {
            try
            {
                ObservableCollection<InfoAlumnos> alumnos = new ObservableCollection<InfoAlumnos>();
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        string Query = "SELECT DISTINCT Alumnos.id_alumno, matricula, Alumnos.nombre, Alumnos.apellidos, Provincias.id_provincia, Provincias.provincia, Provincias.cod_postal FROM Alumnos " +
                                       "INNER JOIN Grupos ON Grupos.id_alumno = Alumnos.id_alumno " +
                                       "INNER JOIN ResidenciasAlumnos ON ResidenciasAlumnos.id_alumno = Alumnos.id_alumno " +
                                       "INNER JOIN Provincias ON Provincias.id_provincia = ResidenciasAlumnos.id_provincia " +
                                       "INNER JOIN Tutores ON Tutores.id_tutor = Grupos.id_tutor " +
                                       "INNER JOIN Profesores ON Profesores.id_profesor = Tutores.id_profesor " +
                                       "WHERE CONCAT(TRIM(Profesores.nombre),' ', TRIM(Profesores.apellidos)) LIKE('%" + tutor + "%') " +
                                       comando + ";";
                        using (SqlCommand cmd = conexion.CreateCommand())
                        {
                            cmd.CommandText = Query;
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    InfoAlumnos alumno = new InfoAlumnos();
                                    alumno.id_alumno = reader.GetInt32(0);
                                    if (!await reader.IsDBNullAsync(1))
                                        alumno.matricula = reader.GetInt32(1);
                                    if (!await reader.IsDBNullAsync(2))
                                        alumno.nombre = reader.GetString(2).Trim(' ');
                                    if (!await reader.IsDBNullAsync(3))
                                        alumno.apellidos = reader.GetString(3).Trim(' ');
                                    if (!await reader.IsDBNullAsync(4))
                                        alumno.id_provincia = reader.GetInt32(4);
                                    if (!await reader.IsDBNullAsync(5))
                                        alumno.provincia= reader.GetString(5);
                                    if (!await reader.IsDBNullAsync(6))
                                        alumno.cod_postal = reader.GetInt32(6);
                                    alumnos.Add(alumno);
                                }
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

        public async static Task<List<InfoGrupos>> GetInfoGruposAsync (string connectionString)
        {
            try
            {
                List<InfoGrupos> grupos = new List<InfoGrupos>();
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        string Query = "SELECT id_tutor, Profesores.nombre, Profesores.apellidos, carrera, grupo, semestre FROM Tutores " +
                                       "INNER JOIN Profesores ON Profesores.id_profesor = Tutores.id_profesor;";
                        using (SqlCommand cmd = conexion.CreateCommand())
                        {
                            cmd.CommandText = Query;
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    InfoGrupos grupo = new InfoGrupos();
                                    grupo.id = reader.GetInt32(0);
                                    if (!await reader.IsDBNullAsync(1))
                                        grupo.nombre = reader.GetString(1).Trim(' ');
                                    if (!await reader.IsDBNullAsync(2))
                                        grupo.apellidos = reader.GetString(2).Trim(' ');
                                    if (!await reader.IsDBNullAsync(3))
                                        grupo.carrera = reader.GetString(3).Trim(' ');
                                    if (!await reader.IsDBNullAsync(4))
                                        grupo.grupo = reader.GetString(4).Trim(' ');
                                    if (!await reader.IsDBNullAsync(5))
                                        grupo.semestre = reader.GetInt32(5);
                                    grupos.Add(grupo);
                                }
                            }
                        }
                    }
                }
                return grupos;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }

        public async static Task<int> SetTutor(string connectionString, Tutores tutor)
        {
            SqlTransaction transaccion = null;
            string Query = "INSERT INTO Tutores (id_tutor, id_profesor, grupo, semestre, carrera) VALUES (@id_t, @id_p, @g, @s, @c)";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    transaccion = conexion.BeginTransaction();
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query;
                        cmd.Parameters.AddWithValue("@id_t", tutor.id_tutor);
                        cmd.Parameters.AddWithValue("@id_p", tutor.id_Profesor);
                        cmd.Parameters.AddWithValue("@g", tutor.grupo);
                        cmd.Parameters.AddWithValue("@s", tutor.semestre);
                        cmd.Parameters.AddWithValue("@c", tutor.carrera);
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            return -1;
                        transaccion.Commit();
                    }
                }
                return 0;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
                transaccion.Rollback();
            }
            return -1;
        }

        public async static Task<int> SetGrupo(string connectionString, Grupos grupo)
        {
            SqlTransaction transaccion = null;
            string Query = "INSERT INTO Grupos (id_alumno, id_tutor) VALUES (@id_a, @id_t)";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    transaccion = conexion.BeginTransaction();
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query;
                        cmd.Parameters.AddWithValue("@id_a", grupo.id_alumno);
                        cmd.Parameters.AddWithValue("@id_t", grupo.id_tutor);
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            return -1;
                        transaccion.Commit();
                    }
                }
                return 0;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
                transaccion.Rollback();
            }
            return -1;
        }

        public async static Task<int> setAlumnoAsync(string connectionString, InfoAlumnos alumno, int idTutor)
        {
            SqlTransaction transaccion = null;
            string[] Query = {"INSERT INTO Alumnos (id_alumno, matricula, nombre, apellidos) VALUES (@id_a, @m, @n, @a)",
                              "INSERT INTO Provincias (id_provincia, cod_postal, provincia) VALUES (@id_P, @cp, @p)",
                              "INSERT INTO ResidenciasAlumnos (id_alumno, id_provincia) VALUES (@id_fa, @id_fp)",
                              "INSERT INTO Grupos (id_alumno, id_tutor) VALUES (@id_tfa, @id_ft)"};
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    transaccion = conexion.BeginTransaction();
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query[0];
                        cmd.Parameters.AddWithValue("@id_a", alumno.id_alumno);
                        cmd.Parameters.AddWithValue("@m", alumno.matricula);
                        cmd.Parameters.AddWithValue("@n", alumno.nombre);
                        cmd.Parameters.AddWithValue("@a", alumno.apellidos);
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();

                        cmd.CommandText = Query[1];
                        cmd.Parameters.AddWithValue("@id_p", alumno.id_provincia);
                        cmd.Parameters.AddWithValue("@cp", alumno.cod_postal);
                        cmd.Parameters.AddWithValue("@p", alumno.provincia);
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();

                        cmd.CommandText = Query[2];
                        cmd.Parameters.AddWithValue("@id_fa", alumno.id_alumno);
                        cmd.Parameters.AddWithValue("@id_fp", alumno.id_provincia);
                        cmd.Transaction = transaccion;
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();

                        cmd.CommandText = Query[3];
                        cmd.Parameters.AddWithValue("@id_tfa", alumno.id_alumno);
                        cmd.Parameters.AddWithValue("@id_ft", idTutor);
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();
                        transaccion.Commit();
                    }
                }
                return 0;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
                transaccion.Rollback();
            }
            return -1;
        }

        public async static Task<int> setProfesoresAsync(string connectionString, InfoProfesores profesor)
        {
            SqlTransaction transaccion = null;
            string[] QueryResidencia = { "INSERT INTO Provincias (id_provincia, cod_postal, provincia) VALUES (@id_prov, @cp, @p)",
                                         "INSERT INTO ResidenciasProfesores (id_provincia, id_profesor) VALUES (@id_p, id_prof)"};
            string Query = "INSERT INTO Profesores (id_profesor, nombre, apellidos, departamento, correo, imagen) VALUES (@id_p, @n, @a, @d, @c, @img)";
            try
            {
                byte[] result;
                using (var streamReader = new MemoryStream())
                {
                    profesor.imagen.CopyTo(streamReader);
                    result = streamReader.ToArray();
                }
                profesor.id_profesor = await GetId((App.Current as App).ConnectionString, "SELECT (MAX(id_profesor) + 1) FROM Profesores;");
                int id_provincia = await GetId((App.Current as App).ConnectionString, "SELECT (MAX(id_provincia) + 1) FROM Provincias");
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    transaccion = conexion.BeginTransaction();
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query;
                        cmd.Parameters.AddWithValue("@id_p", profesor.id_profesor);
                        cmd.Parameters.AddWithValue("@n", profesor.nombre);
                        cmd.Parameters.AddWithValue("@a", profesor.apellidos);
                        cmd.Parameters.AddWithValue("@d", profesor.departamento);
                        cmd.Parameters.AddWithValue("@c", profesor.correo);
                        cmd.Parameters.AddWithValue("@img", result);
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();

                        cmd.CommandText = QueryResidencia[0];
                        cmd.Parameters.AddWithValue("@id_prov", id_provincia);
                        cmd.Parameters.AddWithValue("@cp", profesor.cod_postal);
                        cmd.Parameters.AddWithValue("@p", profesor.provincia);
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();

                        cmd.CommandText = QueryResidencia[1];
                        cmd.Parameters.AddWithValue("@id_p", id_provincia);
                        cmd.Parameters.AddWithValue("@id_prof", profesor.id_profesor);
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();
                        
                        transaccion.Commit();
                    }
                }
                return 0;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
                transaccion.Rollback();
            }
            return -1;
        }

        public async static Task<int> setBajaAlumnoAsync(string connectionString, InfoAlumnos alumno)
        {
            SqlTransaction transaccion = null;
            string[] Query = { "DELETE FROM Alumnos WHERE id_alumno = " + alumno.id_alumno,
                               "DELETE FROM Provincias WHERE id_provincia = " + alumno.id_provincia,
                               "DELETE FROM ResidenciasAlumnos WHERE id_alumno = " + alumno.id_alumno,
                               "DELETE FROM Grupos WHERE id_alumno = " + alumno.id_alumno};
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    transaccion = conexion.BeginTransaction();
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query[0];
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error borrando la fila de la base de datos!").ShowAsync();
                        cmd.CommandText = Query[1];
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error borrando la fila de la base de datos!").ShowAsync();
                        cmd.CommandText = Query[2];
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error borrando la fila de la base de datos!").ShowAsync();
                        cmd.CommandText = Query[3];
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error borrando la fila de la base de datos!").ShowAsync();
                        transaccion.Commit();
                    }
                }
                return 0;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
                transaccion.Rollback();
            }
            return -1;
        }

        public async static Task<int> setBajaProfesorAsync(string connectionString, InfoProfesores profesor)
        {
            SqlTransaction transaccion = null;
            string[] Query = { "DELETE FROM Profesores WHERE id_profesor = " + profesor.id_profesor,
                               "DELETE FROM ResidenciasProfesores WHERE id_profesor = " + profesor.id_profesor,
                               "DELETE FROM Grupos WHERE id_tutor = " + await GetId((App.Current as App).ConnectionString, "SELECT id_tutor FROM Tutores WHERE id_profesor = "+ profesor.id_profesor +";"),
                               "DELETE FROM Tutores WHERE id_profesor = "+ profesor.id_profesor +""};
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    transaccion = conexion.BeginTransaction();
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query[0];
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error borrando la fila de la base de datos!").ShowAsync();
                        cmd.CommandText = Query[1];
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error borrando la fila de la base de datos!").ShowAsync();
                        cmd.CommandText = Query[2];
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error borrando la fila de la base de datos!").ShowAsync();
                        cmd.CommandText = Query[3];
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error borrando la fila de la base de datos!").ShowAsync();
                        transaccion.Commit();
                    }
                }
                return 0;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
                transaccion.Rollback();
            }
            return -1;
        }

        public async static Task<int> setActualizarAlumnoAsync(string connectionString, InfoAlumnos alumno)
        {
            SqlTransaction transaccion = null;
            string[] Query = {"UPDATE Alumnos SET matricula = " + alumno.matricula + ", nombre = '" + alumno.nombre + "', apellidos = '" + alumno.apellidos +
                              "' WHERE id_alumno = " + alumno.id_alumno,
                              "UPDATE Provincias SET cod_postal = " + alumno.cod_postal + ", provincia = '" + alumno.provincia +
                              "' WHERE id_provincia = " + alumno.id_provincia};
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    transaccion = conexion.BeginTransaction();
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query[0];
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error actualizando la fila de la base de datos!").ShowAsync();
                        cmd.CommandText = Query[1];
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error actualizando la fila de la base de datos!").ShowAsync();
                        transaccion.Commit();
                    }
                }
                return 0;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
                transaccion.Rollback();
            }
            return -1;
        }

        public async static Task<int> setActualizarProfesorAsync(string connectionString, InfoProfesores profesor)
        {
            SqlTransaction transaccion = null;
            string[] Query = {"UPDATE Profesores SET nombre  = '" + profesor.nombre + "', apellidos = '" + profesor.apellidos+ "', departamento = '" + profesor.departamento +
                              "', correo = '"+  profesor.correo + "', imagen = @imagen WHERE id_profesor = " + profesor.id_profesor,
                              "UPDATE Provincias SET cod_postal = " + profesor.cod_postal + ", provincia = '" + profesor.provincia +
                              "' WHERE id_provincia = " + profesor.id_provincia};
            try
            {
                byte[] result;
                using (var streamReader = new MemoryStream())
                {
                profesor.imagen.CopyTo(streamReader);
                result = streamReader.ToArray();
                }
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    transaccion = conexion.BeginTransaction();
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        profesor.imagen.Position = 0;
                        cmd.CommandText = Query[0];
                        cmd.Parameters.Add(new SqlParameter("@imagen", result));
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error actualizando la fila de la base de datos!").ShowAsync();
                        cmd.CommandText = Query[1];
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error actualizando la fila de la base de datos!").ShowAsync();
                        transaccion.Commit();
                    }
                }
                return 0;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
                transaccion.Rollback();
            }
            return -1;
        }

        public async static Task<int> setActualizarTutorAsync(string connectionString, TutoresProfesores tutor)
        {
            SqlTransaction transaccion = null;
            string[] Query = { "UPDATE Profesores SET nombre = '" + tutor.nombre + "', apellidos = '" + tutor.apellidos + "' WHERE id_profesor = " + tutor.id_profesor + ";"};
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    transaccion = conexion.BeginTransaction();
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query[0];
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error actualizando la fila de la base de datos!").ShowAsync();
                        transaccion.Commit();
                    }
                }
                return 0;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
                transaccion.Rollback();
            }
            return -1;
        }

        public async static Task<int> setActualizarJefeAsync(string connectionString, string nombreJefeTut, string nombreJefeTutIns, string nombreJefeDep)
        {
            SqlTransaction transaccion = null;
            string[] Query = { "UPDATE CoordinadoresTutorias SET nombre = '" + nombreJefeTut + "' WHERE id_jefe = 1;" ,
                               "UPDATE CoordinadoresTutoriasInstitucionales SET nombre = '" + nombreJefeTutIns + "' WHERE id_jefe = 1;",
                               "UPDATE JefesDepartamentos SET nombre = '" + nombreJefeDep + "' WHERE id_jefe = 1;"};
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    transaccion = conexion.BeginTransaction();
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query[0];
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();
                        cmd.CommandText = Query[1];
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();
                        cmd.CommandText = Query[2];
                        cmd.Transaction = transaccion;
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            await new MessageDialog("Error insertando la fila de la base de datos!").ShowAsync();
                        transaccion.Commit();
                    }
                }
                return 0;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
                transaccion.Rollback();
            }
            return -1;
        }

        public async static Task<int> GetId(string connectionString, string Query)
        {
            int id = 0;
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
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
                }
                return id;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return 0;
        }
    }
}