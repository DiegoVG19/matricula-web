<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="pagos.aspx.cs" Inherits="sidiWeb.pagos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="payment-header">
        <h2 class="payment-title">
            <i class="fas fa-file-invoice-dollar me-2"></i>
            Historial de Pagos
        </h2>
    </div>

    <div class="table-container">
        <div class="table-responsive">
            <asp:GridView ID="gvPagos" runat="server" CssClass="table table-hover"
                AutoGenerateColumns="false" DataKeyNames="IdPago">
                <Columns>
                    <asp:BoundField DataField="Fech" HeaderText="FECHA" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Razon" HeaderText="RAZON" />
                    <asp:BoundField DataField="Nivel" HeaderText="NIVEL" />
                    <asp:BoundField DataField="Monto" HeaderText="MONTO" DataFormatString="S/ {0:N2}" />
                    <asp:BoundField DataField="Recibo" HeaderText="RECIBO" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
