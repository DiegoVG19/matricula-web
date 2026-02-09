<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="perfil.aspx.cs" Inherits="sidiWeb.Perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="profile-header">
    <asp:Label class="mb-0 fw-bold h2" runat="server" ID="lblEstudiante">NOMBRES Y APELLIDOS DEL ESTUDIANTE</asp:Label>
</div>

<div class="profile-info">
    <div class="row">
        <div class="col-md-6">

            <div class="info-group">
                <div class="info-label">
                    <i class="fas fa-envelope input-icon"> </i> Correo electrónico <span class="required">*</span></div>
                <div class="input-group">
                    <asp:TextBox ID="txtCorreo" runat="server"
                        CssClass="form-control"
                        class="info-value"
                        placeholder="ejemplo@correo.com"
                        TextMode="Email"
                        autocomplete="email"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="rfvCorreo"
                    runat="server"
                    ControlToValidate="txtCorreo"
                    ErrorMessage="El correo electrónico es obligatorio"
                    Display="Dynamic"
                    CssClass="validation-error"
                    ForeColor="#dc3545"
                    ValidationGroup="FormValidation">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="validateEmail"
                    runat="server"
                    ErrorMessage="Debe incluir @ y el dominio: .com, .net, etc.)"
                    ControlToValidate="txtCorreo"
                    ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                    Display="Dynamic"
                    CssClass="validation-error"
                    ForeColor="#dc3545"
                    ValidationGroup="FormValidation">
                </asp:RegularExpressionValidator>
            </div>

            <div class="info-group">
                <div class="info-label"><i class="fas fa-mobile-alt input-icon"> </i> Celular <span class="required">*</span></div>
                <div class="input-group">
                    <asp:TextBox ID="txtCelular" runat="server"
                        CssClass="form-control"
                        class="info-value"
                        placeholder="987654321"
                        MaxLength="9"
                        TextMode="Phone"
                        autocomplete="tel"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="rfvCelular"
                    runat="server"
                    ControlToValidate="txtCelular"
                    ErrorMessage="El número de celular es obligatorio"
                    Display="Dynamic"
                    CssClass="validation-error"
                    ForeColor="#dc3545"
                    ValidationGroup="FormValidation">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="validateCellphoneNumber"
                    runat="server"
                    ErrorMessage="El celular debe tener exactamente 9 dígitos"
                    ControlToValidate="txtCelular"
                    ValidationExpression="^\d{9}$"
                    Display="Dynamic"
                    CssClass="validation-error"
                    ForeColor="#dc3545"
                    ValidationGroup="FormValidation">
                </asp:RegularExpressionValidator>
            </div>

            <div class="info-group">
                <div class="info-label"><i class="fas fa-phone input-icon"> </i> Teléfono Fijo <span class="required">*</span></div>
                <div class="input-group">
                    <asp:TextBox ID="txtTelefono" runat="server"
                        CssClass="form-control"
                        class="info-value"
                        placeholder="4567890"
                        MaxLength="7"
                        TextMode="Phone"
                        autocomplete="tel"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="rfvTelefono"
                    runat="server"
                    ControlToValidate="txtTelefono"
                    ErrorMessage="El número de teléfono es obligatorio"
                    Display="Dynamic"
                    CssClass="validation-error"
                    ForeColor="#dc3545"
                    ValidationGroup="FormValidation">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="validateNumber"
                    runat="server"
                    ErrorMessage="El teléfono debe tener exactamente 7 dígitos"
                    ControlToValidate="txtTelefono"
                    ValidationExpression="^\d{7}$"
                    Display="Dynamic"
                    CssClass="validation-error"
                    ForeColor="#dc3545"
                    ValidationGroup="FormValidation">
                </asp:RegularExpressionValidator>
            </div>

            <div class="info-group">
                <div class="info-label"><i class="fas fa-map-marker-alt input-icon"></i> Dirección <span class="required">*</span></div>
                <div class="input-group">
                    <asp:TextBox ID="txtDireccion" runat="server"
                        CssClass="form-control"
                        class="info-value"
                        placeholder="Av. Principal 123, Distrito, Ciudad"
                        autocomplete="street-address"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="rfvDireccion"
                    runat="server"
                    ControlToValidate="txtDireccion"
                    ErrorMessage="La dirección es obligatoria"
                    Display="Dynamic"
                    CssClass="validation-error"
                    ForeColor="#dc3545"
                    ValidationGroup="FormValidation">
                </asp:RequiredFieldValidator>
            </div>

            <!-- Botón para enviar el formulario -->
            <div class="form-actions">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary"
                    OnClick="btnGuardar_Click" ValidationGroup="FormValidation" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger"
                    OnClick="btnCancelar_Click" />
            </div>

            <!-- Panel para mostrar mensajes de validación general -->
            <asp:Panel ID="pnlValidationSummary" runat="server" CssClass="validation-summary" Visible="false">
                <asp:Label ID="lblValidationSummary" runat="server" CssClass="validation-summary-text"></asp:Label>
            </asp:Panel>
        </div>

        <div class="col-md-6">
            <div class="info-group">
                <div class="info-label">N° DNI</div>
                <asp:Label ID="lblDni" runat="server" class="info-value fw-bold">12345678</asp:Label>
            </div>
            <div class="info-group">
                <div class="info-label">Fecha de nacimiento</div>
                <asp:Label ID="lblNacimiento" runat="server" class="info-value">XX/XX/XX</asp:Label>
            </div>
            <div class="info-group">
                <div class="info-label">Género</div>
                <asp:Label ID="lblGenero" runat="server" class="info-value">INDEFINIDO</asp:Label>
            </div>
            <div class="info-group">
                <div class="info-label">Tipo</div>
                <asp:Label ID="lblTipo" runat="server" class="info-value">FAUSTINIANO/PARTICULAR</asp:Label>
            </div>
        </div>
    </div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            // Validación en tiempo real para el correo electrónico
            var txtCorreo = document.getElementById('<%= txtCorreo.ClientID %>');
            if (txtCorreo) {
                txtCorreo.addEventListener('blur', function () {
                    var emailRegex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
                    if (this.value && !emailRegex.test(this.value)) {
                        this.classList.add('input-validation-error');
                        this.classList.remove('input-validation-valid');
                    } else if (this.value) {
                        this.classList.remove('input-validation-error');
                        this.classList.add('input-validation-valid');
                    } else {
                        this.classList.remove('input-validation-error', 'input-validation-valid');
                    }
                });
            }

            // Validación en tiempo real para el celular (9 dígitos)
            var txtCelular = document.getElementById('<%= txtCelular.ClientID %>');
        if (txtCelular) {
            txtCelular.addEventListener('input', function() {
                // Permitir solo dígitos
                this.value = this.value.replace(/[^\d]/g, '');
                
                // Validar longitud
                if (this.value.length === 9) {
                    this.classList.remove('input-validation-error');
                    this.classList.add('input-validation-valid');
                } else if (this.value.length > 0) {
                    this.classList.add('input-validation-error');
                    this.classList.remove('input-validation-valid');
                } else {
                    this.classList.remove('input-validation-error', 'input-validation-valid');
                }
            });
        }
        
        // Validación en tiempo real para el teléfono fijo (7 dígitos)
        var txtTelefono = document.getElementById('<%= txtTelefono.ClientID %>');
        if (txtTelefono) {
            txtTelefono.addEventListener('input', function() {
                // Permitir solo dígitos
                this.value = this.value.replace(/[^\d]/g, '');
                
                // Validar longitud
                if (this.value.length === 7) {
                    this.classList.remove('input-validation-error');
                    this.classList.add('input-validation-valid');
                } else if (this.value.length > 0) {
                    this.classList.add('input-validation-error');
                    this.classList.remove('input-validation-valid');
                } else {
                    this.classList.remove('input-validation-error', 'input-validation-valid');
                }
            });
        }

            // Mejorar la experiencia de validación de ASP.NET
            if (typeof (ValidatorOnLoad) === 'function') {
                var originalValidatorOnLoad = ValidatorOnLoad;
                ValidatorOnLoad = function () {
                    originalValidatorOnLoad();
                    highlightInvalidFields();
                };
            }

            if (typeof (ValidatorUpdateIsValid) === 'function') {
                var originalValidatorUpdateIsValid = ValidatorUpdateIsValid;
                ValidatorUpdateIsValid = function () {
                    originalValidatorUpdateIsValid();
                    highlightInvalidFields();
                };
            }

            // Función para resaltar campos inválidos
            function highlightInvalidFields() {
                var validators = document.querySelectorAll('[id*="validate"], [id*="rfv"]');
                validators.forEach(function (validator) {
                    if (validator.controltovalidate) {
                        var control = document.getElementById(validator.controltovalidate);
                        if (control) {
                            if (!validator.isvalid) {
                                control.classList.add('input-validation-error');
                                control.classList.remove('input-validation-valid');
                            } else if (control.value) {
                                control.classList.remove('input-validation-error');
                                control.classList.add('input-validation-valid');
                            }
                        }
                    }
                });
            }

            // Efecto de focus mejorado
            var inputs = document.querySelectorAll('.info-value');
            inputs.forEach(function (input) {
                input.addEventListener('focus', function () {
                    this.parentElement.classList.add('input-focused');
                });

                input.addEventListener('blur', function () {
                    this.parentElement.classList.remove('input-focused');
                });
            });
        });
    </script>
</asp:Content>