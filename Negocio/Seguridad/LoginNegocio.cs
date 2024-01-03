using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Base_de_datos;

namespace Negocio.Seguridad
{
    public class LoginNegocio
    {
        ConexionBD accesoDatos = new ConexionBD();

        public bool ValidarCredenciales(string usuario, string password)
        {
            return this.accesoDatos.ValidarCredencialesLogin(usuario, password);
        }

        public bool RegistrarUsuario(string cedula, string nombre, string apellido, string usuario, string password)
        {
            return this.accesoDatos.RegistrarUsuario(cedula, nombre, apellido, usuario, password);
        }
    }
}
