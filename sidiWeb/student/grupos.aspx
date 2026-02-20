<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="grupos.aspx.cs" Inherits="sidiWeb.grupos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grades-header">
        <h2 class="grades-title">
            <i class="fas fa-star me-2"></i>
            LISTAR GRUPOS
        </h2>
    </div>

    <div class="grades-container">
        <div class="language-selector">
            <label for="ddlIdiomas" class="form-label">Seleccione el idioma:</label>
            <asp:DropDownList ID="ddlIdiomas" runat="server" CssClass="form-select"
                AutoPostBack="true" OnSelectedIndexChanged="ddlIdiomas_SelectedIndexChanged">
                <asp:ListItem Text="Seleccione..." Value="" />
            </asp:DropDownList>
        </div>

        <div class="groups-container mt-4">
            <asp:Panel ID="pnlGrupos" runat="server" CssClass="groups-grid">
                <!-- Los botones se generarán dinámicamente -->
            </asp:Panel>
        </div>
    </div>
      <div class="modal fade" id="modalInfoGrupo" tabindex="-1" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" style="max-width: 800px;"> 
        <div class="modal-content border-0">
            <div class="modal-header text-white border-0" style="background-color: #003366;">
                <h5 class="section-title"><i class="fas fa-users"></i>Información del Grupo</h5>
                <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body p-0" style="height: 450px; overflow: hidden;">
                <div class="card p-4 shadow-sm">
                    
                    <div class="group-info-grid">
                        <div>
                            <strong>NÚMERO:</strong>
                            <asp:Label ID="lblNumero" runat="server"></asp:Label>
                        </div>
                        <div>
                            <strong>IDIOMA:</strong>
                            <asp:Label ID="lblIdioma" runat="server"></asp:Label>
                        </div>
                        <div>
                            <strong>NIVEL:</strong>
                            <asp:Label ID="lblNivel" runat="server"></asp:Label>
                        </div>
                        <div>
                            <strong>CICLO:</strong>
                            <asp:Label ID="lblCiclo" runat="server"></asp:Label>
                        </div>
                        <div>
                            <strong>MODALIDAD:</strong>
                            <asp:Label ID="lblModalidad" runat="server"></asp:Label>
                        </div>
                        <div>
                            <strong>HORARIO:</strong>
                            <asp:Label ID="lblHorario" runat="server"></asp:Label>
                        </div>
                        <div class="full-width">
                            <strong>DÍAS:</strong>
                            <asp:Label ID="lblDias" runat="server"></asp:Label>
                        </div>
                        <div>
                            <strong>DURACIÓN:</strong>
                            <asp:Label ID="lblDuracion" runat="server"></asp:Label>
                        </div>
                        <div class="full-width">
                            <strong>DOCENTE:</strong>
                            <asp:Label ID="lblDocente" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        //$(document).ready(function () {
        //    // Ahora sí, el script busca el ID "modalAviso" y lo muestra
        //    var myModal = new bootstrap.Modal(document.getElementById('modalInfoGrupo'));
        //    myModal.show();
        //});
    </script>
</asp:Content>
