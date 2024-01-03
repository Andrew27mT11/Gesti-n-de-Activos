<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportacionActivos.aspx.cs" Inherits="SG_ActivosComputacionales.Paginas.ImportacionActivos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Importación de Activos</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.0/css/all.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link href="../content/ImportacionActivos.css" rel="stylesheet" />

    <%-- Notificacciones Toastr --%>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <link href="../content/toastr.css" rel="stylesheet" />
    <script src="../Scripts/toastr.js"></script>
    <script src="../Scripts/notificaciones.js"></script>

    <style>
        html, body {
            height: 85%;
            margin: 0;
            padding: 0;
            overflow: hidden;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <div class="imgSuperior">
            <img id="imgTopWave" src="../Imagenes/svg/top-wave.svg" />
        </div>

        <div class="imgInferior">
            <img id="imgbuttomWave" src="../Imagenes/svg/bottom-wave.svg" />
        </div>
        <div class="contender">
            <div class="content-title">
                <h1>Activos</h1>
            </div>
            <asp:Button ID="btnImportar" runat="server" Text="Importar Datos Manualmente" OnClientClick="return openModal();" /><br />
            <%-- Modal para insertar manualmente --%>

            <div id="myModal" class="modal">
                <div class="modal-content">
                    <span class="close" onclick="closeModal()">&times;</span>
                    <h2>Ingrese los Activos Manualmente</h2>
                    <hr />

                    <asp:Label ID="Label0" runat="server" Text="ID Activo:"></asp:Label><br />
                    <asp:TextBox ID="txtIdActivo" runat="server"></asp:TextBox>
                    <br />
                    <br />

                    <asp:Label ID="Label1" runat="server" Text="Categoría:"></asp:Label><br />
                    <asp:DropDownList ID="ddlCategorias" runat="server">
                        <asp:ListItem>Computadora</asp:ListItem>
                    </asp:DropDownList><br />
                    <br />

                    <asp:Label ID="Label2" runat="server" Text="Marca:"></asp:Label><br />
                    <asp:DropDownList ID="ddlMarca" runat="server">
                        <asp:ListItem>Lenovo</asp:ListItem>
                    </asp:DropDownList><br />
                    <br />

                    <asp:Label ID="Label3" runat="server" Text="Modelo:"></asp:Label><br />
                    <asp:TextBox ID="txtModelo" runat="server"></asp:TextBox><br />
                    <br />

                    <asp:Label ID="Label4" runat="server" Text="Estado:"></asp:Label><br />
                    <asp:DropDownList ID="ddlEstado" runat="server">
                        <asp:ListItem>Activo</asp:ListItem>
                        <asp:ListItem>Desecho</asp:ListItem>
                        <asp:ListItem>Reubicado</asp:ListItem>
                    </asp:DropDownList><br />
                    <br />

                    <asp:Label ID="Label5" runat="server" Text="Condición:"></asp:Label><br />
                    <asp:DropDownList ID="ddlCondicion" runat="server">
                        <asp:ListItem>Bueno</asp:ListItem>
                        <asp:ListItem>Desecho</asp:ListItem>
                        <asp:ListItem>Regular</asp:ListItem>
                    </asp:DropDownList><br />
                    <br />

                    <asp:Label ID="Label6" runat="server" Text="Departamento:"></asp:Label><br />
                    <asp:DropDownList ID="ddlDepartamento" runat="server">
                        <asp:ListItem>TI</asp:ListItem>
                    </asp:DropDownList><br />
                    <br />


                    <asp:Label ID="Label7" runat="server" Text="Área:"></asp:Label><br />
                    <asp:DropDownList ID="ddlArea" runat="server">
                        <asp:ListItem>Informatica</asp:ListItem>
                    </asp:DropDownList><br />
                    <br />

                    <asp:Label ID="Label8" runat="server" Text="Dirección Exacta:"></asp:Label><br />
                    <asp:TextBox ID="txtDireccionExacta" runat="server"></asp:TextBox><br />
                    <br />

                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn-guardar" OnClick="btnGuardar_Click" />

                </div>
            </div>


            <script>
                // Función para abrir el modal
                function openModal() {
                    document.getElementById('myModal').style.display = 'block';
                    return false; // Evita que la página se recargue
                }

                // Función para cerrar el modal
                function closeModal() {
                    document.getElementById('myModal').style.display = 'none';
                }

                // Cierra el modal si se hace clic fuera de él
                window.onclick = function (event) {
                    if (event.target === document.getElementById('myModal')) {
                        closeModal();
                    }
                };

            </script>
            <hr />
            <asp:FileUpload ID="SubirActivos" runat="server" accept=".csv" />
            <asp:Button ID="btnCargarActivos" runat="server" Text="Cargar Activos" OnClick="btnCargarActivos_Click" />
            <asp:GridView ID="gvActivos" runat="server"></asp:GridView>
            <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click"></asp:Button>
        </div>

    </form>
</body>
</html>
