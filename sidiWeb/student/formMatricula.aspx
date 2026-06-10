<%@ Page Title="Formulario de Matrícula" Language="C#" MasterPageFile="~/SiteStudent.Master" AutoEventWireup="true" CodeBehind="formMatricula.aspx.cs" Inherits="sidiWeb.student.formMatricula" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="card shadow-sm p-4" style="border-radius:15px;">
            
            <div class="d-flex justify-content-between align-items-center mb-4">
                <h2 class="text-primary mb-0">
                    <i class="fas fa-file-signature"></i> PROCESO DE MATRÍCULA
                </h2>
                <div class="text-end">
                    <small class="text-muted">Fecha: <%= DateTime.Now.ToString("dd/MM/yyyy") %></small>
                </div>
            </div>
            <hr />

            <%-- DATOS DEL ESTUDIANTE --%>
            <div class="alert alert-secondary mb-4">
                <div class="row">
                    <div class="col-md-6">
                        <strong>Estudiante:</strong> <asp:Label ID="lblNombreCompleto" runat="server"></asp:Label> <br />
                        <strong>Código:</strong> <asp:Label ID="lblCodigo" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-6 text-md-end">
                        <asp:Label ID="lblEstudianteNombre" runat="server" CssClass="fw-bold text-primary"></asp:Label>
                    </div>
                </div>
            </div>

            <%-- ESTADO DE CONTINUIDAD (Manejado desde C#) --%>
            <div id="divEstadoContinuidad" runat="server" visible="false" class="mb-4">
                <div id="estadoContinuidadText" runat="server"></div>
            </div>

            <div class="row">
                <%-- COLUMNA IZQUIERDA: INGRESO DE VOUCHERS --%>
                <div class="col-lg-5 mb-4">
                    <div class="card border-primary h-100">
                        <div class="card-header bg-primary text-white">
                            <i class="fas fa-plus-circle me-2"></i> Agregar Cursos
                        </div>
                        <div class="card-body">
                            <p class="text-muted small mb-3">Si vas a estudiar más de un idioma, ingresa cada número de operación por separado y haz clic en Validar.</p>
                            
                            <div class="mb-3">
                                <label class="form-label fw-bold">Número de Operación (Voucher)</label>
                                <asp:TextBox ID="txtNumeroOperacion" runat="server" CssClass="form-control" placeholder="Ej. 1234567890"></asp:TextBox>
                            </div>
                            
                            <asp:LinkButton ID="btnValidarPago" runat="server" CssClass="btn btn-primary w-100" OnClick="btnValidarPago_Click">
                                <i class="fas fa-search me-2"></i> Validar y Agregar
                            </asp:LinkButton>
                            
                            <%-- CONTENEDOR DE MENSAJES JS O C# --%>
                            <div id="divMensajes" class="mt-3">
                                <asp:Label ID="lblMensajeError" runat="server" CssClass="text-danger fw-bold d-block mt-2" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

                <%-- COLUMNA DERECHA: RESUMEN DE MATRÍCULA (CARRITO) --%>
                <div class="col-lg-7 mb-4">
                    <div class="card border-success h-100">
                        <div class="card-header bg-success text-white">
                            <i class="fas fa-list me-2"></i> Resumen de tu Matrícula
                        </div>
                        <div class="card-body p-0">
                            
                            <%-- LISTA DE CURSOS AGREGADOS --%>
                            <asp:Repeater ID="rptCursosAgregados" runat="server" OnItemCommand="rptCursosAgregados_ItemCommand">
                                <HeaderTemplate>
                                    <table class="table table-hover mb-0">
                                        <thead class="table-light">
                                            <tr>
                                                <th>Idioma/Nivel</th>
                                                <th>Modalidad</th>
                                                <th>Voucher</th>
                                                <th class="text-end">Costo</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <strong><%# Eval("NombreIdioma") %></strong><br />
                                                    <small class="text-muted"><%# Eval("NombreNivel") %></small>
                                                </td>
                                                <td><%# Eval("NombreModalidad") %></td>
                                                <td><span class="badge bg-secondary"><%# Eval("NumeroOperacion") %></span></td>
                                                <td class="text-end fw-bold">S/ <%# Convert.ToDecimal(Eval("CostoCiclo")).ToString("0.00") %></td>
                                                <td class="text-center">
                                                    <asp:LinkButton ID="btnQuitar" runat="server" CommandName="Quitar" CommandArgument='<%# Eval("NumeroOperacion") %>' CssClass="text-danger" title="Quitar">
                                                        <i class="fas fa-times-circle"></i>
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                        </tbody>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>

                            <%-- MENSAJE SI ESTÁ VACÍO --%>
                            <asp:Panel ID="pnlCarritoVacio" runat="server" CssClass="text-center p-5">
                                <i class="fas fa-clipboard-list fa-3x text-muted mb-3"></i>
                                <p class="text-muted mb-0">Aún no has agregado ningún curso.</p>
                                <p class="text-muted small">Valida tu voucher para comenzar.</p>
                            </asp:Panel>
                            
                        </div>
                        
                        <%-- TOTAL Y CONFIRMACIÓN --%>
                        <div class="card-footer bg-light">
                            <div class="d-flex justify-content-between align-items-center mb-3">
                                <span class="fw-bold text-secondary">Total Pagado:</span>
                                <asp:Label ID="lblTotalPagar" runat="server" CssClass="h4 text-success mb-0 fw-bold">S/ 0.00</asp:Label>
                            </div>
                            
                            <div class="d-flex justify-content-between">
                                <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-outline-secondary" OnClick="btnCancelar_Click">
                                    Cancelar
                                </asp:LinkButton>
                                
                                <asp:LinkButton ID="btnConfirmarMatricula" runat="server" CssClass="btn btn-success" OnClick="btnConfirmarMatricula_Click" Enabled="false">
                                    <i class="fas fa-check me-2"></i> Confirmar Matrícula Múltiple
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <asp:HiddenField ID="hdfMesesInactivo" runat="server" Value="0" />
        </div>
    </div>

    <script src="../Scripts/matricula.js" type="text/javascript"></script>
</asp:Content>