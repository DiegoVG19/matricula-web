using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        // Variables para el Aviso Global
        public string TituloGlobal { get; set; } = "";
        public string DescripcionGlobal { get; set; } = "";
        public string FechaGlobal { get; set; } = "";
        public string IconoGlobal { get; set; } = "info";
        public bool MostrarGlobal { get; set; } = false;

        // Variables para el Aviso por Grupo
        public string TituloGrupo { get; set; } = "";
        public string DescripcionGrupo { get; set; } = "";
        public string FechaGrupo { get; set; } = "";
        public string IconoGrupo { get; set; } = "info";
        public bool MostrarGrupo { get; set; } = false;

        // Estructura limpia para almacenar cada aviso procesado
        public class PublicacionAviso
        {
            public string Icono { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public string Fecha { get; set; }
        }


        public List<PublicacionAviso> ListaGlobales { get; set; } = new List<PublicacionAviso>();
        public List<PublicacionAviso> ListaGrupos { get; set; } = new List<PublicacionAviso>();

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
                // loadStudentData(); 
            }

            if (!IsPostBack)
            {
                // Solución al certificado SSL inválido de la universidad
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                // Cargamos todos los avisos institucionales (Globales)
                CargarAvisosGlobalesMultiples();

                // Obtenemos el grupo real del estudiante consultando el último en el que está registrado
                string codigoGrupoEstudiante = ObtenerUltimoGrupoEstudiante(userId);

                if (!string.IsNullOrEmpty(codigoGrupoEstudiante))
                {
                    // Con el código del grupo, cargamos todos los avisos que le pertenecen
                    CargarAvisosPorGrupoMultiples(codigoGrupoEstudiante);
                }
            }
        }

        // Método clave que conecta tu BD con la lógica del CSV
        private string ObtenerUltimoGrupoEstudiante(string idUsuario)
        {
            string codigoGrupo = "";
            string carnetId = "";
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connect))
                {
                    con.Open();

                    
                    SqlCommand cmdUser = new SqlCommand("detalle_alumno_por_id", con) { CommandType = CommandType.StoredProcedure };
                    cmdUser.Parameters.AddWithValue("@id", idUsuario);
                    SqlDataReader drUser = cmdUser.ExecuteReader();

                    if (drUser.Read())
                    {
                        carnetId = drUser["dni"].ToString();
                    }
                    drUser.Close();

                    if (!string.IsNullOrEmpty(carnetId))
                    {
                        
                        SqlCommand cmdGrupo = new SqlCommand("listgrupos_alumnosv2", con) { CommandType = CommandType.StoredProcedure };
                        cmdGrupo.Parameters.AddWithValue("@nrocarnet", carnetId);
                        cmdGrupo.Parameters.AddWithValue("@idIdioma", DBNull.Value);
                        cmdGrupo.Parameters.AddWithValue("@nivel", DBNull.Value);

                        SqlDataReader drGrupo = cmdGrupo.ExecuteReader();

                        if (drGrupo.Read())
                        {
                            // Extraemos el campo 'numero' que contiene el formato "026-2025" Ejemplo
                            codigoGrupo = drGrupo["numero"].ToString().Trim();
                        }
                        drGrupo.Close();
                    }
                }
            }
            catch (Exception)
            {
                codigoGrupo = "";
            }

            return codigoGrupo;
        }

        private void CargarAvisosGlobalesMultiples()
        {
            try
            {
                string urlArchivo = "https://idiomas.unjfsc.edu.pe/intranet/avisos/avisos_globales.csv";
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    string contenido = client.DownloadString(urlArchivo);
                    if (!string.IsNullOrEmpty(contenido))
                    {
                        string[] lineas = contenido.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string linea in lineas)
                        {
                            string[] datos = linea.Split(',');
                            if (datos.Length >= 5)
                            {
                                
                                string fechaFormateada = "";
                                if (DateTime.TryParse(datos[0].Trim(), out DateTime dt))
                                {
                                    fechaFormateada = dt.ToString("dd MMM");
                                }

                                ListaGlobales.Add(new PublicacionAviso
                                {
                                    Fecha = fechaFormateada,
                                    Icono = datos[2].Trim(),       // info / alerta
                                    Titulo = datos[3].Trim(),      // Título del aviso
                                    Descripcion = datos[4].Trim()  // Detalle del aviso
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private void CargarAvisosPorGrupoMultiples(string codigoGrupo)
        {
            try
            {
                string urlArchivo = $"https://idiomas.unjfsc.edu.pe/intranet/avisos/avisos_grupos/{codigoGrupo}.csv";
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    string contenido = client.DownloadString(urlArchivo);
                    if (!string.IsNullOrEmpty(contenido))
                    {
                        string[] lineas = contenido.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string linea in lineas)
                        {
                            string[] datos = linea.Split(',');
                            if (datos.Length >= 5)
                            {
                                string fechaFormateada = "";
                                if (DateTime.TryParse(datos[0].Trim(), out DateTime dt))
                                {
                                    fechaFormateada = dt.ToString("dd MMM");
                                }

                                ListaGrupos.Add(new PublicacionAviso
                                {
                                    Fecha = fechaFormateada,
                                    Icono = datos[2].Trim(),
                                    Titulo = datos[3].Trim(),
                                    Descripcion = datos[4].Trim()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        // ===============================================================================
        // ABAJO SE MANTIENEN INTACTOS TUS MÉTODOS ORIGINALES COMENTADOS
        // ===============================================================================

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
        //        rfvCorreo, validateEmail,
        //        rfvCelular, validateCellphoneNumber,
        //        rfvTelefono, validateNumber,
        //        rfvDireccion
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