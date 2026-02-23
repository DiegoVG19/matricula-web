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
        string userId, carnetId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 1. Verificación de Seguridad
            if (Session["UserId"] == null || Session["CarnetId"] == null)
            {
                Session.Clear();
                Response.Redirect("Login.aspx");
                return;
            }

            userId = Session["UserId"].ToString();
            carnetId = Session["CarnetId"].ToString();

            if (!IsPostBack)
            {
                selectLanguage(carnetId);
            }
            else
            {
                // CRÍTICO: Si hay un postback (clic en un grupo), los botones deben re-crearse
                // antes de que ASP.NET intente ejecutar el evento Click.
                if (!string.IsNullOrEmpty(ddlIdiomas.SelectedValue) && !string.IsNullOrEmpty(ddlNiveles.SelectedValue))
                {
                    loadCourses(carnetId, ddlIdiomas.SelectedValue, ddlNiveles.SelectedValue);
                }
            }
        }

        private void selectLanguage(string carnetId)
        {
            if (string.IsNullOrEmpty(carnetId)) return;

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
        }

        protected void ddlIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlIdiomas.SelectedValue))
            {
                // Limpiamos grupos previos
                pnlGrupos.Controls.Clear();
                // Cargamos solo los niveles que el alumno realmente tiene en ese idioma
                CargarNivelesReales(carnetId, ddlIdiomas.SelectedValue);
            }
            else
            {
                ddlNiveles.Items.Clear();
                ddlNiveles.Enabled = false;
                pnlGrupos.Controls.Clear();
            }
        }

        private void CargarNivelesReales(string carnet, string idIdioma)
        {
            ddlNiveles.Items.Clear();
            ddlNiveles.Items.Add(new ListItem("Seleccione el Nivel...", ""));

            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                // Usamos el SP enviando nivel NULL para obtener todos los niveles que tiene el alumno
                SqlCommand cmd = new SqlCommand("listar_grupos_alumno", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@nrocarnet", carnet));
                cmd.Parameters.Add(new SqlParameter("@idIdioma", idIdioma));
                cmd.Parameters.Add(new SqlParameter("@nivel", DBNull.Value));

                sqlConnection.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                HashSet<string> nivelesEncontrados = new HashSet<string>();

                while (dr.Read())
                {
                    string nivelNombre = dr["nombreNivel"].ToString();
                    if (!nivelesEncontrados.Contains(nivelNombre))
                    {
                        nivelesEncontrados.Add(nivelNombre);
                        ddlNiveles.Items.Add(new ListItem(nivelNombre, nivelNombre));
                    }
                }
                ddlNiveles.Enabled = nivelesEncontrados.Count > 0;
            }
        }

        protected void ddlNiveles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlNiveles.SelectedValue))
            {
                loadCourses(carnetId, ddlIdiomas.SelectedValue, ddlNiveles.SelectedValue);
            }
            else
            {
                pnlGrupos.Controls.Clear();
            }
        }

        private void loadCourses(string carnetId, string idIdioma, string nivel)
        {
            if (string.IsNullOrEmpty(carnetId) || string.IsNullOrEmpty(idIdioma) || pnlGrupos == null) return;

            pnlGrupos.Controls.Clear();
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("listar_grupos_alumno", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@nrocarnet", carnetId));
                cmd.Parameters.Add(new SqlParameter("@idIdioma", Convert.ToInt32(idIdioma)));
                cmd.Parameters.Add(new SqlParameter("@nivel", nivel));

                sqlConnection.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string groupId = dr["idGrupo"].ToString();

                    LinkButton btn = new LinkButton
                    {
                        CssClass = "group-button",
                        ID = "grupo_" + groupId,
                        Text = $"<i class='fas fa-book'></i> " +
                               $"<span>{dr["numero"]}</span>" +
                               $"<span>{dr["nombreNivel"]} {dr["ciclo"]}</span>",
                        CommandArgument = groupId
                    };
                    // Suscribimos el evento Click
                    btn.Click += new EventHandler(btnGrupo_Click);

                    pnlGrupos.Controls.Add(new LiteralControl("<div class='group-item'>"));
                    pnlGrupos.Controls.Add(btn);
                    pnlGrupos.Controls.Add(new LiteralControl("</div>"));
                }
            }
        }

        protected void btnGrupo_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            if (btn != null && !string.IsNullOrEmpty(btn.CommandArgument))
            {
                loadGroupData(btn.CommandArgument);

                // Disparar el modal usando ScriptManager (más fiable en postbacks)
                string script = "var myModal = new bootstrap.Modal(document.getElementById('modalInfoGrupo')); myModal.show();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", script, true);
            }
        }

        private void loadGroupData(string idGrupo)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("listar_grupo_seleccion", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@idGrupo", idGrupo));

                sqlConnection.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    lblNumero.Text = dr["numero"].ToString();
                    lblIdioma.Text = dr["nombreIdioma"].ToString();
                    lblNivel.Text = dr["nombreNivel"].ToString();
                    lblCiclo.Text = dr["ciclo"].ToString();
                    lblModalidad.Text = dr["modalidad"].ToString();

                    if (dr["fechaInicio"] != DBNull.Value && dr["fechaFinal"] != DBNull.Value)
                    {
                        lblDuracion.Text = Convert.ToDateTime(dr["fechaInicio"]).ToString("dd/MM/yyyy") + " - " +
                                           Convert.ToDateTime(dr["fechaFinal"]).ToString("dd/MM/yyyy");
                    }
                    lblHorario.Text = dr["horaInicio"].ToString() + " - " + dr["horaFinal"].ToString();
                    lblDocente.Text = dr["DOCENTE"].ToString();
                    lblDias.Text = dr["dias"].ToString().ToUpper();
                }
            }
        }
    }
}