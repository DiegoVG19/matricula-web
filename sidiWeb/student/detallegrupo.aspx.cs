using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace sidiWeb
{
    public partial class detallegrupo : System.Web.UI.Page
    {
        String dni, userId, carnetId, idGrupo;
        int faltasPermitidas, faltas, estado;
        DataTable allGradesData;

        protected void Page_Load(object sender, EventArgs e)
        {
                // Verificar si las variables de sesión existen
                if (Session["UserId"] == null || Session["CarnetId"] == null || Session["idGrupo"] == null)
                {
                    Session.Clear();
                    Response.Redirect("Login.aspx");
                    return;
                }

                // Inicializar variables después de verificar que existen
                userId = Session["UserId"].ToString();
                carnetId = Session["CarnetId"].ToString();
                idGrupo = Session["idGrupo"].ToString();

                if (!IsPostBack)
                {
                    // Verificar que idGrupo no sea nulo o vacío
                    if (!string.IsNullOrEmpty(idGrupo))
                    {
                        loadGroupData(idGrupo);
                    }
                    else
                    {
                        Session["idGrupo"] = 0;
                        Response.Redirect("Grupos.aspx");
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
                // Verificar que ddlTipoNota y lblIdioma no sean nulos
                if (ddlTipoNota == null || lblIdioma == null)
                {
                    return;
                }

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
                        ddlTipoNota.Items.Add(new ListItem("RUWANAPAJ", "RUWANAPAJ")); // Corregido el valor duplicado
                        break;
                }
        }

        protected void btnMostrarAsistencias_Click(object sender, EventArgs e)
        {
                // Verificar que idGrupo y carnetId no sean nulos
                if (string.IsNullOrEmpty(idGrupo) || string.IsNullOrEmpty(carnetId))
                {
                    return;
                }

                loadDays(idGrupo);
                loadAssistance(idGrupo, carnetId);

                if (pnlAsistencias != null)
                {
                    pnlAsistencias.Visible = true;
                }
        }

        protected void btnMostrarNotas_Click(object sender, EventArgs e)
        {
                // Verificar que ddlTipoNota no sea nulo
                if (ddlTipoNota == null)
                {
                    return;
                }

                ddlTipoNota.Items.Clear();
                ddlTipoNota.Items.Add(new ListItem("Promedios hasta ahora", "P"));

                // Cargar las habilidades según el idioma
                loadHabilities();

                // Verificar que idGrupo y userId no sean nulos
                if (string.IsNullOrEmpty(idGrupo) || string.IsNullOrEmpty(userId))
                {
                    return;
                }

                LoadAllGrades(idGrupo, userId);
                loadPonderate(idGrupo, userId);

                // Configurar visibilidad inicial
                if (ddlTipoNota.Items.Count > 0)
                {
                    ddlTipoNota.SelectedValue = "P"; // Establecer a Promedios por defecto
                }

                if (gvPromedios != null && gvNotas != null)
                {
                    gvPromedios.Visible = true;
                    gvNotas.Visible = false;
                }

                if (pnlNotas != null)
                {
                    pnlNotas.Visible = true;
                }
        }

        protected void ddlTipoNota_SelectedIndexChanged(object sender, EventArgs e)
        {
                // Verificar que ddlTipoNota, gvPromedios y gvNotas no sean nulos
                if (ddlTipoNota == null || gvPromedios == null || gvNotas == null)
                {
                    return;
                }

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

        private void LoadAllGrades(String idGrupo, String userId)
        {
                // Verificar parámetros
                if (string.IsNullOrEmpty(idGrupo) || string.IsNullOrEmpty(userId))
                {
                    return;
                }

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
                // Verificar que allGradesData, ddlTipoNota y gvNotas no sean nulos
                if (allGradesData == null || ddlTipoNota == null || gvNotas == null || ddlTipoNota.SelectedItem == null)
                {
                    return;
                }

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

        private void loadGroupData(String idGrupo)
        {
                // Verificar parámetro
                if (string.IsNullOrEmpty(idGrupo))
                {
                    return;
                }

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
                        // Verificar que los controles no sean nulos antes de asignarles valores
                        if (lblNumero != null) lblNumero.Text = dr["numero"].ToString();
                        if (lblIdioma != null) lblIdioma.Text = dr["nombreIdioma"].ToString();
                        if (lblNivel != null) lblNivel.Text = dr["nombreNivel"].ToString();
                        if (lblCiclo != null) lblCiclo.Text = dr["ciclo"].ToString();
                        if (lblModalidad != null) lblModalidad.Text = dr["modalidad"].ToString();

                        if (lblDuracion != null && dr["fechaInicio"] != DBNull.Value && dr["fechaFinal"] != DBNull.Value)
                        {
                            lblDuracion.Text = Convert.ToDateTime(dr["fechaInicio"]).ToString("dd/MM/yyyy") + " - " +
                                              Convert.ToDateTime(dr["fechaFinal"]).ToString("dd/MM/yyyy");
                        }

                        if (lblHorario != null)
                            lblHorario.Text = dr["horaInicio"].ToString() + " - " + dr["horaFinal"].ToString();

                        if (lblDocente != null) lblDocente.Text = dr["DOCENTE"].ToString();
                        if (lblDias != null) lblDias.Text = dr["dias"].ToString();
                    }
                    else
                    {
                        Response.Redirect("index.aspx");
                    }

                    dr.Close();
                }
        }

        private void loadAssistance(String idGrupo, String carnetId)
        {
                // Verificar parámetros
                if (string.IsNullOrEmpty(idGrupo) || string.IsNullOrEmpty(carnetId) || gvAsistencias == null)
                {
                    return;
                }

                faltas = 0; // Reiniciar contador de faltas

                string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

                using (SqlConnection sqlConnection = new SqlConnection(connect))
                {
                    SqlCommand cmd = new SqlCommand("listar_asistencia_alumno", sqlConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@nrocarnet", carnetId));
                    cmd.Parameters.Add(new SqlParameter("@idgrupo", idGrupo));

                    sqlConnection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Columns.Contains("fecha"))
                    {
                        dt.Columns.Add("FechaFormateada", typeof(string));

                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["nombreAsistencia"] != DBNull.Value &&
                                (row["nombreAsistencia"].ToString() == "F"))
                            {
                                faltas++;
                            }

                            if (row["fecha"] != DBNull.Value)
                            {
                                DateTime fecha = Convert.ToDateTime(row["fecha"]);
                                row["FechaFormateada"] = fecha.ToString("dddd, dd MMMM yyyy", new System.Globalization.CultureInfo("es-ES"));
                            }
                            else
                            {
                                row["FechaFormateada"] = string.Empty;
                            }
                        }

                        if (lblFalta != null) lblFalta.Text = faltas.ToString();

                        dt.Columns.Remove("fecha");
                        dt.Columns["FechaFormateada"].ColumnName = "fecha";
                    }

                    gvAsistencias.DataSource = dt;
                    gvAsistencias.DataBind();

                    if (lblEstado != null)
                    {
                        lblEstado.Text = (faltasPermitidas < faltas) ? "Inhabilitado" : "Habilitado";
                        lblEstado.ForeColor = (lblEstado.Text.Equals("Habilitado")) ? Color.Green : Color.Red;
                }
                }
        }

        private void loadDays(String idGrupo)
        {
                // Verificar parámetro
                if (string.IsNullOrEmpty(idGrupo))
                {
                    return;
                }

                faltasPermitidas = 0; // Reiniciar contador

                string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

                using (SqlConnection sqlConnection = new SqlConnection(connect))
                {
                    SqlCommand cmd = new SqlCommand("listar_dias_grupo", sqlConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@idgrupo", idGrupo));

                    sqlConnection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        faltasPermitidas++;
                    }

                    dr.Close();

                    if (lblLimiteFaltas != null)
                    {
                        lblLimiteFaltas.Text = faltasPermitidas.ToString();
                    }
                }
        }

        private void loadPonderate(String idGrupo, String userId)
        {
            // Verificar parámetros
            if (string.IsNullOrEmpty(idGrupo) || string.IsNullOrEmpty(userId) || gvPromedios == null)
            {
                return;
            }

            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("sp_promedios_alumno_grupo", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                // Convertir a entero si es necesario
                int idGrupoInt, userIdInt;
                if (int.TryParse(idGrupo, out idGrupoInt) && int.TryParse(userId, out userIdInt))
                {
                    cmd.Parameters.Add(new SqlParameter("@idGrupo", idGrupoInt));
                    cmd.Parameters.Add(new SqlParameter("@idalumno", userIdInt));
                }
                else
                {
                    // Si no se puede convertir, usar los valores originales
                    cmd.Parameters.Add(new SqlParameter("@idGrupo", idGrupo));
                    cmd.Parameters.Add(new SqlParameter("@idalumno", userId));
                }

                sqlConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvPromedios.DataSource = dt;
                gvPromedios.DataBind();
            }
        }

        // Agregar este método para manejar el evento RowDataBound
        protected void gvPromedios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtener el DataTable del DataSource
                GridView gv = (GridView)sender;
                DataTable dt = (DataTable)gv.DataSource;

                // Verificar si es la última fila de datos
                if (e.Row.RowIndex == dt.Rows.Count - 1)
                {
                    // Buscar la celda que contiene el promedio
                    // Asumiendo que el promedio está en la última columna, ajusta el índice según tu estructura
                    int columnIndex = e.Row.Cells.Count - 1; // Última columna

                    // También puedes buscar por nombre de columna si conoces el nombre:
                    // int columnIndex = GetColumnIndexByName(gv, "NombreColumnaPromedio");

                    string promedioText = e.Row.Cells[columnIndex].Text;

                    // Intentar convertir a decimal
                    if (decimal.TryParse(promedioText, out decimal promedio))
                    {
                        if (promedio >= 14)
                        {
                            e.Row.Cells[columnIndex].ForeColor = System.Drawing.Color.Green;
                            e.Row.Cells[columnIndex].Font.Bold = true; // Opcional: hacer el texto en negrita
                        }
                        else
                        {
                            e.Row.Cells[columnIndex].ForeColor = System.Drawing.Color.Red;
                            e.Row.Cells[columnIndex].Font.Bold = true; // Opcional: hacer el texto en negrita
                        }
                    }
                }
            }
        }

        // Método auxiliar para obtener el índice de columna por nombre (opcional)
        private int GetColumnIndexByName(GridView gv, string columnName)
        {
            for (int i = 0; i < gv.Columns.Count; i++)
            {
                if (gv.Columns[i].HeaderText.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1; // No encontrado
        }

    }
}