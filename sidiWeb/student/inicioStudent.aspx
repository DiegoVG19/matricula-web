<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="inicioStudent.aspx.cs" Inherits="sidiWeb.inicioStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/Resources/inicioStudent.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-body d-flex flex-column gap-3">
        <div class="d-flex center-y flex-sm-column flex-lg-row gap-3">
            <div class="card col-lg-6 col-sm-12"><span>Bienvenid@, Ricardo Victorio</span></div>
            <div class="card col-lg-6 col-sm-12"><span>Próximas Clases</span></div>
        </div>
        <div class="d-flex center-y flex-sm-column flex-lg-row gap-3 w-100">
            <div class="card col-lg-6 col-sm-12"><span>Mis Cursos</span></div>
            <div class="card col-lg-6 col-sm-12"><span>Mi Progreso</span></div>
        </div>
        <div class="d-flex center-y flex-sm-column flex-lg-row gap-3 w-100">

            <div class="card col-lg-8 col-sm-12 p-3" style="max-height: 220px !important; display: flex; flex-direction: column; box-sizing: border-box;">
                <h5 class="card-title mb-3 fw-bold text-secondary">Novedades y Avisos</h5>

                <div class="d-flex flex-column gap-2" style="overflow-y: auto; overflow-x: hidden; flex-grow: 1; min-width: 0; padding-right: 4px;">

                    <% if (ListaGlobales.Count == 0 && ListaGrupos.Count == 0)
                        { %>
                    <div class="text-center py-2 text-muted w-100">
                        <p class="mb-0" style="font-size: 0.9rem;">No hay avisos ni novedades recientes por el momento.</p>
                    </div>
                    <% } %>
                    <% foreach (var aviso in ListaGlobales)
                        { %>
                    <div class="alert alert-primary m-0 p-2 shadow-sm" style="border-left: 5px solid #0d6efd; border-top: 0; border-right: 0; border-bottom: 0; display: flex; justify-content: space-between; align-items: center; width: 100%; box-sizing: border-box;">
                        <div class="d-flex align-items-center gap-3" style="min-width: 0; flex-grow: 1;">
                            <div class="p-2 bg-light rounded-circle text-primary d-flex align-items-center justify-content-center flex-shrink-0" style="width: 32px; height: 32px;">
                                <i class="bi bi-globe" style="font-size: 0.9rem;"></i>
                            </div>
                            <div style="min-width: 0;">
                                <h6 class="mb-0 fw-bold text-dark" style="font-size: 0.9rem;"><%= aviso.Titulo %></h6>
                                <p class="mb-0 text-secondary" style="font-size: 0.8rem; line-height: 1.2; white-space: normal; word-break: break-word;"><%= aviso.Descripcion %></p>
                            </div>
                        </div>
                        <div class="ms-auto text-end ps-2 flex-shrink-0">
                            <span class="badge bg-white text-secondary border font-monospace text-uppercase" style="font-size: 0.65rem; padding: 3px 5px;"><%= aviso.Fecha %></span>
                        </div>
                    </div>
                    <% } %>
                    <% foreach (var aviso in ListaGrupos)
                        { %>
                    <div class="alert alert-warning m-0 p-2 shadow-sm" style="border-left: 5px solid #ffc107; border-top: 0; border-right: 0; border-bottom: 0; display: flex; justify-content: space-between; align-items: center; width: 100%; box-sizing: border-box;">
                        <div class="d-flex align-items-center gap-3" style="min-width: 0; flex-grow: 1;">
                            <div class="p-2 bg-light rounded-circle text-warning d-flex align-items-center justify-content-center flex-shrink-0" style="width: 32px; height: 32px;">
                                <i class="bi bi-people-fill" style="font-size: 0.9rem;"></i>
                            </div>
                            <div style="min-width: 0;">
                                <h6 class="mb-0 fw-bold text-dark" style="font-size: 0.9rem;">[Tu Grupo] <%= aviso.Titulo %></h6>
                                <p class="mb-0 text-secondary" style="font-size: 0.8rem; line-height: 1.2; white-space: normal; word-break: break-word;"><%= aviso.Descripcion %></p>
                            </div>
                        </div>
                        <div class="ms-auto text-end ps-2 flex-shrink-0">
                            <span class="badge bg-white text-secondary border font-monospace text-uppercase" style="font-size: 0.65rem; padding: 3px 5px;"><%= aviso.Fecha %></span>
                        </div>
                    </div>
                    <% } %>
                </div>
            </div>
            <div class="card col-lg-4 col-sm-12 p-3">
                <h5 class="card-title mb-3 fw-bold text-secondary">Recursos Rápidos</h5>
                <div class="text-muted" style="font-size: 0.85rem;">
                    <a href="https://aulainstitutoidiomas.unjfsc.edu.pe/login/index.php" class="text-primary fw-bold text-decoration-none" target="_blank">Ir al Aula Virtual - Instituto de Idiomas
                    </a>
                </div>
            </div>

        </div>
    </div>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
</script>
</asp:Content>
