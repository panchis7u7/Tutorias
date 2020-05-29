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

        public async static Task<ObservableCollection<string>> getCarrerasAsync(string connectionString)
        {
            try
            {
                ObservableCollection<string> carreras = new ObservableCollection<string>();
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    await conexion.OpenAsync();
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        String Query = "SELECT DISTINCT carrera FROM Alumnos";
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

        public async static Task<List<InfoGrupos>> GetGruposAsync (SqlConnection conexion, string Query)
        {
            int i = 0;
            try
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = conexion.CreateCommand())
                    {
                        cmd.CommandText = Query;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            List<InfoGrupos> grupos = new List<InfoGrupos>();
                            while (await reader.ReadAsync())
                            {
                                InfoGrupos grupo = new InfoGrupos();
                                grupo.id = i;
                                grupo.grupo = reader.GetString(0).Trim(' ');
                                if (!await reader.IsDBNullAsync(1))
                                    grupo.nombre = reader.GetString(1).Trim(' ');
                                if (!await reader.IsDBNullAsync(2))
                                    grupo.apellidos = reader.GetString(2).Trim(' ');
                                if (!await reader.IsDBNullAsync(3))
                                    grupo.semestre = reader.GetInt32(3);
                                if (!await reader.IsDBNullAsync(4))
                                    grupo.carrera = reader.GetString(4).Trim(' ');
                                grupos.Add(grupo);
                                i += 1;
                            }
                            reader.Close();
                            cmd.Dispose();
                            return grupos;
                        }
                    }
                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
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
