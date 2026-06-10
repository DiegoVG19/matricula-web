<%@ Page Title="Matrícula Virtual" Language="C#" MasterPageFile="~/SiteStudent.Master" AutoEventWireup="true" CodeBehind="matricula.aspx.cs" Inherits="sidiWeb.student.matricula" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/matricula.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="card shadow-sm p-4" style="border-radius:15px;">

            <div class="d-flex justify-content-between align-items-center mb-4">
                <h2 class="text-primary mb-0">
                    <i class="fas fa-graduation-cap"></i> MATRÍCULA VIRTUAL
                </h2>
                
                <div>
                    <asp:Label ID="lblNombreAlumno" runat="server" CssClass="fw-bold text-secondary me-3"></asp:Label>
                    <asp:LinkButton ID="btnExportarPDF" runat="server" CssClass="btn btn-outline-primary" OnClientClick="exportarTablaPDF(); return false;">
                        <i class="fas fa-file-pdf"></i> Exportar a PDF
                    </asp:LinkButton>
                </div>
            </div>

            <hr />

            <%-- TABLA DE ESTADO ACTUAL --%>
            <div class="table-responsive mb-4">
                <table class="table table-bordered table-hover" id="tablaMatricula">
                    <thead class="table-primary">
                        <tr>
                            <th scope="col">Idioma</th>
                            <th scope="col">Ciclo</th>
                            <th scope="col">Nivel</th>
                            <th scope="col">Vez</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Literal ID="litFilasTabla" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>

        </div>
    </div>

    <%-- MODAL DE NOTIFICACIÓN DE MATRÍCULA CENTRAL --%>
    <div class="modal fade" id="modalAvisoMatricula" tabindex="-1" aria-labelledby="modalMatriculaLabel" aria-hidden="true" data-bs-backdrop="static">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content" style="border-radius: 15px; border: none; box-shadow: 0 10px 30px rgba(0,0,0,0.2);">
                
                <div class="modal-header bg-primary text-white" style="border-top-left-radius: 15px; border-top-right-radius: 15px;">
                    <h5 class="modal-title" id="modalMatriculaLabel"><i class="fas fa-bullhorn me-2"></i> Aviso Académico</h5>
                    <%-- Nota: Si usas Bootstrap 4, cambia 'data-bs-dismiss' por 'data-dismiss' --%>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                
                <div class="modal-body text-center p-4">
                    <i class="fas fa-calendar-check fa-4x text-primary mb-3"></i>
                    <h3 class="mb-3 text-dark">¡El proceso de matrícula está abierto!</h3>
                    <p class="text-muted lead">Asegura tu vacante para el siguiente ciclo académico. ¿Deseas iniciar tu proceso de matrícula ahora?</p>
                </div>
                
                <div class="modal-footer justify-content-center border-0 pb-4">
                    <button type="button" class="btn btn-outline-secondary px-4" data-bs-dismiss="modal">Cerrar</button>
                    
                    <%-- Este es tu botón C# que redirige al proceso real --%>
                    <asp:LinkButton ID="btnIniciarMatricula" runat="server" CssClass="btn btn-primary px-4 shadow" OnClick="btnIniciarMatricula_Click">
                        Aceptar e Iniciar <i class="fas fa-arrow-right ms-2"></i>
                    </asp:LinkButton>
                </div>

            </div>
        </div>
    </div>

    <script src="../Scripts/matricula.js" type="text/javascript"></script>

    <%-- SCRIPT TEMPORAL PARA FORZAR LA APERTURA DEL MODAL --%>
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function() {
            // Inicializa y muestra el modal al cargar la página
            // Cambia 'bootstrap.Modal' a '$(...)modal()' si usas una versión muy antigua de Bootstrap basada en jQuery.
            var miModal = new bootstrap.Modal(document.getElementById('modalAvisoMatricula'));
            miModal.show();
        });
    </script>
</asp:Content>