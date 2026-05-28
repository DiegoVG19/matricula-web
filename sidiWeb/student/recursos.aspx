<%@ Page Title="Recursos del Estudiante" Language="C#" MasterPageFile="~/SiteStudent.Master" 
    AutoEventWireup="true" CodeBehind="recursos.aspx.cs" Inherits="sidiWeb.student.recursos" %>

<%-- 1. CONTENEDOR DE LA CABECERA (Enlace al CSS externo) --%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="recursos.css" rel="stylesheet" type="text/css" />
</asp:Content>

<%-- 2. CONTENEDOR PRINCIPAL (Estructura Responsiva) --%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        
        <div class="row mb-4">
            <div class="col-12">
                <h2 class="fw-bold text-dark header-title">Mis Recursos</h2>
                <p class="text-muted small">Portales externos, matrículas y formatos institucionales centralizados en un solo lugar.</p>
            </div>
        </div>

        <div class="row g-4">
            
            <%-- Columna 1: Aulas Virtuales --%>
            <div class="col-lg-4 col-md-6">
                <div class="card dashboard-card h-100 p-3">
                    <div class="card-body">
                        <h5 class="card-title-custom mb-3">
                            <i class="fas fa-laptop-code me-2 text-primary"></i>Aulas Virtuales
                        </h5>
                        <p class="text-muted small mb-4">Portales interactivos para el desarrollo de tus clases cotidianas.</p>
                        
                        <a href="https://learn.eltngl.com/" target="_blank" class="text-decoration-none text-dark">
                            <div class="resource-item d-flex align-items-center p-3 mb-3">
                                <div class="icon-box bg-spark me-3">
                                    <i class="fas fa-bolt"></i>
                                </div>
                                <div class="flex-grow-1">
                                    <h6 class="mb-0 fw-semibold text-secondary">Plataforma Spark</h6>
                                    <small class="text-muted">Contenidos de idiomas</small>
                                </div>
                                <i class="fas fa-chevron-right text-muted small"></i>
                            </div>
                        </a>

                        <a href="https://aulainstitutoidiomas.unjfsc.edu.pe/login/index.php" target="_blank" class="text-decoration-none text-dark">
                            <div class="resource-item d-flex align-items-center p-3">
                                <div class="icon-box bg-aula me-3">
                                    <i class="fas fa-chalkboard-user"></i>
                                </div>
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
                <div class="card dashboard-card h-100 p-3">
                    <div class="card-body">
                        <h5 class="card-title-custom mb-3">
                            <i class="fas fa-user-check me-2 text-purple"></i>Proceso de Matrícula
                        </h5>
                        <p class="text-muted small mb-4">Enlaces obligatorios para asegurar y registrar tu cupo del próximo ciclo.</p>

                        <a href="https://docs.google.com/forms/d/e/1FAIpQLSdmf-mbKTWDL2MTBaBnTtSDB51ZRfhs3sB9udMYiMWej-VLZw/viewform" target="_blank" class="text-decoration-none text-dark">
                            <div class="resource-item d-flex align-items-center p-3 mb-3">
                                <div class="icon-box bg-fumidi me-3">
                                    <i class="fas fa-user-plus"></i>
                                </div>
                                <div class="flex-grow-1">
                                    <h6 class="mb-0 fw-semibold text-secondary">FUMIDI</h6>
                                    <small class="text-muted">Matrícula para el siguiente ciclo</small>
                                </div>
                                <i class="fas fa-chevron-right text-muted small"></i>
                            </div>
                        </a>

                        <a href="https://forms.gle/LPaUzBtvVdWxK4mS9" target="_blank" class="text-decoration-none text-dark">
                            <div class="resource-item d-flex align-items-center p-3">
                                <div class="icon-box bg-libro me-3">
                                    <i class="fas fa-book-open"></i>
                                </div>
                                <div class="flex-grow-1">
                                    <h6 class="mb-0 fw-semibold text-secondary">Trámite del Libro</h6>
                                    <small class="text-muted">Tramite y reporte de Problemas de Libro</small>
                                </div>
                                <i class="fas fa-chevron-right text-muted small"></i>
                            </div>
                        </a>
                    </div>
                </div>
            </div>

            <%-- Columna 3: Trámites y Formulación --%>
            <div class="col-lg-4 col-md-12">
                <div class="card dashboard-card h-100 p-3">
                    <div class="card-body">
                        <h5 class="card-title-custom mb-3">
                            <i class="fas fa-file-invoice me-2 text-success"></i>Trámites y Formulación
                        </h5>
                        <p class="text-muted small mb-4">Justificación de inasistencias y descargas de plantillas administrativas.</p>

                        <div class="resource-item p-3">
                            <div class="d-flex align-items-center mb-2 collapse-trigger" data-bs-toggle="collapse" data-bs-target="#collapseFormatos">
                                <div class="icon-box bg-formatos me-3">
                                    <i class="fas fa-file-pdf"></i>
                                </div>
                                <div class="flex-grow-1">
                                    <h6 class="mb-0 fw-semibold text-secondary">Justificación de Inasistencias (FUT)</h6>
                                    <small class="text-muted">Descargar formatos, ver requisitos y costos</small>
                                </div>
                                <i class="fas fa-chevron-down text-muted small"></i>
                            </div>
                            
                            <div class="collapse mt-3" id="collapseFormatos">
                                <div class="ps-4 mb-3">
                                    <p class="text-xs text-uppercase fw-bold text-muted tracking-wider mb-2 section-subtitle">Documentos para Descargar:</p>
                                    <ul class="list-unstyled small mb-0">
                                        <li class="py-2 border-bottom">
                                            <a href="https://drive.google.com/file/d/1KsL6YzSF74i2dg3d5iYE8W3LYQWPibu2/view?usp=sharing" target="_blank" class="text-decoration-none text-secondary d-flex justify-content-between align-items-center">
                                                <span><i class="far fa-file-alt me-2 text-danger"></i> FUT Oficial (Dirigido a Dirección)</span>
                                                <i class="fas fa-external-link-alt text-muted xs-icon"></i>
                                            </a>
                                        </li>
                                        <li class="py-2 border-bottom">
                                            <a href="https://drive.google.com/file/d/1j1eHGd-naG8nQ7QDtKQ_pJxLjZLtiOER/view?usp=sharing" target="_blank" class="text-decoration-none text-secondary d-flex justify-content-between align-items-center">
                                                <span><i class="far fa-file-alt me-2 text-danger"></i> Anexo 2</span>
                                                <i class="fas fa-external-link-alt text-muted xs-icon"></i>
                                            </a>
                                        </li>
                                        <li class="py-2 border-bottom mb-2">
                                            <a href="https://drive.google.com/file/d/15PG7JxtUkcnwaKAt4gKrbC9jCLP6PgW7/view?usp=sharing" target="_blank" class="text-decoration-none text-secondary d-flex justify-content-between align-items-center">
                                                <span><i class="far fa-file-pdf me-2 text-warning"></i> Modelo de Llenado</span>
                                                <i class="fas fa-external-link-alt text-muted xs-icon"></i>
                                            </a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="p-3 bg-light rounded-3 border mx-2 panel-requisitos">
                                    <div class="d-flex align-items-center mb-2">
                                        <i class="fas fa-info-circle text-primary me-2"></i>
                                        <strong class="text-dark">Requisitos del Expediente:</strong>
                                    </div>
                                    <ul class="ps-3 mb-3 text-secondary small">
                                        <li>FUT relleno, Copia de DNI y Sustento de inasistencia.</li>
                                        <li>Derecho de trámite (<strong>Tributo 586 - El pago se puede realizar en el Banco de la Nación, Pagalo.pe o Caja de la UNJFSC</strong>).</li>
                                    </ul>

                                    <div class="row g-2 mb-3 text-center">
                                        <div class="col-6">
                                            <div class="p-2 rounded-2 price-box faustiniano">
                                                <small class="text-muted d-block sub-text">Faustiniano</small>
                                                <span class="fw-bold text-warning price-value">S/. 1.00</span>
                                            </div>
                                        </div>
                                        <div class="col-6">
                                            <div class="p-2 rounded-2 price-box general">
                                                <small class="text-muted d-block sub-text">Público General</small>
                                                <span class="fw-bold text-secondary price-value">S/. 5.00</span>
                                            </div>
                                        </div>
                                    </div>

                                    <a href="https://facilita.gob.pe/t/4528" target="_blank" class="btn btn-primary btn-sm w-100 rounded-2 fw-semibold shadow-sm">
                                        <i class="fas fa-paper-plane me-1"></i> Enviar por Facilita
                                    </a>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>