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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            login();
        }

        private void login()
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("sp_login", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("@usuario", txtUsuario.Text));
                cmd.Parameters.Add(new SqlParameter("@password", txtContrasena.Text));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Session["UserId"] = dr["idAlumno"].ToString();
                    Response.Redirect("index.aspx");
                }
                else
                {
                    lblError.Text = "INVALIDO";
                }
                cmd.Connection.Close();
            }
        }

    }
}