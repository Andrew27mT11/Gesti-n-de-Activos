using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Datos.Base_de_datos;

namespace Negocio.InclusionArchivos
{
    public class InclusionActivos
    {
        ConexionBD conexionBD = new ConexionBD();
        int _idActivo = 0;
        string _Categoria = "", _Marca = "", _Modelo = "", _Estado = "", _Condicion = "", _Departamento = "", _Area = "", _DireccionE = "";

        #region Propiedades
        public int idActivo { get => _idActivo; set => _idActivo = value; }
        public string Categoria { get => _Categoria; set => _Categoria = value; }
        public string Marca { get => _Marca; set => _Marca = value; }
        public string Modelo { get => _Modelo; set => _Modelo = value; }
        public string Estado { get => _Estado; set => _Estado = value; }
        public string Condicion { get => _Condicion; set => _Condicion = value; }
        public string Departamento { get => _Departamento; set => _Departamento = value; }
        public string Area { get => _Area; set => _Area = value; }
        public string DireccionE { get => _DireccionE; set => _DireccionE = value; }
        #endregion

        #region Metodos
        public void AgregarActivoDesdeCSV(DataTable dt, out DataTable errores)
        {
            errores = new DataTable();
            errores.Columns.Add("idActivo", typeof(int));
            errores.Columns.Add("Categoria", typeof(string));
            errores.Columns.Add("Marca", typeof(string));
            errores.Columns.Add("Modelo", typeof(string));
            errores.Columns.Add("Estado", typeof(string));
            errores.Columns.Add("Condicion", typeof(string));
            errores.Columns.Add("Departamento", typeof(string));
            errores.Columns.Add("Area", typeof(string));
            errores.Columns.Add("DireccionExacta", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    idActivo = Convert.ToInt32(row["idActivo"]);
                    Categoria = Convert.ToString(row["Categoria"]);
                    Marca = Convert.ToString(row["Marca"]);
                    Modelo = Convert.ToString(row["Modelo"]);
                    Estado = Convert.ToString(row["Estado"]);
                    Condicion = Convert.ToString(row["Condicion"]);
                    Departamento = Convert.ToString(row["Departamento"]);
                    Area = Convert.ToString(row["Area"]);
                    DireccionE = Convert.ToString(row["DireccionExacta"]);

                    ValidarDatos();
                    AgregarActivo();
                }
                catch (Exception ex)
                {
                    DataRow errorRow = errores.NewRow();
                    errorRow["idActivo"] = idActivo;
                    errorRow["Categoria"] = Categoria;
                    errorRow["Marca"] = Marca;
                    errorRow["Modelo"] = Modelo;
                    errorRow["Estado"] = Estado;
                    errorRow["Condicion"] = Condicion;
                    errorRow["Departamento"] = Departamento;
                    errorRow["Area"] = Area;
                    errorRow["DireccionExacta"] = DireccionE;
                    errores.Rows.Add(errorRow);

                    Console.WriteLine($"Error al procesar la línea del archivo CSV: {ex.Message}");
                }
            }
        }
        public void AgregarActivo()
        {
            conexionBD.InsertarActivo(idActivo, Categoria, Marca, Modelo, Estado, Condicion, Departamento, Area, DireccionE);
        }
        public void ValidarDatos()
        {
            // Validación de espacios vacíos
            if (string.IsNullOrEmpty(idActivo.ToString()) || string.IsNullOrWhiteSpace(Categoria) || string.IsNullOrWhiteSpace(Marca) || string.IsNullOrWhiteSpace(Modelo) ||
                string.IsNullOrWhiteSpace(Estado) || string.IsNullOrWhiteSpace(Condicion) || string.IsNullOrWhiteSpace(Departamento) ||
                string.IsNullOrWhiteSpace(Area) || string.IsNullOrWhiteSpace(DireccionE))
            {
                throw new Exception("Ningún campo puede estar vacío.");

            }

            // Validación de solo números en idActivo
            if (!int.TryParse(idActivo.ToString(), out _))
            {
                throw new Exception("El ID del activo debe ser un número entero.");
            }

            // Validación de no números en Marca, Estado, Condicion, Departamento y Area
            if (ContieneNumeros(Marca) || ContieneNumeros(Estado) || ContieneNumeros(Condicion) ||
                ContieneNumeros(Departamento) || ContieneNumeros(Area))
            {
                throw new Exception("Marca, Estado, Condicion, Departamento y Area no deben contener números.");
            }
        }

        private bool ContieneNumeros(string input)
        {
            // Verifica si la cadena contiene algún dígito
            return input.Any(char.IsDigit);
        }


        #endregion
    }
}