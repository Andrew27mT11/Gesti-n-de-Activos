using System;
using System.Data;
using Datos.Base_de_datos;

namespace Negocio.Reporteria
{
    public class ReportesActivos
    {
        ConexionBD conexionBD = new ConexionBD();

        int? idFiltro = null;
        string valorFiltro = null;

        #region "Propiedades"
            public int? IdFiltro { get => idFiltro; set => idFiltro = value; }
            public string ValorFiltro { get => valorFiltro; set => valorFiltro = value; }

        #endregion

        #region "Métodos"
        public DataSet ObtenerDatosReporte()
        {
            string processName = "GetReportesActivos";
            return conexionBD.ObtenerReportes(processName);
        }

        public DataSet ObtenerEstadosReporte()
        {
            string processName = "GetReporteEstados";
            return conexionBD.ObtenerReportes(processName);
        }

        public DataSet ObtenerCondicionesReporte()
        {
            string processName = "GetReporteCondiciones";
            return conexionBD.ObtenerReportes(processName);
        }

        public DataSet FiltrarActivos(string filtro, DataSet dsActivos)
        {
            try
            {
                DataSet dsActivosFiltrados = new DataSet();

                // Verifica si existen tablas en el DataSet
                if (dsActivos.Tables.Count >= 1)
                {
                    DataTable dtActivos = dsActivos.Tables[0];

                    // Utiliza LINQ para realizar el filtrado según el criterio especificado
                    switch (filtro.ToLower())
                    {
                        case "id":

                            // Filtrar por ID
                            var activosFiltrados = from activo in dtActivos.AsEnumerable()
                                                   where activo.Field<int>("ID") == Convert.ToInt32(IdFiltro)
                                                   select activo;

                            dsActivosFiltrados.Tables.Add(activosFiltrados.CopyToDataTable());
                            break;

                        case "estado":

                            // Filtrar por estado
                            activosFiltrados = from activo in dtActivos.AsEnumerable()
                                               where activo.Field<string>("Estado") == ValorFiltro
                                               select activo;

                            dsActivosFiltrados.Tables.Add(activosFiltrados.CopyToDataTable());
                            break;

                        case "condicion":

                            // Filtrar por condicion
                            activosFiltrados = from activo in dtActivos.AsEnumerable()
                                               where activo.Field<string>("Condicion") == ValorFiltro
                                               select activo;

                            dsActivosFiltrados.Tables.Add(activosFiltrados.CopyToDataTable());
                            break;

                        default:

                            // No se especificó un filtro válido
                            throw new Exception("Filtro no válido. Por favor, especifica 'id', 'estado', u otro filtro válido.");
                    }
                }
                else
                {
                    // No existen tablas en el DataSet
                    throw new Exception("No existen tablas en el DataSet proporcionado.");
                }

                return dsActivosFiltrados;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado: " + ex.Message);
            }
        }

        #endregion


        #region "Validaciones de filtrado"

        public void Validaciones()
        {
            if(ValorFiltro == "")
            {
                ValorFiltro = null;
            }
        }

        #endregion
    }
}
