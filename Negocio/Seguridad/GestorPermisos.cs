using Datos.Base_de_datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Seguridad
{
    public class GestorPermisos
    {
        string cedula = "";
        string _usuario = "";
        string _email = "";
        string rol = "";
        ConexionBD conexionBD = new ConexionBD();

        #region "PROPIEDADES"

        public string Cedula { get => cedula; set => cedula = value; }
        public string Usuario { get => _usuario; set => _usuario = value; }
        public string Email { get => _email; set => _email = value; }
        public string Rol { get => rol; set => rol = value; }

        #endregion

        #region "METODOS"
        
        public void GetInfoUser(string user, string password)
        {
            conexionBD.GetUserInfo(user, password);

            Cedula = conexionBD.Cedula;
            Usuario = conexionBD.NombreCompleto;
            Rol = conexionBD.Rol;
        }

        #endregion
    }
}
