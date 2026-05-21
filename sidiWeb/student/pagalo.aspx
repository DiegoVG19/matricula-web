<%@ Page Title="Pagos en Línea" Language="C#" MasterPageFile="~/SiteStudent.Master" AutoEventWireup="true" CodeBehind="pagalo.aspx.cs" Inherits="sidiWeb.student.pagalo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="card shadow-sm border-0" style="border-radius: 10px;">
            <div class="card-body text-center p-5">
                <img src="https://th.bing.com/th/id/OIP.Gp0TcTzTuf9rLbnnijTEbAHaHa?w=156&h=180&c=7&r=0&o=7&pid=1.7&rm=3" alt="Págalo.pe" style="max-width: 100px;" class="mb-4" />
                
                <h2 class="text-primary font-weight-bold">Plataforma Págalo.pe</h2>
                <p class="lead text-muted">Realiza el pago de tus tasas académicas de forma rápida y segura desde el Banco de la Nación.</p>
                

                <div class="mt-4">
                    <a href="https://pagalo.pe/" target="_blank" class="btn btn-primary btn-lg px-5 shadow">
                        <i class="fas fa-external-link-alt"></i> Ir a Págalo.pe
                    </a>
                </div>

                <div class="mt-4">
                    <a href="https://youtu.be/xNtpjgBk8es" target="_blank" class="text-info">
                        <i class="fab fa-youtube"></i> Ver video tutorial de cómo pagar
                    </a>
                </div>
                <div class="alert alert-warning d-inline-block mt-3">
    <i class="fas fa-exclamation-triangle"></i> <strong>Recuerda:</strong> Una vez realizado el pago, debes subir la foto de tu voucher en el <b>Siguiente Enlace</b>.
</div>
                <div class="mt-4">
    <a href="https://forms.gle/ZViUhWgg48hsL7T16" target="_blank" class="btn btn-primary btn-lg px-5 shadow">
        <i class="fas fa-external-link-alt"></i> Matrículas para GRUPOS EMPEZADOS
    </a>
</div>
            </div>
        </div>
    </div>
</asp:Content>