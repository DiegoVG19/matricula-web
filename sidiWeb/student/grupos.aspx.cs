using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                Response.Redirect("Login.aspx");
                return;
            }

            userId = Session["UserId"].ToString();
            carnetId = Session["CarnetId"].ToString();

            if (!IsPostBack)
            {
                CargarIdiomas(carnetId);
            }
            else
            {
                // CRÍTICO: Re-generar botones dinámicos en Postback para que el evento Click funcione
                if (!string.IsNullOrEmpty(ddlIdiomas.SelectedValue) && !string.IsNullOrEmpty(ddlNiveles.SelectedValue))
                {
                    loadCourses(carnetId, ddlIdiomas.SelectedValue, ddlNiveles.SelectedValue);
                }
            }
        }

        private void CargarIdiomas(string carnet)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("buscar_idiomas_asistencia", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@numerocarnet", carnet);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                ddlIdiomas.Items.Clear();
                ddlIdiomas.Items.Add(new ListItem("Seleccione un idioma", ""));

                while (dr.Read())
                {
                    // Usamos el ID como Value para evitar errores de parámetros en los siguientes SP
                    ddlIdiomas.Items.Add(new ListItem(dr["nombreIdioma"].ToString(), dr["idIdioma"].ToString()));
                }
            }
        }

        protected void ddlIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlIdiomas.SelectedValue))
            {
                pnlGrupos.Controls.Clear();
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
            using (SqlConnection con = new SqlConnection(connect))
            {
                // Enviamos @nivel como DBNull para obtener todos los niveles registrados del alumno
                SqlCommand cmd = new SqlCommand("listar_grupos_alumno", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@nrocarnet", carnet);
                cmd.Parameters.AddWithValue("@idIdioma", idIdioma);
                cmd.Parameters.AddWithValue("@nivel", DBNull.Value);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                HashSet<string> nivelesUnicos = new HashSet<string>();

                while (dr.Read())
                {
                    string nivel = dr["nombreNivel"].ToString();
                    if (!nivelesUnicos.Contains(nivel))
                    {
                        nivelesUnicos.Add(nivel);
                        ddlNiveles.Items.Add(new ListItem(nivel, nivel));
                    }
                }
                ddlNiveles.Enabled = (ddlNiveles.Items.Count > 1);
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

        private void loadCourses(string carnet, string idIdioma, string nivel)
        {
            pnlGrupos.Controls.Clear();
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("listar_grupos_alumno", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@nrocarnet", carnet);
                cmd.Parameters.AddWithValue("@idIdioma", idIdioma);
                cmd.Parameters.AddWithValue("@nivel", nivel);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string groupId = dr["idGrupo"].ToString();

                    LinkButton btn = new LinkButton
                    {
                        CssClass = "group-button",
                        ID = "btnG_" + groupId,
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
            }
        }

        protected void btnGrupo_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            if (btn != null && !string.IsNullOrEmpty(btn.CommandArgument))
            {
                loadGroupData(btn.CommandArgument);
                // Invocamos el modal de Bootstrap
                string script = "var myModal = new bootstrap.Modal(document.getElementById('modalInfoGrupo')); myModal.show();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", script, true);
            }
        }

        private void loadGroupData(string idGrupo)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("listar_grupo_seleccion", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@idGrupo", idGrupo);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    lblNumero.Text = dr["numero"].ToString();
                    lblIdioma.Text = dr["nombreIdioma"].ToString();
                    lblNivel.Text = dr["nombreNivel"].ToString();
                    lblCiclo.Text = dr["ciclo"].ToString();
                    lblModalidad.Text = dr["modalidad"].ToString();
                    lblDocente.Text = dr["DOCENTE"].ToString();
                    lblDias.Text = dr["dias"].ToString().ToUpper();
                    lblHorario.Text = dr["horaInicio"].ToString() + " - " + dr["horaFinal"].ToString();

                    if (dr["fechaInicio"] != DBNull.Value)
                    {
                        lblDuracion.Text = Convert.ToDateTime(dr["fechaInicio"]).ToString("dd/MM/yyyy") + " - " +
                                           Convert.ToDateTime(dr["fechaFinal"]).ToString("dd/MM/yyyy");
                    }
                }
            }
        }
    }
}