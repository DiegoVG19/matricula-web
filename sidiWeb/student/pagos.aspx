<%-- Importante: Inherits debe coincidir con el namespace del .cs --%>
<%@ Page Title="Historial de Pagos" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="pagos.aspx.cs" Inherits="sidiWeb.student.pagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Resources/paginacion.css" rel="stylesheet"/>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upTable">
        <ProgressTemplate>
            <div class="loading-overlay"><div class="spinner"></div></div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="info-card mb-4" style="padding: 20px; background: #fff; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);">
        <div class="row align-items-center">
            <div class="col-md-6">
    <h2 class="m-0 text-primary">
        <i class="fas fa-file-invoice-dollar me-2"></i>HISTORIAL DE PAGOS
                </h2>
            </div>
            <div class="col-md-6 mt-3 mt-md-0">
                <asp:UpdatePanel ID="upSearch" runat="server">
    <ContentTemplate>
        <div class="input-wrapper">
            <i class="fas fa-search"></i>
            <asp:TextBox ID="txtSearchRecibo" runat="server" 
                CssClass="info-input" 
                placeholder="Escribe el número de recibo..." 
                onkeyup="doSearch(this);" 
                autocomplete="off"></asp:TextBox>
        </div>
        
        <script type="text/javascript">
            function doSearch(input) {
                // Guardamos la posición del cursor antes del postback
                window.lastSelectionStart = input.selectionStart;
                window.lastSelectionEnd = input.selectionEnd;

                // Forzamos el PostBack manual
                __doPostBack('<%= txtSearchRecibo.UniqueID %>', '');
            }

            // Esto se ejecuta AUTOMÁTICAMENTE después de que el UpdatePanel termina
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                var input = document.getElementById('<%= txtSearchRecibo.ClientID %>');
                if (input) {
                    input.focus();
                    // Restauramos la posición exacta del cursor
                    input.setSelectionRange(window.lastSelectionStart, window.lastSelectionEnd);
                }
            });
        </script>
    </ContentTemplate>
</asp:UpdatePanel>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="upTable" runat="server">
        <ContentTemplate>
            <div class="info-card p-0 overflow-hidden" style="background: #fff; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);">
                <div class="table-responsive" style="overflow-x: auto;">
                    <asp:GridView ID="gvPagos" runat="server" 
                        CssClass="table table-hover mb-0"
                        AutoGenerateColumns="false" 
                        DataKeyNames="IdPago"
                        AllowPaging="true" 
                        PageSize="8" 
                        AllowSorting="true"
                        OnPageIndexChanging="gvPagos_PageIndexChanging"
                        OnSorting="gvPagos_Sorting"
                        GridLines="None">
                        
                        <Columns>
                          <asp:TemplateField SortExpression="Fech">
    <HeaderTemplate>
        <div style="display: flex; align-items: center; gap: 10px;">
            <asp:LinkButton ID="lbSortFecha" runat="server" 
                CommandName="Sort" 
                CommandArgument="Fech" 
                Text="FECHA" 
                CssClass="btn-header" />
            
            <span style="font-size: 1.2em; color: #ffffff;">
                <i class="fas fa-sort"></i> </span>
        </div>
    </HeaderTemplate>
    <ItemTemplate>
        <asp:Label ID="lblFech" runat="server" 
            Text='<%# Bind("Fech", "{0:dd/MM/yyyy}") %>'>
        </asp:Label>
    </ItemTemplate>
</asp:TemplateField>
                            <asp:BoundField DataField="Razon" HeaderText="CONCEPTO" SortExpression="Razon" />
                            <asp:BoundField DataField="Nivel" HeaderText="NIVEL" />
                            <asp:TemplateField HeaderText="MONTO" SortExpression="Monto">
                                <ItemTemplate>
                                    <span style="color: #000;">S/ <%# Eval("Monto", "{0:N2}") %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RECIBO" SortExpression="Recibo">
                                <ItemTemplate>
                                    <span class="badge bg-light text-dark border"><%# Eval("Recibo") %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <PagerStyle CssClass="pagination-modern" />
                        
                        <EmptyDataTemplate>
                            <div style="padding: 0px; text-align: center; color: #999;">
                                <i class="fas fa-search-minus fa-3x mb-3"></i>
                                <p>No se encontraron registros de pago.</p>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtSearchRecibo" EventName="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>