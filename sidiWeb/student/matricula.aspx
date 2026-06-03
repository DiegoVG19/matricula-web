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

    <script src="../Scripts/matricula.js" type="text/javascript"></script>
</asp:Content>