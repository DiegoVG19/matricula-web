<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="sidiWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Instituto de Idiomas - UNJFSC</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <link href="Resources/loginStyles.css" rel="stylesheet" />

</head>
<body>

    <div class="container-fluid">
        <div class="row">
            <!-- Logo Section -->
            <div class="col-md-6 logo-section d-flex align-items-center justify-content-center">
                <div class="logo-container text-center">
                    <img src="https://hebbkx1anhila5yf.public.blob.vercel-storage.com/LOGO-IDI-2024-1-Nk57IjbCFsPu1lzetqAZkYHloeG1mi.png" alt="Instituto de Idiomas Logo" class="img-fluid mb-4" />
                    <h1 class="institute-name">Instituto de Idiomas</h1>
                </div>
            </div>

            <!-- Form Section -->
            <div class="col-md-6 form-section d-flex align-items-center justify-content-center">
                <div class="login-form w-100" style="max-width: 400px;">
                    <form id="form2" runat="server">
                        <div class="mb-4">
                            <label for="txtUsuario" class="form-label text-white fw-semibold">Usuario</label>
                            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="DNI"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" 
                                ControlToValidate="txtUsuario" 
                                ErrorMessage="El usuario es requerido" 
                                Display="Dynamic" 
                                CssClass="text-warning">
                            </asp:RequiredFieldValidator>
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
                        <div>
                            <asp:label ID="lblError" runat="server" class="form-label text-red fw-bold"></asp:label>
                        </div>
                        <br />
                        <asp:Button ID="btnIngresar" runat="server" Text="Aceptar" 
                            CssClass="btn btn-login w-100" OnClick="btnIngresar_Click"/>
                    </form>
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
        });
    </script>

</body>
</html>


