using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Datos.Base_de_datos
{
    public class ConexionBD
    {
        string connection_string = ConfigurationManager.ConnectionStrings["BD_SG_ActivosComputacionales"].ConnectionString;

        // Para devolver el rol, nombre Completo y cedula
        string _NombreCompleto;
        string _Rol;
        string _Cedula;

        // Para determinar los permisos que tiene en TRUE / FALSE el usuario que se encuentra logeado
        bool _Create, _Read, _Update, _Delete;

        #region "PROPIEDADES"

        public string NombreCompleto { get => _NombreCompleto; set => _NombreCompleto = value; }
        public string Rol { get => _Rol; set => _Rol = value; }
        public string Cedula { get => _Cedula; set => _Cedula = value; }
        public bool Create { get => _Create; set => _Create = value; }
        public bool Read { get => _Read; set => _Read = value; }
        public bool Update { get => _Update; set => _Update = value; }
        public bool Delete { get => _Delete; set => _Delete = value; }

        #endregion



        /*
         
        Si vamos a usar solo esta clase para todas las conexiones escriban sus metodos en la región de cada uno para llevar un orden 
        y no perderse por si hay que corregir código
         
         */


        #region "InclusionActivos"

        /* Aqui como trabajan 2 personas si es necesario que separen en 2 sub-regiones lo pueden hacer, 
         * si no trabajan los 2 juntos dentro de esta misma */

        /*  
         
         Si van a separarse por persona descomentan las siguientes lineas, si no solo las borran:
         
         */

        //#region "Gustavo"

        //#endregion

        #region "Alejandro"

        public void InsertarActivo(int idactivo, string categoria, string marca, string modelo, string estado, string Condicion, string depa, string area, string direccionE)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connection_string))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("ProyectoG2_FundaAdmin.SP_InsertarActivo", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@idActivo", SqlDbType.Int) { Value = idactivo });
                        cmd.Parameters.Add(new SqlParameter("@Categoria", SqlDbType.VarChar, 30) { Value = categoria });
                        cmd.Parameters.Add(new SqlParameter("@Marca", SqlDbType.VarChar, 30) { Value = marca });
                        cmd.Parameters.Add(new SqlParameter("@Modelo", SqlDbType.VarChar, 30) { Value = modelo });
                        cmd.Parameters.Add(new SqlParameter("@Estado", SqlDbType.VarChar, 10) { Value = estado });
                        cmd.Parameters.Add(new SqlParameter("@Condicion", SqlDbType.VarChar, 10) { Value = Condicion });
                        cmd.Parameters.Add(new SqlParameter("@Depa", SqlDbType.VarChar, 30) { Value = depa });
                        cmd.Parameters.Add(new SqlParameter("@Area", SqlDbType.VarChar, 30) { Value = area });
                        cmd.Parameters.Add(new SqlParameter("@DireccionE", SqlDbType.VarChar, 50) { Value = direccionE });

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar un nuevo activo al sistema" + ex.Message);
            }
        }

        #endregion

        #endregion

        #region "Seguridad"

        // Andrés trabaja en esta región
        public bool ValidarCredencialesLogin(string usuario, string contraseña)
        {
            try
            {
                using (SqlConnection conexión = new SqlConnection(this.connection_string))
                {
                    conexión.Open();

                    using (SqlCommand comando = new SqlCommand("ValidarCredenciales", conexión)) // Reemplaza "ValidarCredenciales" con el nombre real de tu procedimiento almacenado
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("@Usuario", usuario);
                        comando.Parameters.AddWithValue("@Contraseña", contraseña);

                        int recuento = (int)comando.ExecuteScalar();

                        return recuento > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar credenciales: " + ex.Message);
            }
        }

        public void GetUserInfo(string user, string password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connection_string))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_GetDataUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@User", user);
                        cmd.Parameters.AddWithValue("@Pass", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                Cedula = reader[0].ToString();
                                NombreCompleto = reader[1].ToString();
                                Rol = reader[2].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al recuperar los datos del usuario " + ex.Message); ;
            }
        }

        public bool RegistrarUsuario(string cedula, string nombre, string apellido, string usuario, string password)
        {
            try
            {
                string rol = "Usuario";
                using (SqlConnection conexión = new SqlConnection(this.connection_string))
                {
                    conexión.Open();
                    using (SqlCommand verificarUsuario = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE Usuario = @Usuario", conexión))
                    {
                        verificarUsuario.Parameters.AddWithValue("@Usuario", usuario);

                        int usuariosExistentes = (int)verificarUsuario.ExecuteScalar();

                        if (usuariosExistentes > 0)
                        {
                            return false;
                        }
                    }

                    using (SqlCommand comando = new SqlCommand("INSERT INTO Usuarios (Cedula, Nombre, Apellido, Usuario, Pass, Rol) VALUES (@Cedula, @Nombre, @Apellido, @Usuario, @Contraseña, @Rol)", conexión))
                    {
                        comando.Parameters.AddWithValue("@Cedula", cedula);
                        comando.Parameters.AddWithValue("@Nombre", nombre);
                        comando.Parameters.AddWithValue("@Apellido", apellido);
                        comando.Parameters.AddWithValue("@Usuario", usuario);
                        comando.Parameters.AddWithValue("@Contraseña", password);
                        comando.Parameters.AddWithValue("@Rol", rol);

                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar usuario: " + ex.Message);
            }
        }
        #endregion

        #region "Reporteria"

        // Keiron trabaja en esta región
        public DataSet ObtenerReportes(string processName)
        {
            try
            {
                DataSet dsReporte = new DataSet();

                using (SqlConnection con = new SqlConnection(connection_string))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(processName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dsReporte);
                        }
                    }
                    return dsReporte;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se encontraron activos dentro del sistema: " + ex.Message);
            }
        }

        #endregion
    }
}
