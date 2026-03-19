using Microsoft.Ajax.Utilities;
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
    public partial class index1 : System.Web.UI.Page
    {
        public string FechaInicioJS { get; set; } = "";
        public string FechaFinJS { get; set; } = "";
        public string UrlImagenJS { get; set; } = "";

        String dni, userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("/");
                return;
            }
            else
            {
                userId = Session["UserId"].ToString();
                loadStudentData();
            }

            // Aquí es donde llamas al método que lee el CSV
            if (!IsPostBack)
            {
                CargarConfiguracionAviso(); // <-- Te faltaba llamar al método aquí
            }
        }
        private void CargarConfiguracionAviso()
        {
            if (Session["ModalAvisoMostrado"] != null)
            {
                FechaInicioJS = "";
                FechaFinJS = "";
                UrlImagenJS = "";
                return;
            }

            try
            {
                string urlArchivo = "https://idiomas.unjfsc.edu.pe/intranet/avisos/config_aviso.csv";

                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    string contenido = client.DownloadString(urlArchivo);

                    if (!string.IsNullOrEmpty(contenido))
                    {
                        string[] datos = contenido.Split(',');

                        if (datos.Length >= 3)
                        {
                            FechaInicioJS = datos[0].Trim();
                            FechaFinJS = datos[1].Trim();
                            UrlImagenJS = datos[2].Trim();

                            Session["ModalAvisoMostrado"] = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Si no se puede acceder al CSV, el modal simplemente no abre
            }
        }

        protected void btnEnableTextBox(object sender, EventArgs e)
        {

        }

        private void loadStudentData()
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("detalle_alumno_por_id", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("@id", userId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblEstudiante.Text = dr["apellidos"].ToString() + " " + dr["nombre"].ToString();
                    Session["CarnetId"] = dr["numerocarnet"].ToString();
                    lblNCarnet.Text = dr["numerocarnet"].ToString();
                    lblCorreo.Text = dr["correoElectronico"].ToString();
                    lblCelular.Text = dr["celular"].ToString();
                    dni = dr["dni"].ToString();
                    lblDni.Text = dni;
                    lblNacimiento.Text = Convert.ToDateTime(dr["fechanacimiento"]).ToString("dd/MM/yyyy");
                    lblGenero.Text = dr["sexo"].ToString();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
                dr.Close();
                cmd.Connection.Close();

                ///////////////////////////////////////////////////////////////////////////////

                SqlCommand cmd2 = new SqlCommand("buscar_tipo_alumno", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd2.Connection.Open();
                cmd2.Parameters.Add(new SqlParameter("@dni", dni));
                SqlDataReader dr2 = cmd2.ExecuteReader();
                if (dr2.Read()) lblTipo.Text = dr2["Descripcion"].ToString();
                dr2.Close();
                cmd2.Connection.Close();
            }
        }

         protected void btnActivarCampos_Click(object sender, EventArgs e) {
            switchCampos(true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            switchCampos(false);
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            string dni = lblDni.Text;
            string correo = txtCorreo.Text;
            string celular = txtCelular.Text;

            if (dni.Trim() != "" && correo.Trim() != "" && celular.Trim() != "") 
            {
                bool guardado = false;
                guardado = editarAlumno(dni, correo, celular);

               if (guardado)
                {
                    loadStudentData();
                    switchCampos(false);
                }
                else 
                {
                    Console.WriteLine("Error al guardar");
                }

            }
        }

        private void switchCampos(bool b)
        {
            if (b)
            {
                txtCorreo.Text = lblCorreo.Text;
                txtCelular.Text = lblCelular.Text;
            }

            lblCorreo.Visible = !b;
            lblCelular.Visible = !b;
            txtCorreo.Visible = b;
            txtCelular.Visible = b;
            btnGuardarCambios.Visible = b;
            btnCancelar.Visible = b;
            txtCorreo.Focus();
        }

        private bool editarAlumno(string dni, string correoElectronico, string celular)
        {
            try
            {
                string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
                using (SqlConnection sqlConnection = new SqlConnection(connect))
                {
                    SqlCommand cmd = new SqlCommand("editar_perfil_alumno", sqlConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Connection.Open();
                    cmd.Parameters.Add(new SqlParameter("@dni", dni));
                    cmd.Parameters.Add(new SqlParameter("@correo", correoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@celular", celular));
                    cmd.ExecuteNonQuery();
                    return true;
                }
                
            } catch
            {
                return false;
            }
        }    
    }
}