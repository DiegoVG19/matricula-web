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
            userId = Session["UserId"].ToString();
            if (userId != null)
            {
                loadStudentData();
            }
            else
            {
                Session.Clear();
                Response.Redirect("Login.aspx");
            }
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
    }
}