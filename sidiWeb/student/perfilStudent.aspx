<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true"
    CodeBehind="perfilStudent.aspx.cs" Inherits="sidiWeb.perfilStudent" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="/Resources/elementosComunes.css">
        <link rel="stylesheet" href="/Resources/perfilStudent.css">
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="d-flex flex-sm-column flex-lg-row h-100 center-x gap-4">
            <div class="profile-header d-flex card center-x center-y gap-4">

                <%--<div class="profile-photo-container">--%>
                <div class="text-center">
                   <img class="foto-perfil" src="/img/prueba-foto-perfil.png" alt="">
                    <%--<div class="profile-photo-placeholder">
                        <i class="fas fa-user"></i>
                    </div>--%>
                </div>

                <asp:Label CssClass="d-block mb-0 fw-bold h2 text-center" runat="server" ID="lblEstudiante">NOMBRES Y
                    APELLIDOS DEL ESTUDIANTE
                </asp:Label>

                <asp:LinkButton ID="btnEditarPerfil" runat="server"
                    CssClass="btn btn-outline-primary custom-btn text-white" OnClick="btnActivarCampos_Click">
                    <i class="fas fa-pencil"></i> Editar Perfil
                </asp:LinkButton>
            </div>

            <div class=" card profile-info h-75">
                <div class="row">
                    <div class="col-md-6">
                        <div class="info-group">
                            <div class="info-label">N° CARNET</div>

                            <asp:Label ID="lblNCarnet" runat="server" class="info-value fw-bold">IXXXXXXXX</asp:Label>

                        </div>
                        <div class="info-group">
                            <div class="info-label">Correo electrónico</div>
                            <asp:TextBox ID="txtCorreo" runat="server" CssClass="info-value" Visible="false">
                                correo@gmail.com</asp:TextBox>
                            <asp:Label ID="lblCorreo" runat="server" class="info-value">correo@gmail.com</asp:Label>
                        </div>
                        <div class="info-group">
                            <div class="info-label">Celular</div>
                            <asp:TextBox ID="txtCelular" runat="server" class="info-value" Visible="false">987654321
                            </asp:TextBox>
                            <asp:Label ID="lblCelular" runat="server" class="info-value">987654321</asp:Label>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="info-group">
                            <div class="info-label">N° DNI </div>
                            <asp:Label ID="lblDni" runat="server" class="info-value fw-bold"> 12345678</asp:Label>
                            <div class="text-danger small fw-bold"> El numero de DNI es USUARIO y CONTRASEÑA para el
                                aula
                                virtual.</div>
                        </div>
                        <div class="info-group">
                            <div class="info-label">Fecha de nacimiento</div>
                            <asp:Label ID="lblNacimiento" runat="server" class="info-value">XX/XX/XX</asp:Label>
                        </div>
                        <div class="info-group">
                            <div class="info-label">Género</div>
                            <asp:Label ID="lblGenero" runat="server" class="info-value">INDEFINIDO</asp:Label>
                        </div>
                        <div class="info-group">
                            <div class="info-label">Tipo</div>
                            <asp:Label ID="lblTipo" runat="server" class="info-value">FAUSTINIANO/PARTICULAR</asp:Label>
                        </div>
                    </div>
                </div>
                <div class="d-flex flex-lg-row flex-column gap-2 justify-content-center">
                    <asp:LinkButton ID="btnGuardarCambios" runat="server" CssClass="btn btn-primary" Visible="false"
                        OnClick="btnGuardarCambios_Click">
                        <i class="fa-solid fa-floppy-disk" style="margin-right:7px"></i>
                        Guardar
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-danger"
                        OnClick="btnCancelar_Click" Visible="false">
                        <i class="fa-solid fa-ban"></i>
                        Cancelar
                    </asp:LinkButton>
                </div>
            </div>
        </div>



        <div class="modal fade" id="modalAviso" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" style="max-width: 800px;">
                <div class="modal-content border-0">
                    <div class="modal-header text-white border-0" style="background-color: #003366;">
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"
                            aria-label="Close"></button>
                    </div>
                    <div class="modal-body p-0" style="height: 450px; overflow: hidden;">
                        <img src="<%= UrlImagenJS %>" class="w-100 h-100" style="object-fit: fill;" alt="Aviso">
                    </div>
                </div>
            </div>
        </div>
        <script>
            $(document).ready(function () {
                var strInicio = "<%= FechaInicioJS %>";
                var strFin = "<%= FechaFinJS %>";
                var urlImg = "<%= UrlImagenJS %>";

                if (strInicio && strFin && urlImg) {
                    var hoy = new Date();
                    var fechaInicio = new Date(strInicio + "T00:00:00");
                    var fechaFin = new Date(strFin + "T23:59:59");

                    if (hoy >= fechaInicio && hoy <= fechaFin) {
                        var imgPrueba = new Image();
                        imgPrueba.src = urlImg;

                        imgPrueba.onload = function () {
                            // Ahora sí, el script busca el ID "modalAviso" y lo muestra
                            var myModal = new bootstrap.Modal(document.getElementById('modalAviso'));
                            myModal.show();
                        };

                        imgPrueba.onerror = function () {
                            console.error("El link de la imagen en el CSV está roto.");
                        };
                    }
                }
            });
        </script>
    </asp:Content>
    <asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
        <script>
        </script>
    </asp:Content>