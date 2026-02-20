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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
