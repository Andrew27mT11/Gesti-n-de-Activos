<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/MasterPage.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="SG_ActivosComputacionales.Paginas.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <! ––«Estilos para imprimir«––>
        <style>
            @media print {
                @page {
                    size: legal;
                    size: landscape;
                }
            }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdPanel_Reportes" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <img id="imgTopWave" src="../Imagenes/svg/top-wave.svg"/>

            <div style="margin: 2rem;"></div>
            <div class="container box">
                <!-- Cada reporte debe mostrar datos sobre el día de la generación -->
                <div id="div_datos_reporte_Activos" style="background-color:#176B87;">
                    <br />
                    <h1 class="title is-4 has-text-centered" style="color:#EEEEEE;">RESUMEN DE REPORTE</h1>
                    <hr style="border: none; border-top: 1px solid #ededed; margin-bottom: 20px; margin-top: 20px; width: 100%;">
                </div>

                <div class="columns">
                    <div class="column is-one-quarter">
                        <!-- Contenido de la tarjeta de filtros -->
                        <div class="card">
                            <div class="card-content">
                                <h2 class="subtitle">Filtros</h2>
                                <hr style="border: none; border-top: 1px solid #ededed; margin-bottom: 20px; margin-top: 20px; width: 100%;">
                                <form>
                                    <div class="field">
                                        <label class="label">Tipo de filtro:</label>
                                        <div class="control">
                                            <asp:DropDownList runat="server" ID="ddlTipoFiltro" CssClass="select is-rounded" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoFiltro_SelectedIndexChanged">
                                                <asp:ListItem Text="Seleccione un filtro" Value="default"></asp:ListItem>
                                                <asp:ListItem Text="ID" Value="ID"></asp:ListItem>
                                                <asp:ListItem Text="Estado" Value="Estado"></asp:ListItem>
                                                <asp:ListItem Text="Condicion" Value="Condicion"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="field" runat="server" id="divddlValorFiltro" visible="false">
                                        <label class="label">Valor del filtro a aplicar según tipo:</label>
                                        <div class="control">
                                            <asp:DropDownList runat="server" ID="ddlValorFiltro" CssClass="select is-rounded" AutoPostBack="true" OnSelectedIndexChanged="ddlValorFiltro_SelectedIndexChanged" ></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="field" runat="server" id="divtxtIDFiltro" visible="false">
                                        <div class="control">
                                            <asp:TextBox runat="server" ID="txtIDFiltro" CssClass="input" placeholder="Ingrese el ID para filtrar"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="field" id="divBtnAplicarFiltros" runat="server" visible="false">
                                        <div class="control">
                                            <asp:Button runat="server" id="btnAplicarFiltro" class="button is-info" Text="Aplicar filtros" style="max-width: 200px; background-color: #053B50;" OnClick="btnAplicarFiltro_Click"></asp:Button>
                                        </div>
                                    </div>

                                    <div class="field">
                                        <div class="control">
                                            <button runat="server" id="btnImprimirPagina" class="button is-info" style="max-width: 200px; background-color: #053B50;" onclick="mostrarOcultarSVGs(); window.print(); mostrarNotificacionExito('PDF creado con éxito')">Generar reporte PDF</button>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                    <div class="column is-one-quarter">

                        <div style="z-index: 1;">
                        <!-- Contenido del GridView -->
                        <asp:GridView runat="server" ID="grdReportes" CssClass="table center mx-auto" Style="border: 0px; border-collapse: collapse;"></asp:GridView>

                        </div>

                        <!-- SVG en la esquina inferior derecha -->
                        <div style="position: absolute; bottom: 10px; right: 10px; z-index: 0;">
                            <!-- Reemplaza este SVG con el tuyo -->
                            <img src="../Imagenes/svg/undraw_file_manager_re_ms29.svg "width="150" height="150"></img>
                        </div>
                    </div>
                </div>
            </div>

            <img id="imgBottomWave" src="../Imagenes/svg/bottom-wave.svg" />

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">

        function mostrarOcultarSVGs() {

            var topWave = document.getElementById("imgTopWave");
            var bottomWave = document.getElementById("imgBottomWave");

            if (topWave.style.display === "none" && bottomWave.style.display === "none") {
                topWave.style.display = "block";
                bottomWave.style.display = "block";
            }
            else {
                topWave.style.display = "none";
                bottomWave.style.display = "none";
            }
        }

    </script>


</asp:Content>

