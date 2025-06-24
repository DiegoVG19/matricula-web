using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sidiWeb.administrative
{
    public partial class index : System.Web.UI.Page
    {
        String userId;
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
                loadAdminData();
            }
        }

        private void loadAdminData()
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("sp_detalle_trabajador_id", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("@idtrabajador", userId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblAdministrador.Text = " "+dr["nombre"].ToString()+" "+dr["apaterno"].ToString()+" "+dr["amaterno"].ToString();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
                dr.Close();
                cmd.Connection.Close();
            }
        }

    }
}