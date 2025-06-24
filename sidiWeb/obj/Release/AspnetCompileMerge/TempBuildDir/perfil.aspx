<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="perfil.aspx.cs" Inherits="sidiWeb.Perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="profile-header">
    <asp:Label class="mb-0 fw-bold h2" runat="server" ID="lblEstudiante">NOMBRES Y APELLIDOS DEL ESTUDIANTE</asp:Label>
</div>

<div class="profile-info">
    <div class="row">
        <div class="col-md-6">
            <div class="info-group">
                <div class="info-label">Correo electrónico</div>
                <asp:TextBox ID="txtCorreo" runat="server" class="info-value" placeholder="Correo@correo.com"></asp:TextBox>
                <asp:RegularExpressionValidator ID="validateEmail"
                    runat="server" 
                    ErrorMessage="Correo Invalido"
                    ControlToValidate="txtCorreo"
                    ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />
            </div>
            <div class="info-group">
                <div class="info-label">Celular</div>
                <asp:TextBox ID="txtCelular" runat="server" class="info-value" placeholder="#########" MaxLength="9"></asp:TextBox>
                <asp:RegularExpressionValidator ID="validateCellphoneNumber" 
                    runat="server" 
                    ErrorMessage="Solo numeros" 
                    ControlToValidate="txtCelular" 
                    ValidationExpression="\d+"/>
            </div>
            <div class="info-group">
                <div class="info-label">Telefono Fijo</div>
                <asp:TextBox ID="txtTelefono" runat="server" class="info-value" placeholder="#######" MaxLength="7"></asp:TextBox>
                <asp:RegularExpressionValidator ID="validateNumber"
                    runat="server"
                    ErrorMessage="Solo numeros"
                    ControlToValidate="txtTelefono"
                    ValidationExpression="\d+" />
            </div>
            <div class="info-group">
                <div class="info-label">Direccion</div>
                <asp:TextBox ID="txtDireccion" runat="server" class="info-value" placeholder="Direccion ### Jr Av Psj"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-6">
            <div class="info-group">
                <div class="info-label">N° DNI</div>
                <asp:Label ID="lblDni" runat="server" class="info-value fw-bold">12345678</asp:Label>
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
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
