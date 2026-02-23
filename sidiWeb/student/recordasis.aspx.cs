using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace sidiWeb.student
{
    public partial class recordasis : System.Web.UI.Page
    {
        string userId, carnetId;
        DataTable allGradesData;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || Session["CarnetId"] == null)
            {
                Response.Redirect("../Login.aspx");
                return;
            }

            userId = Session["UserId"].ToString();
            carnetId = Session["CarnetId"].ToString();

            if (!IsPostBack)
            {
                CargarIdiomas();
            }
            else if (ViewState["AllGradesData"] != null)
            {
                allGradesData = (DataTable)ViewState["AllGradesData"];
            }
        }

        private void CargarIdiomas()
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("buscar_idiomas_asistencia", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@numerocarnet", carnetId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                ddlIdiomaFiltro.Items.Clear();
                ddlIdiomaFiltro.Items.Add(new ListItem("-- Seleccione --", ""));

                while (dr.Read())
                {
                    // IMPORTANTE: El primer parámetro es el TEXTO (nombre), 
                    // el segundo es el VALOR (ID). Esto es lo que faltaba.
                    ddlIdiomaFiltro.Items.Add(new ListItem(
                        dr["nombreIdioma"].ToString(),
                        dr["idIdioma"].ToString()
                    ));
                }
            }
        }

        protected void ddlIdiomaFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCicloFiltro.Items.Clear();
            LimpiarTablas();

            if (!string.IsNullOrEmpty(ddlIdiomaFiltro.SelectedValue))
            {
                string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connect))
                {
                    // 1. Cambiamos el nombre del SP si fuera necesario, pero aquí usamos el que pusiste
                    SqlCommand cmd = new SqlCommand("listar_grupos_alumno", con) { CommandType = CommandType.StoredProcedure };

                    // 2. IMPORTANTE: Los nombres de parámetros deben coincidir EXACTAMENTE con el SQL
                    cmd.Parameters.AddWithValue("@nrocarnet", carnetId);

                    // 3. CAMBIO CLAVE: Aquí debe ser @idIdioma (como en el SP) 
                    // y el Value debe ser el ID (asegúrate que ddlIdiomaFiltro cargue el ID en el Value)
                    cmd.Parameters.AddWithValue("@idIdioma", ddlIdiomaFiltro.SelectedValue);

                    // 4. Agregamos el nivel como NULL para que el SP nos devuelva todos los ciclos de ese idioma
                    cmd.Parameters.AddWithValue("@nivel", DBNull.Value);

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddlCicloFiltro.Items.Add(new ListItem("-- Seleccione --", ""));
                    while (dr.Read())
                    {
                        // Mantenemos la lógica de mostrar Nivel + Ciclo
                        ddlCicloFiltro.Items.Add(new ListItem($"{dr["nombreNivel"]} {dr["ciclo"]}", dr["idGrupo"].ToString()));
                    }
                }
            }
        }

        protected void ddlCicloFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlCicloFiltro.SelectedValue))
            {
                string idGrupo = ddlCicloFiltro.SelectedValue;
                ConsultarDatos(idGrupo);
            }
        }

        private void ConsultarDatos(string idGrupo)
        {
            LoadDaysLimit(idGrupo);
            LoadAssistance(idGrupo, carnetId);
            LoadAllGrades(idGrupo, userId);
            LoadPonderate(idGrupo, userId);
            gvPromedios.Visible = true;
            gvNotas.Visible = false;
        }

        // Métodos de carga de datos (Asistencia, Notas, Límites) se mantienen igual al bloque anterior 
        // para asegurar que las SP funcionen correctamente.

        private void LoadAssistance(string idGrupo, string carnet)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("listar_asistencia_alumno", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@nrocarnet", carnet);
                cmd.Parameters.AddWithValue("@idgrupo", idGrupo);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                int f = 0;
                foreach (DataRow r in dt.Rows) if (r["nombreAsistencia"].ToString() == "F") f++;

                lblFalta.Text = f.ToString();
                gvAsistencias.DataSource = dt;
                gvAsistencias.DataBind();

                int lim = 0; int.TryParse(lblLimiteFaltas.Text, out lim);
                bool inhabilitado = f > lim;
                lblEstado.Text = inhabilitado ? "INHABILITADO" : "HABILITADO";
                lblEstado.CssClass = inhabilitado ? "badge rounded-pill bg-danger" : "badge rounded-pill bg-success";
            }
        }

        private void LoadDaysLimit(string idGrupo)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("listar_dias_grupo", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@idgrupo", idGrupo);
                con.Open();
                int c = 0;
                using (SqlDataReader dr = cmd.ExecuteReader()) { while (dr.Read()) c++; }
                lblLimiteFaltas.Text = c.ToString();
            }
        }

        private void LoadAllGrades(string idGrupo, string uId)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("sp_notas_alumno_grupo", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                cmd.Parameters.AddWithValue("@idalumno", uId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                allGradesData = new DataTable();
                da.Fill(allGradesData);
                ViewState["AllGradesData"] = allGradesData;

                ddlTipoNota.Items.Clear();
                ddlTipoNota.Items.Add(new ListItem("Promedios actuales", "P"));
                DataView view = new DataView(allGradesData);
                DataTable dtSkills = view.ToTable(true, "tipoNota");
                foreach (DataRow r in dtSkills.Rows) ddlTipoNota.Items.Add(new ListItem(r["tipoNota"].ToString(), r["tipoNota"].ToString()));
            }
        }

        private void LoadPonderate(string idGrupo, string uId)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("sp_promedios_alumno_grupo", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                cmd.Parameters.AddWithValue("@idalumno", uId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvPromedios.DataSource = dt;
                gvPromedios.DataBind();
            }
        }

        protected void ddlTipoNota_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoNota.SelectedValue == "P") { gvPromedios.Visible = true; gvNotas.Visible = false; }
            else
            {
                DataView dv = allGradesData.DefaultView;
                dv.RowFilter = $"tipoNota = '{ddlTipoNota.SelectedValue}'";
                gvNotas.DataSource = dv;
                gvNotas.DataBind();
                gvNotas.Visible = true; gvPromedios.Visible = false;
            }
        }

        protected void gvPromedios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int cellIdx = e.Row.Cells.Count - 1;
                if (decimal.TryParse(e.Row.Cells[cellIdx].Text, out decimal val))
                {
                    e.Row.Cells[cellIdx].ForeColor = (val >= 14) ? Color.Green : Color.Red;
                    e.Row.Cells[cellIdx].Font.Bold = true;
                }
            }
        }

        private void LimpiarTablas()
        {
            gvAsistencias.DataSource = null; gvAsistencias.DataBind();
            gvPromedios.DataSource = null; gvPromedios.DataBind();
            lblFalta.Text = "0"; lblLimiteFaltas.Text = "0";
            lblEstado.Text = "";
        }
    }
}