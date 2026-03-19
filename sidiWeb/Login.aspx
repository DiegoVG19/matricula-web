<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="sidiWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Instituto de Idiomas - UNJFSC</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <link href="Resources/loginStyles.css" rel="stylesheet" />

</head>
<body>

    <div class="container-fluid">
        <div class="row">
            <!-- Logo Section -->
            <div class="col-md-6 d-lg-flex d-md-flex d-none align-items-center justify-content-center p-0" style="overflow: hidden; min-height: 100vh;">
                <div class="w-100 h-100">
                    <img src="img/INTRANET.jpeg"
                         id="img-intranet"
                        alt="Instituto de Idiomas"
                        style="width: 100%; height: 100vh; object-fit: cover; display: block;" />
                </div>
            </div>

            <div class="col-md-6 form-section d-flex align-items-start justify-content-center pt-5">
                <div class="login-form w-100" style="max-width: 400px;">
                    <form id="form2" runat="server">

                        <div class="mb-4 mt-0">
                            <div class="text-start mt-0 mb-1">
                                <h1 class="display-5 fw-bold text-white mt-0 pt-0 text-center" style="margin-top: 0 !important; line-height: 1;">INTRANET
                                </h1>
                            </div>
                            <br />

                            <div class="logo-container text-start align-items-center justify-content-center d-flex" style="margin-bottom: -20px;">
                                <img src="img/logo.png"
                                    alt="Logo Idiomas"
                                    class="img-fluid"
                                    style="max-height: 820px; width: auto; display: block;" />
                            </div>
                            <br />
                            <br />

                            <div class="mb-3">
                                <label for="txtUsuario" class="form-label text-white fw-semibold">Usuario</label>
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
                            </div>
                        </div>

                        <div class="mb-4">
                            <label for="txtContrasena" class="form-label text-white fw-semibold">Contraseña</label>
                            <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" CssClass="form-control" placeholder="Contraseña"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvContrasena" runat="server"
                                ControlToValidate="txtContrasena"
                                ErrorMessage="La contraseña es requerida"
                                Display="Dynamic"
                                CssClass="text-warning">
                            </asp:RequiredFieldValidator>
                        </div>

                        <asp:Label ID="lblError" runat="server" class="form-label text-danger fw-bold"></asp:Label>
                        <br />
                        <asp:Button ID="btnIngresar" runat="server" Text="Ingresar"
                            CssClass="btn btn-login w-100" OnClick="btnIngresar_Click" />
                    </form>
                    <br />
                    <div>
                        <a href="https://drive.google.com/file/d/1Ubm-q4tpJSd0_RzgGrJIGZKcgnWJGQiE/view?usp=sharing" target="_blank" class="nav-link text-white text-decoration-underline">Necesito ayuda</a>
                    </div>
                    <br />
                    <div class="social-links text-white">
                        <div class="mb-2 fw-bold">Encuéntranos en:</div>
                        <br />
                        <a href="https://www.facebook.com/IdiomasFaustino" target="_blank" class="text-white me-3">
                            <i class="fab fa-facebook fa-2x"></i>
                        </a>
                        <a href="https://wa.link/zfdbfh" target="_blank" class="text-white">
                            <i class="fab fa-whatsapp fa-2x"></i>
                        </a>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            // Añadir efecto de focus a los campos
            $('.form-control').focus(function () {
                $(this).addClass('focused');
            }).blur(function () {
                $(this).removeClass('focused');
            });
            document.querySelectorAll('[style="position: fixed; z-index: 2147483647; left: 0px; bottom: 0px; height: 65px; right: 0px; display: block; width: 100%; background-color: transparent; margin: 0px; padding: 0px;"], [style="opacity: 0.9; z-index: 2147483647; position: fixed; left: 0px; bottom: 0px; height: 65px; right: 0px; display: block; width: 100%; background-color: #202020; margin: 0px; padding: 0px;"]').forEach(el => {
                el.style.display = 'none';
            });
        });

    </script>
</body>
</html>


