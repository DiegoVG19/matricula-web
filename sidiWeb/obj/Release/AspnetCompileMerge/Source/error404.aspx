<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error404.aspx.cs" Inherits="sidiWeb.error404" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Error 404 - Página no encontrada</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <link href="Resources/err.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Lado izquierdo - Logo e información -->
            <div class="left-side">
                <div class="logo-container">
                    <img src="https://hebbkx1anhila5yf.public.blob.vercel-storage.com/LOGO-IDI-2024-1-Nk57IjbCFsPu1lzetqAZkYHloeG1mi.png" alt="Logo Institucional" class="logo" />
                </div>
                <h1 class="institute-title">INSTITUTO DE IDIOMAS</h1>
                <div class="error-info">
                    <h2 class="error-code">404</h2>
                    <p class="error-message">Página no encontrada</p>
                    <a href="Default.aspx" class="home-button">Volver al inicio</a>
                </div>
            </div>

            <!-- Lado derecho - Mensaje de error -->
            <div class="right-side">
                <div class="error-details">
                    <h2 class="error-title">¡Ups! Algo salió mal</h2>
                    <p class="error-description">
                        La página que estás buscando no existe o ha sido movida. 
                        Por favor, verifica la URL o regresa a la página principal.
                    </p>
                    <div class="divider"></div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
