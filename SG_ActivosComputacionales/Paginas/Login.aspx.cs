using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.Seguridad;

namespace SG_ActivosComputacionales.Paginas
{
    public partial class Login : System.Web.UI.Page
    {
        LoginNegocio inicio = new LoginNegocio();
        GestorPermisos permisos = new GestorPermisos();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Metodos para el Login
        protected void btnLoginSubmit_Click(object sender, EventArgs e)
        {
            string usuario = txtLoginUsuario.Text;
            string password = txtLoginPassword.Text;

            if (ValidarCamposL(usuario, password))
            {
                bool resultado = inicio.ValidarCredenciales(usuario, password);

                if (resultado)
                {
                    permisos.GetInfoUser(usuario, password);

                    Session["Rol"] = permisos.Rol;
                    Session["Usuario"] = permisos.Usuario;
                    Session["Cedula"] = permisos.Cedula;

                    MostrarMensajeTemporal(errorMensajeL.ClientID, " ¡Inicio de sesión exitoso! Bienvenido " + Session["Usuario"].ToString());

                    txtLoginUsuario.Text = "";
                    txtLoginPassword.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "setTimeout(function(){ window.location.href = 'IndexPostLogin.aspx'; }, 3000);", true);
                }
                else
                {
                    txtLoginUsuario.Text = "";
                    txtLoginPassword.Text = "";
                    MostrarMensajeTemporal(errorMensajeL.ClientID, "Credenciales Invalidas. Intentelo de nuevo");
                }
            }
        }

        private bool ValidarCamposL(string usuario, string password)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MostrarMensajeTemporal(errorMensajeL.ClientID, "Hay uno o mas campos vacios. Porfavor llene todos los campos.");
                return false;
            }
            return true;
        }

        //Fin de metodos para el login
        //Metodos para el Registro
        protected void btnSignUpSubmit_Click(object sender, EventArgs e)
        {
            string cedula = txtRegistroCedula.Text;
            string nombre = txtRegistroNombre.Text;
            string apellidos = txtRegistroApellido.Text;
            string usuario = txtRegistroUsuario.Text;
            string password = txtRegistroPassword.Text;
            string passwordC = txtSignUpConfirmPassword.Text;
            if (ValidarCamposR(cedula, nombre, apellidos, usuario, password, passwordC))
            {
                bool resultado = inicio.RegistrarUsuario(cedula, nombre, apellidos, usuario, password);

                if (resultado)
                {
                    txtRegistroCedula.Text = "";
                    txtRegistroNombre.Text = "";
                    txtRegistroApellido.Text = "";
                    txtRegistroUsuario.Text = "";
                    txtRegistroPassword.Text = "";
                    MostrarMensajeTemporal(errorMensajeR.ClientID, "¡Creacion de cuenta realizada exitosamente!");
                }
                else
                {
                    MostrarMensajeTemporal(errorMensajeR.ClientID, "¡Este usuario ya existe dentro del sistema!");
                }
            }
        }

        private bool ValidarCamposR(string cedula, string nombre, string apellidos, string usuario, string password, string passwordC)
        {
            if (string.IsNullOrWhiteSpace(cedula) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellidos) || string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordC))
            {
                MostrarMensajeTemporal(errorMensajeR.ClientID, "Por favor llene todos los campos, hay uno o mas espacios vacios."); 
                 return false;
            }
            if (!EsNumero(cedula))
            {
                MostrarMensajeTemporal(errorMensajeR.ClientID, "La cédula debe ser un número."); 
                 return false;
            }
            if (password != passwordC)
            {
                MostrarMensajeTemporal(errorMensajeR.ClientID, "Las contraseñas no coinciden."); 
                return false;
            }
            errorMensajeR.InnerText = "";
            return true;
        }

        private bool EsNumero(string str)
        {
            return int.TryParse(str, out _);
        }
        //Fin de metodos para el registro

        //Mostrar mensajes
        protected void MostrarMensajeTemporal(string controlId, string mensaje)
        {
            // Generar el script JavaScript
            string script = $"setTimeout(function(){{ document.getElementById('{controlId}').innerText = ''; }}, 6000);";

            // Registrar el script en el cliente
            Page.ClientScript.RegisterStartupScript(this.GetType(), controlId + "_script", script, true);

            // Establecer el mensaje en el campo span
            Page.ClientScript.RegisterStartupScript(this.GetType(), controlId + "_mensaje", $"document.getElementById('{controlId}').innerText = '{mensaje}';", true);
        }
        //Fin mostrar mensajes
    }
}