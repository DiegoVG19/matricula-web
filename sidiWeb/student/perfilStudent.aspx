<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true"
    CodeBehind="perfilStudent.aspx.cs" Inherits="sidiWeb.perfilStudent" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="/Resources/elementosComunes.css">
        <link rel="stylesheet" href="/Resources/perfilStudent.css">
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="d-flex flex-sm-column flex-lg-row h-100 gap-4">
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
                    CssClass="btn btn-2 fw-bold col-6 btn-animated" OnClick="btnActivarCampos_Click">
                    <i class="fas fa-pencil"></i> Editar Perfil
                </asp:LinkButton>
            </div>

            <div class=" card profile-info col-lg-4 col-sm-12">
                <div class="row h-100">
                    <div class="col-md-12">
                        <span class="card-title mb-3 fw-bold text-secondary">Información Personal</span>
                        <div class="info-group">
                            <div class="info-label fw-bold">N° CARNET</div>
                            <div class="w-100">
                            <asp:Label ID="lblNCarnet" runat="server" CssClass="info-value w-100">IXXXXXXXX</asp:Label>
                                </div>
                        </div>
                        <div class="info-group">
                            <div class="info-label fw-bold">Correo electrónico</div>
                            <div class="w-100">
                            <asp:TextBox ID="txtCorreo" runat="server" CssClass="info-value" Visible="false">
                                correo@gmail.com</asp:TextBox>
                            <asp:Label ID="lblCorreo" runat="server" class="info-value">correo@gmail.com</asp:Label>
                                </div>
                        </div>
                        <div class="info-group">
                            <div class="info-label fw-bold">Celular</div>
                            <div class="w-100">
                            <asp:TextBox ID="txtCelular" runat="server" class="info-value" Visible="false">987654321
                            </asp:TextBox>
                            <asp:Label ID="lblCelular" runat="server" class="info-value">987654321</asp:Label>
                                </div>
                        </div>
                        <div class="info-group">
                            <div class="info-label fw-bold">N° DNI </div>
                            <div class="w-100 d-flex flex-column">
                                <asp:Label ID="lblDni" runat="server" class="info-value"> 12345678</asp:Label>
                                <div class="text-danger small fw-bold">
                                    El numero de DNI es USUARIO y CONTRASEÑA para el aula virtual.
                                </div>
                            </div>
                        </div>
                        <div class="info-group">
                            <div class="info-label fw-bold">Fecha de nacimiento</div>
                            <div class="w-100">
                            <asp:Label ID="lblNacimiento" runat="server" class="info-value">XX/XX/XX</asp:Label>
                                </div>
                        </div>
                        <div class="info-group">
                            <div class="info-label fw-bold">Género</div>
                            <div class="w-100">
                            <asp:Label ID="lblGenero" runat="server" class="info-value">INDEFINIDO</asp:Label>
                                </div>
                        </div>
                        <div class="info-group">
                            <div class="info-label fw-bold  ">Tipo</div>
                            <div class="w-100">
                            <asp:Label ID="lblTipo" runat="server" class="info-value">FAUSTINIANO/PARTICULAR</asp:Label>
                                </div>
                        </div>
                    </div>
                
                    <div class="d-flex flex-lg-row flex-column gap-2 justify-content-center w-100">
                        <asp:LinkButton ID="btnGuardarCambios" runat="server" CssClass="btn btn-1 fw-bold btn-animated" Visible="false"
                            OnClick="btnGuardarCambios_Click">
                        <i class="fa-solid fa-floppy-disk"></i>
                        Guardar
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-danger fw-bold btn-animated"
                            OnClick="btnCancelar_Click" Visible="false">
                        <i class="fa-solid fa-ban"></i>
                        Cancelar
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="card col-lg-4 col-sm-12">
                <span class="card-title mb-3 fw-bold text-secondary">Idiomas en Estudio</span>
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