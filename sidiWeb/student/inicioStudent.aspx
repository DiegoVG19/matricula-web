<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="inicioStudent.aspx.cs" Inherits="sidiWeb.inicioStudent" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="/Resources/inicioStudent.css"/>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class=" container d-flex flex-column gap-3">
            <div class="d-flex center-y flex-sm-column flex-lg-row gap-3">
                <div class="card col-lg-6 col-sm-12"><span>Bienvenid@, Ricardo Victorio</span></div>
                <div class="card col-lg-6 col-sm-12"><span>Próximas Clases</span></div>
            </div>
            <div class="d-flex center-y flex-sm-column flex-lg-row gap-3">
                <div class="card col-lg-6 col-sm-12"><span>Mis Cursos</span></div>
                <div class="card col-lg-6 col-sm-12"><span>Mi Progreso</span></div>
            </div>
            <div class="d-flex center-y flex-sm-column flex-lg-row gap-3">
                <div class="card col-lg-8 col-sm-12"><span>Novedades y Avisos</span></div>
                <div class="card col-lg-4 col-sm-12"><span>Recursos Rápidos</span></div>
            </div>
        </div>
    </asp:Content>


    <asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
        <script type="text/javascript">
        </script>
    </asp:Content>