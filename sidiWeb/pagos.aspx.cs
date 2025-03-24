using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sidiWeb
{
    public partial class pagos : System.Web.UI.Page
    {
        String dni, userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = Session["UserId"].ToString();
            if (userId != null)
            {
                loadPayments(userId);
            }
            else
            {
                Session.Clear();
                Response.Redirect("Login.aspx");
            }
        }

        private void loadPayments(string userId)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("mostrar_relacion", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("@cod", userId));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvPagos.DataSource = dt;
                gvPagos.DataBind();
                cmd.Connection.Close();
            }
        }
    }
}