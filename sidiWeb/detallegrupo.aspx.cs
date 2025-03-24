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
    public partial class detallegrupo : System.Web.UI.Page
    {
        String dni, userId, carnetId;
        int faltasPermitidas, faltas, estado, idGrupo;
        private DataTable allGradesData;
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = Session["UserId"].ToString();
            carnetId = Session["CarnetId"].ToString();
            idGrupo = Convert.ToInt32(Request.QueryString["grupo"]);
            if (!IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    if (Request.QueryString["grupo"] != null)
                    {
                        idGrupo = Convert.ToInt32(Request.QueryString["grupo"]);
                        loadGroupData(idGrupo);
                    }
                    else
                    {
                        Response.Redirect("Grupos.aspx");
                    }
                }
                else
                {
                    Session.Clear();
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                if (ViewState["AllGradesData"] != null)
                {
                    allGradesData = (DataTable)ViewState["AllGradesData"];
                }
            }
        }

        protected void loadHabilities()
        {
            switch (lblIdioma.Text)
            {
                case "INGLÉS":
                    ddlTipoNota.Items.Add(new ListItem("WRITING", "WRITING"));
                    ddlTipoNota.Items.Add(new ListItem("SPEAKING", "SPEAKING"));
                    ddlTipoNota.Items.Add(new ListItem("LISTENING", "LISTENING"));
                    ddlTipoNota.Items.Add(new ListItem("READING", "READING"));
                    ddlTipoNota.Items.Add(new ListItem("USE OF ENGLISH", "USE OF ENGLISH"));
                    break;
                case "PORTUGUÉS":
                    ddlTipoNota.Items.Add(new ListItem("COMPRENSAO AUDITIVA", "COMPRENSAO AUDITIVA"));
                    ddlTipoNota.Items.Add(new ListItem("INTERPRETACAO DO TEXTO", "INTERPRETACAO DO TEXTO"));
                    ddlTipoNota.Items.Add(new ListItem("PRODUCAO ESCRITA", "PRODUCAO ESCRITA"));
                    ddlTipoNota.Items.Add(new ListItem("PRODUCAO ORAL", "PRODUCAO ORAL"));
                    ddlTipoNota.Items.Add(new ListItem("GRAMÁTICA", "GRAMÁTICA"));
                    break;
                case "ITALIANO":
                    ddlTipoNota.Items.Add(new ListItem("COMPRENSIONE AUDITIVA", "COMPRENSIONE AUDITIVA"));
                    ddlTipoNota.Items.Add(new ListItem("INTERPRETAZIONE DI TESTO", "INTERPRETAZIONE DI TESTO"));
                    ddlTipoNota.Items.Add(new ListItem("PRODUZIONE SCRITTA", "PRODUZIONE SCRITTA"));
                    ddlTipoNota.Items.Add(new ListItem("PRODUZIONE ORALE", "PRODUZIONE ORALE"));
                    ddlTipoNota.Items.Add(new ListItem("GRAMMATICA", "GRAMMATICA"));
                    break;
                case "QUECHUA":
                    ddlTipoNota.Items.Add(new ListItem("JELLJANAPAJ", "JELLJANAPAJ"));
                    ddlTipoNota.Items.Add(new ListItem("RIMANAPAJ", "RIMANAPAJ"));
                    ddlTipoNota.Items.Add(new ListItem("RUWANAPAJ", "JELLJANAPAJ"));
                    break;
            }
        }
        protected void btnMostrarAsistencias_Click(object sender, EventArgs e)
        {
            loadDays(idGrupo);
            loadAssistance(idGrupo, carnetId);
            pnlAsistencias.Visible = true;
        }

        protected void btnMostrarNotas_Click(object sender, EventArgs e)
        {
            ddlTipoNota.Items.Clear();

            ddlTipoNota.Items.Add(new ListItem("Promedios hasta ahora", "P"));

            // Cargar las habilidades según el idioma
            loadHabilities();

            LoadAllGrades(idGrupo, userId);
            loadPonderate(idGrupo, userId);

            // Configurar visibilidad inicial
            ddlTipoNota.SelectedValue = "P"; // Establecer a Promedios por defecto
            gvPromedios.Visible = true;
            gvNotas.Visible = false;

            pnlNotas.Visible = true;
        }

        protected void ddlTipoNota_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoNota = ddlTipoNota.SelectedValue;

            switch (tipoNota)
            {
                case "P":
                    // Mostrar promedios
                    gvPromedios.Visible = true;
                    gvNotas.Visible = false;
                    break;
                default:
                    FilterAndDisplayGrades();
                    gvNotas.Visible = true;
                    gvPromedios.Visible = false;
                    break;
            }
        }

        private void LoadAllGrades(int idGrupo, String userId)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("sp_notas_alumno_grupo", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@idGrupo", idGrupo));
                cmd.Parameters.Add(new SqlParameter("@idalumno", userId));

                sqlConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                allGradesData = new DataTable();
                da.Fill(allGradesData);

                // Formatear las fechas
                if (allGradesData.Columns.Contains("fecha"))
                {
                    allGradesData.Columns.Add("FechaFormateada", typeof(string));
                    foreach (DataRow row in allGradesData.Rows)
                    {
                        if (row["fecha"] != DBNull.Value)
                        {
                            DateTime fecha = Convert.ToDateTime(row["fecha"]);
                            row["FechaFormateada"] = fecha.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("es-ES"));
                        }
                        else
                        {
                            row["FechaFormateada"] = string.Empty;
                        }
                    }
                    allGradesData.Columns.Remove("fecha");
                    allGradesData.Columns["FechaFormateada"].ColumnName = "fecha";
                }

                // Guardar los datos en ViewState para usarlos después
                ViewState["AllGradesData"] = allGradesData;
            }
        }

        private void FilterAndDisplayGrades()
        {
            if (allGradesData != null)
            {
                DataView dv = allGradesData.DefaultView;

                // Aplicar filtro por tipo de nota
                string tipoNotaTexto = ddlTipoNota.SelectedItem.Text;
                dv.RowFilter = $"tipoNota = '{tipoNotaTexto}'";

                // Ordenar por fecha descendente (más reciente primero)
                dv.Sort = "fecha DESC";

                // Asignar al GridView
                gvNotas.DataSource = dv;
                gvNotas.DataBind();

                // Mostrar mensaje si no hay datos
                if (dv.Count == 0)
                {
                    // Crear una tabla vacía con mensaje
                    DataTable emptyDt = new DataTable();
                    foreach (DataColumn col in allGradesData.Columns)
                    {
                        emptyDt.Columns.Add(col.ColumnName, col.DataType);
                    }

                    DataRow row = emptyDt.NewRow();
                    row["tipoNota"] = tipoNotaTexto;
                    row["titulo"] = "No hay notas disponibles para este tipo";
                    row["nota"] = 0;
                    row["fecha"] = "-";
                    emptyDt.Rows.Add(row);

                    gvNotas.DataSource = emptyDt;
                    gvNotas.DataBind();
                }
            }
        }

        private void loadGroupData(int idGrupo)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("listar_grupo_seleccion", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("@idGrupo", idGrupo));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblNumero.Text = dr["numero"].ToString();
                    lblIdioma.Text = dr["nombreIdioma"].ToString();
                    lblNivel.Text = dr["nombreNivel"].ToString();
                    lblCiclo.Text = dr["ciclo"].ToString();
                    lblModalidad.Text = dr["modalidad"].ToString();
                    lblDuracion.Text = Convert.ToDateTime(dr["fechaInicio"]).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(dr["fechaFinal"]).ToString("dd/MM/yyyy");
                    lblHorario.Text = dr["horaInicio"].ToString() + " - " + dr["horaFinal"].ToString();
                    lblDocente.Text = dr["DOCENTE"].ToString();
                    lblDias.Text = dr["dias"].ToString();
                }
                else
                {
                    Response.Redirect("index.aspx");
                }

                cmd.Connection.Close();
            }
        }

        private void loadAssistance(int idGrupo, String carnetId)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("listar_asistencia_alumno", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("@nrocarnet", carnetId));
                cmd.Parameters.Add(new SqlParameter("@idgrupo", idGrupo));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Columns.Contains("fecha"))
                {
                    dt.Columns.Add("FechaFormateada", typeof(string)); // Nueva columna para la fecha formateada

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["nombreAsistencia"].ToString() == "F" || row["nombreAsistencia"].ToString() == "J") faltas++;
                        DateTime fecha = Convert.ToDateTime(row["fecha"]);
                        row["FechaFormateada"] = fecha.ToString("dddd, dd MMMM yyyy", new System.Globalization.CultureInfo("es-ES"));
                    }
                    lblFalta.Text = faltas.ToString();
                    dt.Columns.Remove("fecha"); // Eliminamos la columna original
                    dt.Columns["FechaFormateada"].ColumnName = "fecha"; // Renombramos la nueva columna
                }

                gvAsistencias.DataSource = dt;
                gvAsistencias.DataBind();
                cmd.Connection.Close();

                lblEstado.Text = (faltasPermitidas < faltas) ? "Inhabilitado" : "Habilitado";
            }
        }

        private void loadDays(int idGrupo)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("listar_dias_grupo", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("@idgrupo", idGrupo));
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    faltasPermitidas++;
                }
                lblLimiteFaltas.Text = faltasPermitidas.ToString();
                cmd.Connection.Close();
            }
        }

        private void loadPonderate(int idGrupo, String userId)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("sp_promedios_alumno_grupo", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@idGrupo", idGrupo));
                cmd.Parameters.Add(new SqlParameter("@idalumno", userId));

                sqlConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvPromedios.DataSource = dt;
                gvPromedios.DataBind();
            }
        }
    }
}