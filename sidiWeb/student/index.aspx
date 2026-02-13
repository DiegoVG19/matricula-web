<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="sidiWeb.index1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="profile-header">
        <asp:Label class="mb-0 fw-bold h2" runat="server" ID="lblEstudiante">NOMBRES Y APELLIDOS DEL ESTUDIANTE</asp:Label>
        <div class="profile-photo-container">
            <div class="profile-photo-placeholder">
                <i class="fas fa-user"></i>
            </div>
        </div>
        <div class="container">
            <div class="row justify-content-end">
                <asp:LinkButton ID="btnEditarPerfil" runat="server" CssClass="btn btn-outline-primary custom-btn col-1 text-white"
                    OnClick="btnActivarCampos_Click">
                    <i class="fas fa-pencil"></i>
                </asp:LinkButton>
            </div>
        </div>
    </div>

    <div class="profile-info">
        <div class="row">
            <div class="col-md-6">
                <div class="info-group">
                    <div class="info-label">N° CARNET</div>

                    <asp:Label ID="lblNCarnet" runat="server" class="info-value fw-bold">IXXXXXXXX</asp:Label>
                    <div class="text-danger small fw-bold">El numero de carnet es USUARIO y CONTRASEÑA para el aula virtual.</div>
                </div>
                <div class="info-group">
                    <div class="info-label">Correo electrónico</div>
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="info-value" Visible="false">correo@gmail.com</asp:TextBox>
                    <asp:Label ID="lblCorreo" runat="server" class="info-value">correo@gmail.com</asp:Label>
                </div>
                <div class="info-group">
                    <div class="info-label">Celular</div>
                    <asp:TextBox ID="txtCelular" runat="server" class="info-value" Visible="false">987654321</asp:TextBox>
                    <asp:Label ID="lblCelular" runat="server" class="info-value">987654321</asp:Label>
                </div>
            </div>
            <div class="col-md-6">
                <div class="info-group">
                    <div class="info-label">N° DNI</div>
                    <asp:Label ID="lblDni" runat="server" class="info-value fw-bold">12345678</asp:Label>
                </div>
                <div class="info-group">
                    <div class="info-label">Fecha de nacimiento</div>
                    <asp:TextBox ID="txtNacimiento" runat="server" TextMode="Date" CssClass="form-control" Visible="false">987654321</asp:TextBox>
                    <asp:Label ID="lblNacimiento" runat="server" class="info-value">XX/XX/XX</asp:Label>
                </div>
                <div class="info-group">
                    <div class="info-label">Género</div>
                    <asp:DropDownList ID="ddlGenero" runat="server" Visible="false" CssClass="form-control">
                        <asp:ListItem Text="MASCULINO" Value="MASCULINO"/>
                        <asp:ListItem Text="FEMENINO" Value="FEMENINO"/>
                    </asp:DropDownList>
                    <asp:Label ID="lblGenero" runat="server" class="info-value">INDEFINIDO</asp:Label>
                </div>
                <div class="info-group">
                    <div class="info-label">Tipo</div>
                    <asp:Label ID="lblTipo" runat="server" class="info-value">FAUSTINIANO/PARTICULAR</asp:Label>
                </div>
            </div>
        </div>
        <div class="d-flex flex-lg-row flex-column gap-2 justify-content-center">
            <asp:LinkButton ID="btnGuardarCambios" runat="server" CssClass="btn btn-primary" Visible="false" OnClick="btnGuardarCambios_Click">
                <i class="fa-solid fa-floppy-disk" style="margin-right:7px"></i>
                    Guardar
            </asp:LinkButton>
            <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-danger" OnClick="btnCancelar_Click" Visible="false">
                <i class="fa-solid fa-ban"></i>
                    Cancelar
            </asp:LinkButton>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script>
    </script>
</asp:Content>
