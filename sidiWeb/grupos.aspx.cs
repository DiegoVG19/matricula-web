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
    public partial class grupos : System.Web.UI.Page
    {
        String dni, userId, carnetId;
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = Session["UserId"].ToString();
            carnetId = Session["CarnetId"].ToString();
            if (!IsPostBack)
            {
                if (userId != null)
                {
                    selectLanguage(carnetId);
                }
                else
                {
                    Session.Clear();
                    Response.Redirect("Login.aspx");
                }
            }
            if (!string.IsNullOrEmpty(ddlIdiomas.SelectedValue))
            {
                loadCourses(carnetId, ddlIdiomas.SelectedItem.ToString());
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlIdiomas.SelectedValue))
            {
                loadCourses(carnetId, ddlIdiomas.SelectedItem.ToString());
            }
        }

        private void selectLanguage(String carnetId)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("buscar_idiomas_asistencia", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("@numerocarnet", carnetId));
                SqlDataReader dr = cmd.ExecuteReader();
                ddlIdiomas.Items.Clear();
                ddlIdiomas.Items.Add(new ListItem("Seleccione algún idioma", ""));

                while (dr.Read())
                {
                    ddlIdiomas.Items.Add(new ListItem(
                       dr["nombreIdioma"].ToString(),
                       dr["idIdioma"].ToString()
                    ));
                }
                cmd.Connection.Close();
            }
        }

        protected void ddlIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlIdiomas.SelectedValue))
            {
                pnlGrupos.Controls.Clear();
                return;
            }

            loadCourses(carnetId, ddlIdiomas.SelectedItem.ToString());
        }

        private void loadCourses(String carnetId, String language)
        {
            pnlGrupos.Controls.Clear();
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("listar_grupos_alumno", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("@nrocarnet", carnetId));
                cmd.Parameters.Add(new SqlParameter("@idioma", language));
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string groupId = dr["IdGrupo"].ToString();
                    Response.Write($"<script>console.log('Creando botón para grupo {groupId}');</script>");

                    LinkButton btn = new LinkButton
                    {
                        CssClass = "group-button",
                        ID = "grupo_" + groupId,  // Se evita ID duplicado
                        Text = $"<i class='fas fa-book'></i> " +
                               $"<span>{dr["numero"]}</span>" +
                               $"<span>{dr["nombreNivel"]} {dr["ciclo"]}</span>",
                        CommandArgument = groupId
                    };
                    btn.Click += new EventHandler(btnGrupo_Click);

                    pnlGrupos.Controls.Add(new LiteralControl("<div class='group-item'>"));
                    pnlGrupos.Controls.Add(btn);
                    pnlGrupos.Controls.Add(new LiteralControl("</div>"));
                }
                cmd.Connection.Close();
            }
        }
        protected void btnGrupo_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idGrupo = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect($"DetalleGrupo.aspx?grupo={idGrupo}");
        }
    }
}