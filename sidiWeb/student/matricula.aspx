<%@ Page Title="Matrícula Virtual" Language="C#" MasterPageFile="~/SiteStudent.Master" AutoEventWireup="true" CodeBehind="matricula.aspx.cs" Inherits="sidiWeb.student.matricula" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="card shadow-sm p-4" style="border-radius: 15px;">
            <h2 class="text-primary"><i class="fas fa-graduation-cap"></i>MATRICULA VIRTUAL</h2>
            <p class="text-muted">Estudiante: <strong>MEDINA VEGA DIEGO ALFREDO</strong></p>
            <hr />

            <asp:Panel ID="pnlVoucher" runat="server">
                <div class="alert alert-info">
                    <i class="fas fa-info-circle"></i>Paso 1: Suba su comprobante de pago para habilitar la matrícula.
                </div>
                <div class="form-group p-4 text-center border" style="border-style: dashed !important; background: #f8fafc;">
                    <label for="MainContent_fuVoucher" class="d-block mb-3">Seleccione la imagen del voucher (JPG, PNG o PDF):</label>
                    <asp:FileUpload ID="fuVoucher" runat="server" CssClass="form-control-file d-inline-block" />
                </div>
                <asp:Button ID="btnSubir" runat="server" Text="Enviar Voucher para Revisión"
                    CssClass="btn btn-primary btn-lg btn-block" OnClick="btnSubir_Click" />
            </asp:Panel>

            <asp:Panel ID="pnlPendiente" runat="server" Visible="false" CssClass="text-center py-5">
                <div class="spinner-border text-warning" role="status" style="width: 3rem; height: 3rem;"></div>
                <h3 class="mt-3 text-warning">Pago en Verificación</h3>
                <p class="lead">Hemos recibido su documento. El administrador validará el pago y asignará su curso en breve.</p>
                <div class="badge badge-secondary p-2">Estado: Pendiente de Confirmación</div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
