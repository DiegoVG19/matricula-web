<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="detallegrupo.aspx.cs" Inherits="sidiWeb.detallegrupo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card p-4 shadow-sm">
        <h5 class="section-title"><i class="fas fa-users"></i>Información del Grupo</h5>
        <div class="group-info-grid">
            <div>
                <strong>NÚMERO:</strong>
                <asp:Label ID="lblNumero" runat="server"></asp:Label>
            </div>
            <div>
                <strong>IDIOMA:</strong>
                <asp:Label ID="lblIdioma" runat="server"></asp:Label>
            </div>
            <div>
                <strong>NIVEL:</strong>
                <asp:Label ID="lblNivel" runat="server"></asp:Label>
            </div>
            <div>
                <strong>CICLO:</strong>
                <asp:Label ID="lblCiclo" runat="server"></asp:Label>
            </div>
            <div>
                <strong>MODALIDAD:</strong>
                <asp:Label ID="lblModalidad" runat="server"></asp:Label>
            </div>
            <div>
                <strong>HORARIO:</strong>
                <asp:Label ID="lblHorario" runat="server"></asp:Label>
            </div>
            <div class="full-width">
                <strong>DÍAS:</strong>
                <asp:Label ID="lblDias" runat="server"></asp:Label>
            </div>
            <div>
                <strong>DURACIÓN:</strong>
                <asp:Label ID="lblDuracion" runat="server"></asp:Label>
            </div>
            <div class="full-width">
                <strong>DOCENTE:</strong>
                <asp:Label ID="lblDocente" runat="server"></asp:Label>
            </div>
            <div>
                <asp:Button ID="btnMostrarAsistencias" runat="server" Text="Mostrar asistencias" CssClass="btn btn-primary" OnClick="btnMostrarAsistencias_Click" />
                <asp:Button ID="btnMostrarNotas" runat="server" Text="Mostrar notas" CssClass="btn btn-primary" OnClick="btnMostrarNotas_Click" />
            </div>
        </div>
    </div>

    <div class="row">
        <!-- Sección de Asistencias -->
        <div class="col-md-6">
            <asp:Panel ID="pnlAsistencias" runat="server" Visible="false">
                <div class="card p-4 shadow-sm">
                    <h5 class="section-title"><i class="fas fa-clipboard-list"></i>Asistencias</h5>
                    <div class="group-info-grid">
                        <div>
                            <strong>LÍMITE DE FALTAS:</strong>
                            <asp:Label ID="lblLimiteFaltas" runat="server"></asp:Label>
                        </div>
                        <div>
                            <strong>FALTAS:</strong>
                            <asp:Label ID="lblFalta" runat="server"></asp:Label>
                        </div>
                        <div>
                            <strong>ESTADO:</strong>
                            <asp:Label ID="lblEstado" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="mt-4">
                        <asp:GridView ID="gvAsistencias" runat="server" CssClass="table table-bordered table-striped"
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                <asp:BoundField DataField="nombreAsistencia" HeaderText="Asistencia" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <!-- Sección de Notas -->
        <div class="col-md-6">
            <asp:Panel ID="pnlNotas" runat="server" Visible="false">
                <div class="card p-4 shadow-sm">
                    <h5 class="section-title"><i class="fas fa-graduation-cap"></i>Notas</h5>
                    <div class="mt-4">
                        <asp:DropDownList ID="ddlTipoNota" runat="server" CssClass="form-select"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlTipoNota_SelectedIndexChanged">
                            <asp:ListItem Text="Promedios hasta ahora" Value="P" />
                            
                        </asp:DropDownList>
                        <br>
                        <asp:GridView ID="gvNotas" runat="server" CssClass="table table-bordered table-striped"
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="tipoNota" HeaderText="TIPO" />
                                <asp:BoundField DataField="titulo" HeaderText="TITULO" />
                                <asp:BoundField DataField="nota" HeaderText="NOTA" />
                                <asp:BoundField DataField="fecha" HeaderText="FECHA" />
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="gvPromedios" runat="server" CssClass="table table-bordered table-striped"
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="tipoNota" HeaderText="TIPO" />
                                <asp:BoundField DataField="nota" HeaderText="NOTA" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
