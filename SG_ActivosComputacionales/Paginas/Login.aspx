<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SG_ActivosComputacionales.Paginas.Login" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Login</title>
    <link rel="stylesheet" href="../CSS/EstilosLogin.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
    <script src="../Scripts/script_LoginVista.js"></script>
</head>
<body>
    <form runat="server">
        <div id="wave-container">
            <img id="imgTopWave" src="../Imagenes/svg/top-wave.svg" />
        </div>
        <div class="cotn_principal">
            <div class="cont_centrar">
                <div class="cont_login">
                    <div class="cont_info_log_sign_up">
                        <div class="col_md_login">
                            <div class="cont_ba_opcitiy">
                                <h2>LOGUEARSE</h2>
                                <p>Bienvenido pulsa Loguearse para ingresar a la pagina de Gestion de Activos.</p>
                                <asp:Button runat="server" ID="btnLogin" CssClass="btn_login" Text="Loguearse" OnClientClick="change_to_login(); return false;" UseSubmitBehavior="false" />
                                <span id="errorMensajeL" class="error-message" runat="server"></span>
                            </div>
                        </div>
                        <div class="col_md_sign_up">
                            <div class="cont_ba_opcitiy">
                                <h2>REGISTRARSE</h2>
                                <p>Si no tienes aun una cuenta en nuestra pagina ingresa en REGISTRARSE.</p>
                                <asp:Button runat="server" ID="btnSignUp" CssClass="btn_sign_up" Text="REGISTRARSE" OnClientClick="change_to_sign_up(); return false;" UseSubmitBehavior="false" />
                                <span id="errorMensajeR" class="error-message" runat="server"></span>
                            </div>
                        </div>
                    </div>
                    <div class="cont_back_info">
                    </div>
                    <div class="cont_forms">
                        <div class="cont_img_back_">
                        </div>
                        <div class="cont_form_login">
                            <a href="#" onclick="hidden_login_and_sign_up()">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-caret-left-fill" viewBox="0 0 16 16">
                                    <path d="m3.86 8.753 5.482 4.796c.646.566 1.658.106 1.658-.753V3.204a1 1 0 0 0-1.659-.753l-5.48 4.796a1 1 0 0 0 0 1.506z" />
                                </svg>
                            </a>
                            <h2>Loguearse</h2>
                            <asp:TextBox runat="server" ID="txtLoginUsuario" placeholder="Usuario" />
                            <asp:TextBox runat="server" ID="txtLoginPassword" TextMode="Password" placeholder="Password" />
                            <asp:Button runat="server" ID="btnLoginSubmit" CssClass="btn_login" Text="Loguearse" OnClick="btnLoginSubmit_Click" />
                        </div>
                        <div class="cont_form_sign_up">
                            <a href="#" onclick="hidden_login_and_sign_up()">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-caret-left-fill" viewBox="0 0 16 16">
                                    <path d="m3.86 8.753 5.482 4.796c.646.566 1.658.106 1.658-.753V3.204a1 1 0 0 0-1.659-.753l-5.48 4.796a1 1 0 0 0 0 1.506z" />
                                </svg>
                            </a>
                            <h2>Registro</h2>
                            <asp:TextBox runat="server" ID="txtRegistroCedula" placeholder="Cedula" />
                            <asp:TextBox runat="server" ID="txtRegistroNombre" placeholder="Nombre" />
                            <asp:TextBox runat="server" ID="txtRegistroApellido" placeholder="Apellido" />
                            <asp:TextBox runat="server" ID="txtRegistroUsuario" placeholder="Usuario" />
                            <asp:TextBox runat="server" ID="txtRegistroPassword" TextMode="Password" placeholder="Password" />
                            <asp:TextBox runat="server" ID="txtSignUpConfirmPassword" TextMode="Password" placeholder="Confirm Password" />
                            <asp:Button runat="server" ID="btnSignUpSubmit" CssClass="btn_sign_up" Text="Registrarse" OnClick="btnSignUpSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
