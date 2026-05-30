using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sidiWeb
{
    public partial class inicioStudent : System.Web.UI.Page
    {
        String dni, userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            else
            {
                userId = Session["UserId"].ToString();
                //loadStudentData();
            }
        }

        //private void loadStudentData()
        //{
        //    string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
        //    using (SqlConnection sqlConnection = new SqlConnection(connect))
        //    {
        //        SqlCommand cmd = new SqlCommand("detalle_alumno_por_id", sqlConnection)
        //        {
        //            CommandType = System.Data.CommandType.StoredProcedure
        //        };
        //        cmd.Connection.Open();
        //        cmd.Parameters.Add(new SqlParameter("@id", userId));
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            lblEstudiante.Text = dr["apellidos"].ToString() + " " + dr["nombre"].ToString();
        //            txtCorreo.Text = dr["correoElectronico"].ToString();
        //            txtCelular.Text = dr["celular"].ToString();
        //            txtTelefono.Text = dr["telefono"].ToString();
        //            txtDireccion.Text = dr["direccion"].ToString();
        //            dni = dr["dni"].ToString();
        //            lblDni.Text = dni;
        //            lblNacimiento.Text = Convert.ToDateTime(dr["fechanacimiento"]).ToString("dd/MM/yyyy");
        //            lblGenero.Text = dr["sexo"].ToString();
        //        }
        //        else
        //        {
        //            Response.Redirect("Login.aspx");
        //        }
        //        dr.Close();
        //        cmd.Connection.Close();

        //        ///////////////////////////////////////////////////////////////////////////////

        //        SqlCommand cmd2 = new SqlCommand("buscar_tipo_alumno", sqlConnection)
        //        {
        //            CommandType = System.Data.CommandType.StoredProcedure
        //        };
        //        cmd2.Connection.Open();
        //        cmd2.Parameters.Add(new SqlParameter("@dni", dni));
        //        SqlDataReader dr2 = cmd2.ExecuteReader();
        //        if (dr2.Read()) lblTipo.Text = dr2["Descripcion"].ToString();
        //        dr2.Close();
        //        cmd2.Connection.Close();
        //    }
        //}

        //public void saveStudentData()
        //{

        //}

        //protected void btnCancelar_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("index.aspx");
        //}
        //protected void btnGuardar_Click(object sender, EventArgs e)
        //{
        //    if (Page.IsValid)
        //    {
        //        // Todos los campos son válidos, proceder con el guardado
        //        try
        //        {
        //            // Aquí iría tu código para guardar los datos
        //            // Por ejemplo: SaveUserData();

        //            // Mostrar mensaje de éxito
        //            ShowValidationSummary("Los datos se han guardado correctamente.", "success");

        //            // Opcional: Redirigir a otra página
        //            // Response.Redirect("ConfirmationPage.aspx");
        //        }
        //        catch (Exception ex)
        //        {
        //            ShowValidationSummary("Error al guardar los datos: " + ex.Message, "error");
        //        }
        //    }
        //    else
        //    {
        //        ShowValidationSummary("Por favor, complete todos los campos requeridos correctamente.", "error");
        //    }
        //}

        //private void ConfigureClientSideValidation()
        //{
        //    // Habilitar validación del lado cliente
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "EnableValidation",
        //        "if (typeof(ValidatorOnLoad) == 'function') { ValidatorOnLoad(); }", true);

        //    // Configurar validadores para mostrar mensajes inmediatamente
        //    foreach (BaseValidator validator in GetAllValidators())
        //    {
        //        validator.Display = ValidatorDisplay.Dynamic;
        //        validator.SetFocusOnError = true;
        //    }
        //}

        //private BaseValidator[] GetAllValidators()
        //{
        //    // Obtener todos los validadores en la página
        //    return new BaseValidator[]
        //    {
        //    rfvCorreo, validateEmail,
        //    rfvCelular, validateCellphoneNumber,
        //    rfvTelefono, validateNumber,
        //    rfvDireccion
        //    };
        //}

        //private void ShowValidationSummary(string message, string type)
        //{
        //    pnlValidationSummary.Visible = true;
        //    lblValidationSummary.Text = message;

        //    // Cambiar el estilo según el tipo de mensaje
        //    if (type == "success")
        //    {
        //        pnlValidationSummary.CssClass = "validation-summary success-summary";
        //    }
        //    else
        //    {
        //        pnlValidationSummary.CssClass = "validation-summary error-summary";
        //    }
        //}

        //// Método para validar campos en tiempo real con JavaScript
        //protected void Page_PreRender(object sender, EventArgs e)
        //{
        //    string script = @"
        //    function validateFieldOnChange(fieldId, validatorId) {
        //        var field = document.getElementById(fieldId);
        //        if (field) {
        //            field.addEventListener('input', function() {
        //                ValidatorValidate(document.getElementById(validatorId));
        //                ValidatorUpdateIsValid();
        //            });
        //        }
        //    }

        //    window.onload = function() {
        //        validateFieldOnChange('" + txtCorreo.ClientID + @"', '" + validateEmail.ClientID + @"');
        //        validateFieldOnChange('" + txtCelular.ClientID + @"', '" + validateCellphoneNumber.ClientID + @"');
        //        validateFieldOnChange('" + txtTelefono.ClientID + @"', '" + validateNumber.ClientID + @"');
        //    };
        //";

        //    ClientScript.RegisterStartupScript(this.GetType(), "ValidationScript", script, true);
        //}

    }
}