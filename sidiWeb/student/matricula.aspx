<%@ Page Title="Matrícula Virtual" Language="C#" MasterPageFile="~/SiteStudent.Master"
    AutoEventWireup="true" CodeBehind="matricula.aspx.cs"
    Inherits="sidiWeb.student.matricula" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/matricula.css" rel="stylesheet" />
    <script src="../Scripts/matricula.js"></script>
<div class="container mt-4">
  <div class="card shadow-sm p-4" style="border-radius:15px;">

    <div class="d-flex justify-content-between align-items-center mb-4">
      <h2 class="text-primary mb-0">
        <i class="fas fa-graduation-cap"></i> MATRÍCULA VIRTUAL
      </h2>
      
      <div>
        <asp:Label ID="lblNombreAlumno" runat="server" CssClass="fw-bold text-secondary me-3"></asp:Label>
        <asp:LinkButton ID="btnExportarPDF" runat="server" CssClass="btn btn-outline-primary" OnClientClick="exportarTablaPDF(); return false;">
          <i class="fas fa-file-pdf"></i> Exportar a PDF
        </asp:LinkButton>
      </div>
    </div>

    <hr />

    <%-- TABLA DE INFORMACIÓN ACADÉMICA --%>
    <div class="table-responsive mb-4">
      <table class="table table-bordered table-hover" id="tablaMatricula">
        <thead class="table-primary">
          <tr>
            <th scope="col">Idioma</th>
            <th scope="col">Ciclo</th>
            <th scope="col">Nivel</th>
            <th scope="col">Vez</th>
          </tr>
        </thead>
        <tbody>
          <asp:Literal ID="litFilasTabla" runat="server"></asp:Literal>
        </tbody>
      </table>
    </div>

    <%-- PANEL: YA MATRICULADO --%>
    <asp:Panel ID="pnlYaMatriculado" runat="server" Visible="false" CssClass="text-center py-5">
      <i class="fas fa-check-circle fa-4x text-success"></i>
      <h3 class="mt-3 text-success">Ya tienes un grupo activo</h3>
      <p class="lead">Actualmente estás matriculado en un grupo en curso. No puedes hacer una nueva solicitud.</p>
    </asp:Panel>

    <%-- PANEL: REQUIERE AUTORIZACIÓN --%>
    <asp:Panel ID="pnlRequiereAuth" runat="server" Visible="false" CssClass="text-center py-5">
      <i class="fas fa-exclamation-triangle fa-4x text-danger"></i>
      <h3 class="mt-3 text-danger">Requiere autorización académica</h3>
      <p class="lead">Tu situación académica requiere una revisión previa. Por favor acércate al área académica o escríbenos.</p>

      <div class="badge badge-danger p-2">
        <asp:Label ID="lblMensajeAuth" runat="server"></asp:Label>
      </div>
    </asp:Panel>

    <%-- PANEL: SOLICITUD PENDIENTE --%>
    <asp:Panel ID="pnlPendiente" runat="server" Visible="false" CssClass="text-center py-5">
      <div class="spinner-border text-warning" role="status" style="width:3rem;height:3rem;"></div>

      <h3 class="mt-3 text-warning">Solicitud en verificación</h3>
      <p class="lead">Hemos recibido tu solicitud. El administrador validará la información y confirmará tu matrícula.</p>

      <div class="badge badge-secondary p-2">Estado: Pendiente de Confirmación</div>
    </asp:Panel>

    <%-- PANEL: MATRÍCULA --%>
    <asp:Panel ID="pnlVoucher" runat="server" Visible="false">

      <%-- Info del alumno --%>
      <div class="alert alert-info">
        <i class="fas fa-info-circle"></i>

        <strong>Estado:</strong>
        <asp:Label ID="lblEstado" runat="server"></asp:Label>
        &nbsp;|&nbsp;

        <strong>Idioma:</strong>
        <asp:Label ID="lblIdioma" runat="server"></asp:Label>
        &nbsp;|&nbsp;

        <strong>Nivel:</strong>
        <asp:Label ID="lblNivel" runat="server"></asp:Label>
        &nbsp;|&nbsp;

        <strong>Ciclo a matricular:</strong>
        <asp:Label ID="lblCiclo" runat="server"></asp:Label>
      </div>

      <%-- Paso 1: Seleccionar grupo --%>
      <div class="alert alert-primary">
        <i class="fas fa-users"></i>
        <strong>Paso 1:</strong> Selecciona tu grupo
      </div>

      <div class="form-group">
        <asp:DropDownList ID="ddlGrupos" runat="server"
            CssClass="form-control"
            DataValueField="idGrupo"
            DataTextField="descripcion">
        </asp:DropDownList>

        <small class="text-muted">
          <asp:Label ID="lblVacantes" runat="server"></asp:Label>
        </small>
      </div>

      <%-- Número de recibo --%>
      <div class="form-group">
        <label>
          <i class="fas fa-receipt"></i>
          Número de recibo (opcional, referencial):
        </label>

        <asp:TextBox ID="txtNumeroRecibo" runat="server"
            CssClass="form-control"
            placeholder="Ej: 87788-PP"
            MaxLength="50">
        </asp:TextBox>
      </div>

      <%-- Mensaje de error --%>
      <asp:Label ID="lblError" runat="server"
          CssClass="text-danger d-block mb-2">
      </asp:Label>

      <asp:Button ID="btnSubir" runat="server"
          Text="Enviar Solicitud de Matrícula"
          CssClass="btn btn-primary btn-lg btn-block"
          OnClick="btnSubir_Click" />

    </asp:Panel>

    <%-- PANEL: ERROR / SIN DATOS --%>
    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="text-center py-5">
      <i class="fas fa-times-circle fa-4x text-secondary"></i>
      <h3 class="mt-3 text-secondary">No se pudo determinar tu estado</h3>
      <p class="lead">Por favor acércate a recepción o escríbenos para atenderte.</p>
    </asp:Panel>

  </div>
</div>
</asp:Content>