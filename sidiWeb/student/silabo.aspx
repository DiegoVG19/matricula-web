<%@ Page Title="Sílabo del Curso" Language="C#" MasterPageFile="~/SiteStudent.Master" AutoEventWireup="true" CodeBehind="silabo.aspx.cs" Inherits="sidiWeb.student.silabo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="card shadow-sm p-4">
            <h2 class="text-primary"><i class="fas fa-file-pdf"></i> MATERIAL DEL CURSO</h2>
            <p class="text-muted">Aquí podrás descargar el sílabo oficial y los materiales compartidos por tu instructor.</p>
            <hr />

            <div class="table-responsive">
                <table class="table table-hover">
                    <thead class="thead-light">
                        <tr>
                            <th>Documento</th>
                            <th>Tipo</th>
                            <th>Acción</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Sílabo - Inglés Básico II</td>
                            <td><span class="badge badge-danger">PDF</span></td>
                            <td>
                                <asp:LinkButton ID="btnDescargarSilabo" runat="server" CssClass="btn btn-success btn-sm" OnClick="btnDescargarSilabo_Click">
                                    <i class="fas fa-download"></i> Descargar
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>