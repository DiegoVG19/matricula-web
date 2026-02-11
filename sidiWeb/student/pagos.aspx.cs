using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sidiWeb.student
{
    public partial class pagos : System.Web.UI.Page
    {
        // Declaramos userId a nivel de clase para que todos los métodos lo vean
        private string userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            userId = Session["UserId"].ToString();

            // AGREGAR ESTO: Detecta el PostBack del buscador
            if (IsPostBack)
            {
                string target = Request.Form["__EVENTTARGET"];
                if (!string.IsNullOrEmpty(target) && target.Contains("txtSearchRecibo"))
                {
                    txtSearchRecibo_TextChanged(txtSearchRecibo, EventArgs.Empty);
                }
            }

            if (!IsPostBack)
            {
                ViewState["SortExp"] = "Fech";
                ViewState["SortDir"] = "DESC";
                loadPayments();
            }
        }

        private void loadPayments()
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("mostrar_relacion", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Usamos la variable de clase userId
                cmd.Parameters.Add(new SqlParameter("@cod", userId));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                DataView dv = dt.DefaultView;

                // Filtro de búsqueda por Recibo
                if (!string.IsNullOrEmpty(txtSearchRecibo.Text))
                {
                    dv.RowFilter = string.Format("Recibo LIKE '%{0}%'", txtSearchRecibo.Text.Trim());
                }

                // Aplicar ordenamiento dinámico
                dv.Sort = ViewState["SortExp"].ToString() + " " + ViewState["SortDir"].ToString();

                gvPagos.DataSource = dv;
                gvPagos.DataBind();
            }
        }

        // Evento para el buscador
        protected void txtSearchRecibo_TextChanged(object sender, EventArgs e)
        {
            gvPagos.PageIndex = 0;
            loadPayments();
        }

        // Evento para cambiar de página (1, 2, 3...)
        protected void gvPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPagos.PageIndex = e.NewPageIndex;
            loadPayments();
        }

        // Evento para ordenar al hacer clic en las columnas
        protected void gvPagos_Sorting(object sender, GridViewSortEventArgs e)
        {
            string currentExp = e.SortExpression;
            if (ViewState["SortExp"].ToString() == currentExp)
            {
                ViewState["SortDir"] = ViewState["SortDir"].ToString() == "ASC" ? "DESC" : "ASC";
            }
            else
            {
                ViewState["SortExp"] = currentExp;
                ViewState["SortDir"] = "ASC";
            }
            loadPayments();
        }

    }
}