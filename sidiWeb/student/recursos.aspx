<%@ Page Title="Recursos del Estudiante" Language="C#" MasterPageFile="~/SiteStudent.Master" 
    AutoEventWireup="true" CodeBehind="recursos.aspx.cs" Inherits="sidiWeb.student.recursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="recursos.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        
        <%-- Encabezado de la página --%>
        <div class="mb-4">
            <h2 class="fw-bold text-dark header-title">Mis Recursos</h2>
            <p class="text-muted small">Portales externos, matrículas y formatos institucionales centralizados en un solo lugar.</p>
        </div>

        <%-- Contenedor de Tarjetas --%>
        <div class="row g-4 align-items-start">
            
            <%-- Columna 1: Aulas Virtuales --%>
            <div class="col-lg-4 col-md-6">
                <div class="card resources-profile-card h-100 p-4">
                    <h5 class="card-title-custom mb-3">
                        <i class="fas fa-laptop-code me-2 text-primary"></i>Aulas Virtuales
                    </h5>
                    <p class="text-muted small mb-4">Portales interactivos para el desarrollo de tus clases cotidianas.</p>
                    
                    <div class="info-group-resource">
                        <a href="https://learn.eltngl.com/" target="_blank" class="text-decoration-none text-dark">
                            <div class="resource-item d-flex align-items-center p-3 mb-3 border rounded shadow-sm">
                                <div class="icon-box bg-spark me-3"><i class="fas fa-bolt"></i></div>
                                <div class="flex-grow-1">
                                    <h6 class="mb-0 fw-semibold text-secondary">Plataforma Spark</h6>
                                    <small class="text-muted">Contenidos de idiomas</small>
                                </div>
                                <i class="fas fa-chevron-right text-muted small"></i>
                            </div>
                        </a>
                        <a href="https://aulainstitutoidiomas.unjfsc.edu.pe/login/index.php" target="_blank" class="text-decoration-none text-dark">
                            <div class="resource-item d-flex align-items-center p-3 border rounded shadow-sm">
                                <div class="icon-box bg-aula me-3"><i class="fas fa-chalkboard-user"></i></div>
                                <div class="flex-grow-1">
                                    <h6 class="mb-0 fw-semibold text-secondary">Aula Virtual</h6>
                                    <small class="text-muted">Accede a tu Aula Virtual</small>
                                </div>
                                <i class="fas fa-chevron-right text-muted small"></i>
                            </div>
                        </a>
                    </div>
                </div>
            </div>

            <%-- Columna 2: Proceso de Matrícula --%>
            <div class="col-lg-4 col-md-6">
                <div class="card resources-profile-card h-100 p-4">
                    <h5 class="card-title-custom mb-3">
                        <i class="fas fa-user-check me-2 text-purple"></i>Proceso de Matrícula
                    </h5>
                    <p class="text-muted small mb-4">Enlaces obligatorios para asegurar y registrar tu cupo del próximo ciclo.</p>

                    <div class="info-group-resource">
                        <a href="https://docs.google.com/forms/d/e/1FAIpQLSdmf-mbKTWDL2MTBaBnTtSDB51ZRfhs3sB9udMYiMWej-VLZw/viewform" target="_blank" class="text-decoration-none text-dark">
                            <div class="resource-item d-flex align-items-center p-3 mb-3 border rounded shadow-sm">
                                <div class="icon-box bg-fumidi me-3"><i class="fas fa-user-plus"></i></div>
                                <div class="flex-grow-1">
                                    <h6 class="mb-0 fw-semibold text-secondary">FUMIDI</h6>
                                    <small class="text-muted">Matrícula para el siguiente ciclo</small>
                                </div>
                                <i class="fas fa-chevron-right text-muted small"></i>
                            </div>
                        </a>
                        <a href="https://forms.gle/LPaUzBtvVdWxK4mS9" target="_blank" class="text-decoration-none text-dark">
                            <div class="resource-item d-flex align-items-center p-3 border rounded shadow-sm">
                                <div class="icon-box bg-libro me-3"><i class="fas fa-book-open"></i></div>
                                <div class="flex-grow-1">
                                    <h6 class="mb-0 fw-semibold text-secondary">Trámite del Libro</h6>
                                    <small class="text-muted">Tramite y reporte de problemas</small>
                                </div>
                                <i class="fas fa-chevron-right text-muted small"></i>
                            </div>
                        </a>
                    </div>
                </div>
            </div>

            <%-- Columna 3: Trámites y Formulación --%>
            <div class="col-lg-4 col-md-12">
                <div class="card resources-profile-card h-100 p-4">
                    <h5 class="card-title-custom mb-3">
                        <i class="fas fa-file-invoice me-2 text-success"></i>Trámites y Formulación
                    </h5>
                    <p class="text-muted small mb-4">Justificación de inasistencias y descargas de plantillas.</p>

                    <div class="info-group-resource">
                        <div class="resource-item p-3 border rounded shadow-sm">
                            <div class="d-flex align-items-center mb-2 collapse-trigger" data-bs-toggle="collapse" data-bs-target="#collapseFormatos" style="cursor: pointer;">
                                <div class="icon-box bg-formatos me-3"><i class="fas fa-file-pdf"></i></div>
                                <div class="flex-grow-1">
                                    <h6 class="mb-0 fw-semibold text-secondary">Justificación de Inasistencias (FUT)</h6>
                                    <small class="text-muted">Formatos y requisitos</small>
                                </div>
                                <i class="fas fa-chevron-down text-muted small"></i>
                            </div>
                            
                            <div class="collapse mt-3" id="collapseFormatos">
                                <ul class="list-unstyled small mb-3">
                                    <li class="py-2 border-bottom"><a href="https://drive.google.com/file/d/1KsL6YzSF74i2dg3d5iYE8W3LYQWPibu2/view?usp=sharing" target="_blank" class="text-decoration-none text-secondary d-flex justify-content-between"><span>FUT Oficial</span> <i class="fas fa-external-link-alt"></i></a></li>
                                    <li class="py-2 border-bottom"><a href="https://drive.google.com/file/d/1j1eHGd-naG8nQ7QDtKQ_pJxLjZLtiOER/view?usp=sharing" target="_blank" class="text-decoration-none text-secondary d-flex justify-content-between"><span>Anexo 2</span> <i class="fas fa-external-link-alt"></i></a></li>
                                </ul>
                                <div class="p-3 bg-light rounded border">
                                    <strong class="small text-dark">Requisitos:</strong>
                                    <ul class="ps-3 mb-2 small text-secondary">
                                        <li>FUT, Copia DNI y sustento.</li>
                                        <li>Derecho de trámite (Tributo 586).</li>
                                    </ul>
                                    <a href="https://facilita.gob.pe/t/4528" target="_blank" class="btn btn-primary btn-sm w-100">Enviar por Facilita</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>