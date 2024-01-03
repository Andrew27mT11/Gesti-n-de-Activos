using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.Reporteria;

namespace SG_ActivosComputacionales.Paginas
{
    public partial class WebForm1 : Page
    {
        // Inicializar la clase ReportesActivos para acceder a sus métodos
        ReportesActivos gestorReportesActivos = new ReportesActivos();

        protected void Page_Load(object sender, EventArgs e)
        {

            // Linea para ejecutar la notificacion toastr de JavaScript (Exito)
            // ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacionExito", $"mostrarNotificacionExito('{mensajePersonalizado}');", true);

            // Linea para ejecutar la notificacion toastr de JavaScript (Error)
            // ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacionError", $"mostrarNotificacionError('{mensajePersonalizado}');", true);

            try
            {
                if (Session["Usuario"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else if (!IsPostBack)
                {
                    // Obtiene los valores sin filtro desde la BD
                    ObtenerDatosReporteActivosDesdeBD();
                    grdReportes.DataSource = (DataSet)Session["dsReporteActivos"];
                    grdReportes.DataBind();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacionError", $"mostrarNotificacionError('{ex.Message}');", true);
            }
        }

        protected void ddlTipoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string tipoFiltro = ddlTipoFiltro.SelectedValue;

                switch (tipoFiltro.ToLower())
                {
                    case "id":
                        // Habilita el textbox para escribir el ID
                        divddlValorFiltro.Visible = false;
                        divtxtIDFiltro.Visible = true;
                        divBtnAplicarFiltros.Visible = true;
                        UpdPanel_Reportes.Update();

                        break;

                    case "estado":

                        // Limpiar valores anteriores si existen
                        ddlValorFiltro.Items.Clear();

                        // Añade un selector default "Seleccione valor"
                        ddlValorFiltro.Items.Add(new ListItem("Seleccione valor", ""));

                        // Obtiene los valores de estado desde bd para bindear al ddl
                        DataSet dsEstados = (DataSet)Session["dsEstados"];

                        if (dsEstados != null && dsEstados.Tables.Count > 0 && dsEstados.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in dsEstados.Tables[0].Rows)
                            {
                                string conditionValue = row["Estado"].ToString();

                                ddlValorFiltro.Items.Add(new ListItem(conditionValue, conditionValue));
                            }
                        }

                        divtxtIDFiltro.Visible = false;
                        divddlValorFiltro.Visible = true;
                        divBtnAplicarFiltros.Visible = false;

                        UpdPanel_Reportes.Update();

                        break;

                    case "condicion":

                        // Limpiar valores anteriores si existen
                        ddlValorFiltro.Items.Clear();

                        // Añade un selector default "Seleccione valor"
                        ddlValorFiltro.Items.Add(new ListItem("Seleccione valor", ""));

                        // Obtiene los valores de condicion desde bd para bindear al ddl
                        DataSet dsCondiciones = (DataSet)Session["dsCondiciones"];

                        if (dsCondiciones != null && dsCondiciones.Tables.Count > 0 && dsCondiciones.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in dsCondiciones.Tables[0].Rows)
                            {
                                string conditionValue = row["Condicion"].ToString();

                                ddlValorFiltro.Items.Add(new ListItem(conditionValue, conditionValue));
                            }
                        }
                        
                        divtxtIDFiltro.Visible = false;
                        divddlValorFiltro.Visible = true;
                        divBtnAplicarFiltros.Visible = false;


                        UpdPanel_Reportes.Update();

                        break;

                    case "default":
                        divddlValorFiltro.Visible = false;
                        divtxtIDFiltro.Visible = false;
                        divBtnAplicarFiltros.Visible = false;

                        grdReportes.DataSource = (DataSet)Session["dsReporteActivos"];
                        grdReportes.DataBind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacionError", $"mostrarNotificacionError('{ex.Message}');", true);
            }
        }

        protected void btnAplicarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                string tipoFiltro = ddlTipoFiltro.SelectedValue;
                DataSet dsActvos = (DataSet)Session["dsReporteActivos"];
                int? valorIDFiltro = int.TryParse(txtIDFiltro.Text, out int parsedValue) ? (int?)parsedValue : null;
                string valorFiltro = ddlValorFiltro.SelectedValue;

                gestorReportesActivos.IdFiltro = valorIDFiltro;
                gestorReportesActivos.ValorFiltro = valorFiltro;

                gestorReportesActivos.Validaciones();

                grdReportes.DataSource = gestorReportesActivos.FiltrarActivos(tipoFiltro, dsActvos);
                grdReportes.DataBind();

                UpdPanel_Reportes.Update();

                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacionExito", $"mostrarNotificacionExito('Filtros aplicados con éxito!');", true);
                txtIDFiltro.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacionError", $"mostrarNotificacionError('{ex.Message}');", true);
            }
        }

        private void ObtenerDatosReporteActivosDesdeBD()
        {
            Session["dsReporteActivos"] = gestorReportesActivos.ObtenerDatosReporte();
            Session["dsEstados"] = gestorReportesActivos.ObtenerEstadosReporte();
            Session["dsCondiciones"] = gestorReportesActivos.ObtenerCondicionesReporte();
        }

        protected void ddlValorFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string valorSeleccionado = ddlValorFiltro.SelectedValue;

                if(valorSeleccionado != "")
                {
                    divBtnAplicarFiltros.Visible = true;
                    UpdPanel_Reportes.Update();
                }
                else
                {
                    divBtnAplicarFiltros.Visible = false;
                    UpdPanel_Reportes.Update();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacionError", $"mostrarNotificacionError('{ex.Message}');", true);
            }
        }
    }
}