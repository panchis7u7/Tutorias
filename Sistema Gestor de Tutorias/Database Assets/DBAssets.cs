using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
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
        public async static Task<ObservableCollection<Profesores>> getProfesoresAsync(string connectionString)
        {
            try
            {
                ObservableCollection<Profesores> profesores = new ObservableCollection<Profesores>();
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        String Query = "SELECT * FROM Profesores";
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
            } catch (Exception eSql)
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

        public async static Task<ObservableCollection<string>> getCarrerasAsync (string connectionString)
        {
            try
            {
                ObservableCollection<string> carreras = new ObservableCollection<string>();
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        String Query = "SELECT DISTINCT carrera FROM Tutores";
                        using (SqlCommand cmd = conexion.CreateCommand())
                        {
                            cmd.CommandText = Query;
                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    string p;
                                    p = reader.GetString(0).Trim(' ');
                                    carreras.Add(p);
                                }
                            }
                        }
                    }
                }
                return carreras;
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
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    string Query = "INSERT INTO Tutores (id_tutor, id_profesor, grupo, semestre, carrera) VALUES (@id_t, @id_p, @g, @s, @c)";
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query;
                        cmd.Parameters.AddWithValue("@id_t", tutor.id_tutor);
                        cmd.Parameters.AddWithValue("@id_p", tutor.id_Profesor);
                        cmd.Parameters.AddWithValue("@id_g", tutor.grupo);
                        cmd.Parameters.AddWithValue("@id_s", tutor.semestre);
                        cmd.Parameters.AddWithValue("@id_c", tutor.carrera);
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            return -1;
                    }
                }
                return 0;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return -1;
        }

        public async static Task<int> SetGrupo(string connectionString, Grupos grupo)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    string Query = "INSERT INTO Grupos (id_alumno, id_tutor) VALUES (@id_a, @id_t)";
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query;
                        cmd.Parameters.AddWithValue("@id_a", grupo.id_alumno);
                        cmd.Parameters.AddWithValue("@id_t", grupo.id_tutor);
                        if (await cmd.ExecuteNonQueryAsync() < 0)
                            return -1;
                    }
                }
                return 0;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
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
