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
            DateTime fechaNacimiento = DateTime.Parse(txtNacimiento.Text);
            string genero = ddlGenero.Text;

            if (dni.Trim() != "" && correo.Trim() != "" && celular.Trim() != "" && fechaNacimiento != null && genero != "") 
            {
                bool guardado = false;
                guardado = editarAlumno(dni, correo, celular, fechaNacimiento, genero);

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
                var fechaNacimiento = DateTime.Parse(lblNacimiento.Text);
                txtNacimiento.Text = fechaNacimiento.ToString("yyyy-MM-dd");
                ddlGenero.SelectedValue = lblGenero.Text; 
            }

            lblCorreo.Visible = !b;
            lblCelular.Visible = !b;
            lblNacimiento.Visible = !b;
            lblGenero.Visible = !b;
            txtCorreo.Visible = b;
            txtCelular.Visible = b;
            txtNacimiento.Visible = b;
            ddlGenero.Visible = b;
            btnGuardarCambios.Visible = b;
            btnCancelar.Visible = b;
            txtCorreo.Focus();
        }

        private bool editarAlumno(string dni, string correoElectronico, string celular, DateTime fechaNacimiento, string genero)
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
                    cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", fechaNacimiento));
                    cmd.Parameters.Add(new SqlParameter("@sexo", genero));
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