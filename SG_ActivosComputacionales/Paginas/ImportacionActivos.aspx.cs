using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.InclusionArchivos;

namespace SG_ActivosComputacionales.Paginas
{
    public partial class ImportacionActivos : System.Web.UI.Page
    {
        InclusionActivos obj_Activos = new InclusionActivos();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Usuario"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else if (!IsPostBack)
                {
                    if (Session["Activos"] != null)
                    {
                        gvActivos.DataSource = (DataTable)Session["Activos"];
                        gvActivos.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacionError", $"mostrarNotificacionError('{ex.Message}');", true);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                obj_Activos.idActivo = int.Parse(txtIdActivo.Text);
                obj_Activos.Categoria = ddlCategorias.SelectedValue;
                obj_Activos.Marca = ddlMarca.SelectedValue;
                obj_Activos.Modelo = txtModelo.Text;
                obj_Activos.Estado = ddlEstado.SelectedValue;
                obj_Activos.Condicion = ddlCondicion.SelectedValue;
                obj_Activos.Departamento = ddlDepartamento.SelectedValue;
                obj_Activos.Area = ddlArea.SelectedValue;
                obj_Activos.DireccionE = txtDireccionExacta.Text;

                obj_Activos.ValidarDatos();
                obj_Activos.AgregarActivo();

                LimpiarCampos();
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacionExito", $"mostrarNotificacionExito('¡Se insertaron los activos al sistema!');", true);


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacionError", $"mostrarNotificacionError('¡No se pudo agregar el activo al sistema!');", true);
            }
        }


        private void LimpiarCampos()
        {
            txtIdActivo.Text = string.Empty;
            ddlCategorias.SelectedIndex = 0;
            ddlMarca.SelectedIndex = 0;
            txtModelo.Text = string.Empty;
            ddlEstado.SelectedIndex = 0;
            ddlCondicion.SelectedIndex = 0;
            ddlDepartamento.SelectedIndex = 0;
            ddlArea.SelectedIndex = 0;
            txtDireccionExacta.Text = string.Empty;
        }
        protected void btnCargarActivos_Click(object sender, EventArgs e)
        {
            try
            {
                if (SubirActivos.HasFile)
                {
                    string fileExtension = Path.GetExtension(SubirActivos.FileName).ToLower();

                    // Verificar si la extensión es .csv
                    if (fileExtension == ".csv")
                    {
                        string filePath = Path.Combine(Path.GetTempPath(), SubirActivos.FileName);

                        SubirActivos.SaveAs(filePath);

                        DataTable dt = LeerArchivoCSV(filePath);
                        DataTable errores;

                        obj_Activos.AgregarActivoDesdeCSV(dt, out errores);

                        gvActivos.DataSource = dt;
                        gvActivos.DataBind();

                        bool activosIngresados = dt.Rows.Count > 0 && errores.Rows.Count < dt.Rows.Count;

                        if (activosIngresados)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacionExito", $"mostrarNotificacionExito('¡Se insertaron activos al sistema!');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacionError", $"mostrarNotificacionError('No se pudieron agregar activos al sistema.');", true);
                        }

                        ResaltarErrores(errores);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacionError", $"mostrarNotificacionError('Por favor, seleccione un archivo CSV válido.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacionError", $"mostrarNotificacionError('Por favor, seleccione un archivo.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarNotificacionError", $"mostrarNotificacionError('{ex.Message}');", true);
            }
        }


        private void ResaltarErrores(DataTable errores)
        {
            try
            {
                string colorRosaClaro = "#FFD6D6";

                foreach (DataRow row in errores.Rows)
                {
                    int idActivoError = Convert.ToInt32(row["idActivo"]);

                    foreach (GridViewRow gvRow in gvActivos.Rows)
                    {
                        int idActivoGrid = Convert.ToInt32(gvRow.Cells[0].Text);

                        if (idActivoGrid == idActivoError)
                        {
                            gvRow.BackColor = System.Drawing.ColorTranslator.FromHtml(colorRosaClaro);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "mostrarNotificacionError", $"mostrarNotificacionError('{ex.Message}');", true);
            }
        }

        private DataTable LeerArchivoCSV(string filePath)
        {
            try
            {
                DataTable dt = new DataTable();

                string[] lines = File.ReadAllLines(filePath);
                string[] headers = lines[0].Split(',');

                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] data = lines[i].Split(',');
                    DataRow row = dt.NewRow();

                    for (int j = 0; j < headers.Length; j++)
                    {
                        row[j] = data[j];
                    }

                    dt.Rows.Add(row);
                }

                return dt;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "mostrarNotificacionError", $"mostrarNotificacionError('{ex.Message}');", true);
                return null;
            }
        }


        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("IndexPostLogin.aspx");
        }
    }
}
