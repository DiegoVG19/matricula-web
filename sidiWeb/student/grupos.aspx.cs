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
            // Verificar si las variables de sesión existen antes de usarlas
            if (Session["UserId"] == null || Session["CarnetId"] == null)
            {
                Session.Clear();
                Response.Redirect("Login.aspx");
                return;
            }

            // Inicializar variables después de verificar que existen
            Session["idGrupo"] = 0;
            userId = Session["UserId"].ToString();
            carnetId = Session["CarnetId"].ToString();

            if (!IsPostBack)
            {
                selectLanguage(carnetId);
            }

            // Verificar que ddlIdiomas no sea null y tenga un valor seleccionado
            if (ddlIdiomas != null && !string.IsNullOrEmpty(ddlIdiomas.SelectedValue))
            {
                loadCourses(carnetId, ddlIdiomas.SelectedItem.ToString());
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // En Page_Init, ddlIdiomas podría no estar inicializado aún
            // Es mejor mover esta lógica a Page_Load o a un evento posterior
            // Si es necesario mantenerlo aquí, agregar verificaciones
            if (ddlIdiomas != null && !string.IsNullOrEmpty(ddlIdiomas.SelectedValue))
            {
                // Verificar que Session["CarnetId"] exista
                if (Session["CarnetId"] != null)
                {
                    string tempCarnetId = Session["CarnetId"].ToString();
                    loadCourses(tempCarnetId, ddlIdiomas.SelectedItem.ToString());
                }
            }
        }

        private void selectLanguage(String carnetId)
        {
            if (string.IsNullOrEmpty(carnetId))
            {
                return; // Evitar procesar si carnetId es nulo o vacío
            }

            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                    SqlCommand cmd = new SqlCommand("buscar_idiomas_asistencia", sqlConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@numerocarnet", carnetId));

                    sqlConnection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Verificar que ddlIdiomas no sea null antes de usarlo
                    if (ddlIdiomas != null)
                    {
                        ddlIdiomas.Items.Clear();
                        ddlIdiomas.Items.Add(new ListItem("Seleccione algún idioma", ""));

                        while (dr.Read())
                        {
                            ddlIdiomas.Items.Add(new ListItem(
                               dr["nombreIdioma"].ToString(),
                               dr["idIdioma"].ToString()
                            ));
                        }
                    }
                    sqlConnection.Close();
                
            }
        }

        protected void ddlIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar que ddlIdiomas no sea null
            if (ddlIdiomas == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(ddlIdiomas.SelectedValue))
            {
                if (pnlGrupos != null)
                {
                    pnlGrupos.Controls.Clear();
                }
                return;
            }

            // Verificar que carnetId no sea null
            if (Session["CarnetId"] != null)
            {
                string tempCarnetId = Session["CarnetId"].ToString();
                loadCourses(tempCarnetId, ddlIdiomas.SelectedItem.ToString());
            }
        }

        private void loadCourses(String carnetId, String language)
        {
            // Verificar parámetros
            if (string.IsNullOrEmpty(carnetId) || string.IsNullOrEmpty(language) || pnlGrupos == null)
            {
                return;
            }

            pnlGrupos.Controls.Clear();
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                    SqlCommand cmd = new SqlCommand("listar_grupos_alumno", sqlConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@nrocarnet", carnetId));
                    cmd.Parameters.Add(new SqlParameter("@idioma", language));

                    sqlConnection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        string groupId = dr["IdGrupo"].ToString();

                        LinkButton btn = new LinkButton
                        {
                            CssClass = "group-button",
                            ID = "grupo_" + groupId,
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
                    sqlConnection.Close();
            }
        }

        protected void btnGrupo_Click(object sender, EventArgs e)
        {
                LinkButton btn = (LinkButton)sender;
                if (btn != null && !string.IsNullOrEmpty(btn.CommandArgument))
                {
                    int idGrupo = Convert.ToInt32(btn.CommandArgument);
                    Session["idGrupo"] = idGrupo;
                    Response.Redirect($"detallegrupo.aspx?grupo={idGrupo}");
                }
        }
    }
}