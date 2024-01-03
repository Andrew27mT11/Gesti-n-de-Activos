using System;
using System.Web;

namespace SG_ActivosComputacionales.Paginas
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rol = string.Empty;

            if (!IsPostBack && Session["Usuario"] != null)
            {
                rol = Session["Rol"].ToString();

                if (rol == "Administrador")
                {
                    lnkbtnImportarActivos.Visible = true;
                    lnkbtnReporteria.Visible = true;

                }

                if (rol == "Usuario")
                {
                    lnkbtnImportarActivos.Visible = false;
                    lnkbtnReporteria.Visible = true;
                }
            }
        }

        protected void lnkbtnSalir_Click(object sender, EventArgs e)
        {
            Session["Rol"] = null;
            Session["Usuario"] = null;
            Session["Cedula"] = null;

            HttpContext.Current.Session.Abandon();
        }

        protected void lnkbtnImportarActivos_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
            {
                Response.Redirect("ImportacionActivos.aspx");
            }
        }

        protected void lnkbtnReporteria_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
            {
                Response.Redirect("Reportes.aspx");
            }
        }

        protected void lnkbtnInicio_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null) 
            {
                Response.Redirect("IndexPostLogin.aspx");
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }
    }
}