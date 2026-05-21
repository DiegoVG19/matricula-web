<%@ Page Title="Récord Académico" Language="C#" MasterPageFile="~/SiteStudent.Master" AutoEventWireup="true" CodeBehind="recordasis.aspx.cs" Inherits="sidiWeb.student.recordasis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="container-fluid mt-3">
        <div class="card shadow-sm border-0" style="border-radius: 10px;">
            
            <asp:UpdatePanel ID="upMain" runat="server">
                <ContentTemplate>
                    
                    <div class="card-header bg-white border-bottom-0 py-3">
                        <div class="d-flex align-items-center justify-content-between flex-wrap">
                            <h5 class="text-primary font-weight-bold mb-2 mb-md-0">
                                <i class="fas fa-file-invoice me-2"></i>ASISTENCIAS Y NOTAS
                            </h5> 
                            
                            <div class="d-flex flex-column flex-lg-row gap-3 align-items-center"> 
                                <div class="d-flex align-items-center">
                                    <span class="small font-weight-bold text-secondary me-2">IDIOMA:</span>
                                    <asp:DropDownList ID="ddlIdiomaFiltro" runat="server" CssClass="form-select form-select-sm" 
                                        style="width: 180px;" AutoPostBack="true" OnSelectedIndexChanged="ddlIdiomaFiltro_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="d-flex align-items-center">
                                    <span class="small font-weight-bold text-secondary me-2">CICLO:</span>
                                    <asp:DropDownList ID="ddlCicloFiltro" runat="server" CssClass="form-select form-select-sm" 
                                        style="width: 200px;" AutoPostBack="true" OnSelectedIndexChanged="ddlCicloFiltro_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <asp:Label ID="lblEstado" runat="server" CssClass="badge rounded-pill px-3 py-2"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="card-body pt-0" style="position: relative; min-height: 200px;">
                        
                        <%-- EL CARGANDO: Ahora está dentro del card-body y centrado ahí --%>
                        <asp:UpdateProgress ID="upLoading" runat="server" AssociatedUpdatePanelID="upMain">
                            <ProgressTemplate>
                                <div style="position: absolute; top: 0; left: 0; width: 100%; height: 100%; 
                                            background: rgba(255,255,255,0.7); z-index: 10; 
                                            display: flex; align-items: center; justify-content: center; border-radius: 0 0 10px 10px;">
                                    <div class="text-center" style="margin-top: 100px;"> <%-- Ajusta este margin si lo quieres más abajo --%>
                                        <div class="spinner-border text-primary" role="status"></div>
                                        <div class="text-primary fw-bold mt-2">Cargando datos...</div>
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                        <hr class="mt-0 mb-4" />

                        <div class="row">
                            <%-- Sección Asistencia --%>
                            <div class="col-lg-5 mb-4">
                                <div class="p-3 border rounded shadow-sm bg-white h-100">
                                    <div class="d-flex justify-content-between align-items-center mb-3">
                                        <h6 class="font-weight-bold text-dark mb-0"><i class="fas fa-calendar-check me-2 text-info"></i>ASISTENCIA</h6>
                                        <div class="text-end">
                                            <span class="small text-muted">Faltas: </span>
                                            <asp:Label ID="lblFalta" runat="server" CssClass="h6 font-weight-bold text-danger" Text="0"></asp:Label>
                                            <span class="small text-muted"> / Límite: </span>
                                            <asp:Label ID="lblLimiteFaltas" runat="server" CssClass="small font-weight-bold" Text="0"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="table-responsive" style="max-height: 350px;">
                                        <asp:GridView ID="gvAsistencias" runat="server" CssClass="table table-sm table-hover" AutoGenerateColumns="False" GridLines="None">
                                            <HeaderStyle CssClass="table-light small text-uppercase" />
                                            <Columns>
                                                <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="nombreAsistencia" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <%-- Sección Calificaciones --%>
                            <div class="col-lg-7 mb-4">
                                <div class="p-3 border rounded shadow-sm bg-white h-100">
                                    <div class="d-flex justify-content-between align-items-center mb-3">
                                        <h6 class="font-weight-bold text-dark mb-0"><i class="fas fa-star me-2 text-warning"></i>CALIFICACIONES</h6>
                                        <asp:DropDownList ID="ddlTipoNota" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoNota_SelectedIndexChanged" CssClass="form-select form-select-sm w-auto shadow-sm">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvPromedios" runat="server" CssClass="table table-sm table-bordered text-center" OnRowDataBound="gvPromedios_RowDataBound">
                                            <HeaderStyle CssClass="bg-primary text-white small" />
                                        </asp:GridView>
                                        <asp:GridView ID="gvNotas" runat="server" CssClass="table table-sm table-hover" Visible="false" AutoGenerateColumns="false" GridLines="Horizontal">
                                            <HeaderStyle CssClass="table-light small" />
                                            <Columns>
                                                <asp:BoundField DataField="tipoNota" HeaderText="Tipo" />
                                                <asp:BoundField DataField="titulo" HeaderText="Evaluación" />
                                                <asp:BoundField DataField="nota" HeaderText="Nota" ItemStyle-Font-Bold="true" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>