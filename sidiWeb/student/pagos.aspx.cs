using sidiWeb.Code.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using sidiWeb.Code.Api;
using System.Globalization;

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

        private string ObtenerNivelPorTributo(string tributo)
        {
            string nivel = "";

            string cadena = ConfigurationManager
                .ConnectionStrings["dbSidi"]
                .ConnectionString;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                string sql = @"
            SELECT 
                i.nombreIdioma,
                n.nombreNivel
            FROM PrecioMatricula pm
            INNER JOIN Idioma i
                ON pm.idIdioma = i.idIdioma
            INNER JOIN Nivel n
                ON pm.idNivel = n.idNivel
            WHERE pm.tributo = @tributo";


                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue(
                    "@tributo",
                    tributo
                );


                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();


                if (dr.Read())
                {
                    nivel =
                        dr["nombreIdioma"].ToString()
                        + " "
                        +
                        dr["nombreNivel"].ToString();
                }
            }

            return nivel;
        }

        private void loadPayments()
        {
            string dni = Session["Dni"].ToString();

            ApiPagosService api = new ApiPagosService();

            PagoApiResponse respuesta = api.ObtenerPagos(dni);

            DataTable dt = new DataTable();

            dt.Columns.Add("IdPago", typeof(int));
            dt.Columns.Add("Fech", typeof(DateTime));
            dt.Columns.Add("Concepto");
            dt.Columns.Add("Nivel");
            dt.Columns.Add("Monto", typeof(decimal));
            dt.Columns.Add("Recibo");

            if (respuesta != null && respuesta.data != null)
            {
                foreach (var pago in respuesta.data)
                {
                    DataRow row = dt.NewRow();

                    row["IdPago"] = pago.identificador;

                    DateTime fecha;

                    if (DateTime.TryParse(pago.fecha, out fecha))
                        row["Fech"] = fecha;
                    else
                        row["Fech"] = DBNull.Value;

                    string codigo = pago.concepto?.codigo ?? "";

                    string tributo = codigo.Replace("TUS", "");


                    if (tributo == "633")
                    {
                        // Pago antiguo
                        row["Concepto"] = pago.concepto?.nombre ?? "";

                        row["Nivel"] = "";
                    }
                    else
                    {
                        // Pago nuevo
                        row["Concepto"] = tributo;

                        row["Nivel"] = ObtenerNivelPorTributo(tributo);
                    }


                    row["Monto"] = pago.monto;


                    row["Recibo"] = pago.transaccionId;

                    dt.Rows.Add(row);
                }
            }

            DataView dv = dt.DefaultView;

            if (!string.IsNullOrEmpty(txtSearchRecibo.Text))
            {
                dv.RowFilter = string.Format(
                    "Recibo LIKE '%{0}%'",
                    txtSearchRecibo.Text.Trim());
            }

            dv.Sort = ViewState["SortExp"] + " " + ViewState["SortDir"];

            gvPagos.DataSource = dv;
            gvPagos.DataBind();
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