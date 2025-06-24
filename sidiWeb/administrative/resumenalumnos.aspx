<%@ Page Title="" Language="C#" MasterPageFile="~/SiteAdministrative.master" AutoEventWireup="true" CodeBehind="resumenalumnos.aspx.cs" Inherits="sidiWeb.administrative.resumenalumnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .search-container {
            margin-bottom: 2rem;
        }
        
        .search-input-group {
            display: flex;
            gap: 0.5rem;
            align-items: stretch;
        }
        
        .search-input-wrapper {
            flex: 1;
            position: relative;
        }
        
        .btn-search {
            background-color: var(--primary-color);
            border-color: var(--primary-color);
            color: white;
            padding: 12px 24px;
            border-radius: 8px;
            font-weight: 600;
            transition: all 0.3s ease;
            border: none;
            cursor: pointer;
        }
        
        .btn-search:hover {
            background-color: var(--primary-hover);
            transform: translateY(-1px);
            box-shadow: 0 4px 8px rgba(0, 42, 92, 0.3);
        }
        
        .btn-clear {
            background-color: #6c757d;
            border-color: #6c757d;
            color: white;
            padding: 12px 20px;
            border-radius: 8px;
            font-weight: 600;
            transition: all 0.3s ease;
            border: none;
            cursor: pointer;
        }
        
        .btn-clear:hover {
            background-color: #5a6268;
            transform: translateY(-1px);
        }
        
        .student-card {
            border: 2px solid #e9ecef;
            border-radius: 8px;
            padding: 1rem;
            margin-bottom: 0.75rem;
            background-color: white;
            transition: all 0.3s ease;
        }
        
        .student-card:hover {
            background-color: #f8f9fa;
            border-color: var(--primary-color);
            transform: translateY(-2px);
            box-shadow: 0 4px 8px rgba(0, 42, 92, 0.15);
        }
        
        .student-avatar {
            width: 45px;
            height: 45px;
            background-color: var(--primary-color);
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            color: white;
            font-size: 1.2rem;
        }
        
        .student-name {
            color: var(--primary-color);
            font-weight: 600;
            margin-bottom: 0.25rem;
            font-size: 1.1rem;
        }
        
        .student-career {
            color: #6c757d;
            font-size: 0.9rem;
            margin: 0;
        }
        
        .student-email {
            color: #6c757d;
            font-size: 0.85rem;
            margin: 0.25rem 0 0 0;
        }
        
        .badge.activo {
            background-color: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
            padding: 0.25rem 0.75rem;
            border-radius: 15px;
            font-size: 0.75rem;
            font-weight: bold;
        }
        
        .badge.inactivo {
            background-color: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
            padding: 0.25rem 0.75rem;
            border-radius: 15px;
            font-size: 0.75rem;
            font-weight: bold;
        }
        
        .no-results {
            text-align: center;
            padding: 3rem 1rem;
            color: #6c757d;
        }
        
        .no-results i {
            font-size: 4rem;
            margin-bottom: 1rem;
            color: var(--primary-color);
        }
        
        .search-info {
            background-color: #d1ecf1;
            border: 1px solid #bee5eb;
            color: #0c5460;
            padding: 0.75rem 1rem;
            border-radius: 8px;
            margin-bottom: 1rem;
            display: flex;
            align-items: center;
            gap: 0.5rem;
        }
        
        .search-info.warning {
            background-color: #fff3cd;
            border-color: #ffeaa7;
            color: #856404;
        }
        
        .results-container {
            max-height: 600px;
            overflow-y: auto;
        }
        
        .results-container::-webkit-scrollbar {
            width: 6px;
        }
        
        .results-container::-webkit-scrollbar-track {
            background: var(--background-light);
            border-radius: 3px;
        }
        
        .results-container::-webkit-scrollbar-thumb {
            background: var(--primary-color);
            border-radius: 3px;
        }
        
        .results-container::-webkit-scrollbar-thumb:hover {
            background: var(--primary-hover);
        }
        
        /* Responsive */
        @media (max-width: 768px) {
            .search-input-group {
                flex-direction: column;
            }
            
            .btn-search, .btn-clear {
                width: 100%;
                justify-content: center;
            }
            
            .student-card {
                padding: 0.75rem;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Header de la sección -->
    <div class="section-header">
        <h1 class="section-title">
            <i class="fas fa-search"></i>
            Buscar Alumnos
        </h1>
        <p class="mb-0">Ingrese los nombres o DNI</p>
    </div>

    <!-- Buscador -->
    <div class="search-container">
        <div class="table-container">
            <div class="search-input-group">
                <div class="search-input-wrapper">
                    <div class="input-group">
                        <i class="fas fa-search input-icon"></i>
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="info-value" 
                                   placeholder="Nombre / DNI"></asp:TextBox>
                    </div>
                </div>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn-search" 
                          OnClick="btnBuscar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn-clear" 
                          OnClick="btnLimpiar_Click" />
            </div>
        </div>
    </div>

    <!-- Área de resultados -->
    <div class="row">
        <div class="col-12">
            <div class="table-container">
                <h4 class="mb-3" style="color: var(--primary-color); display: flex; align-items: center; gap: 0.5rem;">
                    <i class="fas fa-list"></i>
                    Resultados
                </h4>
                
                <!-- Panel de información de búsqueda -->
                <asp:Panel ID="pnlSearchInfo" runat="server" Visible="false" CssClass="search-info">
                    <i class="fas fa-info-circle"></i>
                    <asp:Label ID="lblSearchInfo" runat="server"></asp:Label>
                </asp:Panel>
                
                <!-- Contenedor de resultados -->
                <div class="results-container">
                    <!-- Panel de estado inicial -->
                    <asp:Panel ID="pnlEstadoInicial" runat="server" Visible="true" CssClass="no-results">
                        <i class="fas fa-search"></i>
                        <h5 style="color: var(--primary-color);">Realizar búsqueda</h5>
                        <p>Ingresa un término de búsqueda y haz clic en "Buscar" para ver los resultados</p>
                    </asp:Panel>
                    
                    <!-- Panel sin resultados -->
                    <asp:Panel ID="pnlSinResultados" runat="server" Visible="false" CssClass="no-results">
                        <i class="fas fa-search-minus"></i>
                        <h5 style="color: var(--primary-color);">Sin resultados</h5>
                        <p>No se encontraron alumnos que coincidan con tu búsqueda</p>
                    </asp:Panel>
                    
                    <!-- Repeater para mostrar resultados -->
                    <asp:Repeater ID="rptResultados" runat="server">
                        <ItemTemplate>
                            <div class="student-card">
                                <div class="d-flex justify-content-between align-items-center">
                                    <div class="d-flex align-items-center">
                                        <div class="student-avatar me-3">
                                            <i class="fas fa-user"></i>
                                        </div>
                                        <div>
                                            <p class="student-career"><%# Eval("NCarnet") %></p>
                                            <div class="student-name"><%# Eval("Nombre") %></div>
                                            <p class="student-email"><%# Eval("Dni") %></p>
                                        </div>
                                    </div>
                                    <div class="text-end">
                                        <div class="mt-2">
                                            <small class="text-muted">Tipo de Alumno: <%# Eval("TipoAlumno") %>°</small><br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
