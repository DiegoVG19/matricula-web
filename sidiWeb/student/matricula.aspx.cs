using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace sidiWeb.student
{
    public partial class matricula : System.Web.UI.Page
    {
        private string ConnStr =>
            ConfigurationManager.ConnectionStrings["BDCHISTEMAS"].ConnectionString;

        private int IdAlumno
        {
            get => (int)(ViewState["IdAlumno"] ?? 0);
            set => ViewState["IdAlumno"] = value;
        }

        private int CicloSiguiente
        {
            get => (int)(ViewState["CicloSiguiente"] ?? 0);
            set => ViewState["CicloSiguiente"] = value;
        }

        private int IdNivel
        {
            get => (int)(ViewState["IdNivel"] ?? 0);
            set => ViewState["IdNivel"] = value;
        }

        private int IdIdioma
        {
            get => (int)(ViewState["IdIdioma"] ?? 0);
            set => ViewState["IdIdioma"] = value;
        }

        private string TipoCaso
        {
            get => ViewState["TipoCaso"]?.ToString() ?? "";
            set => ViewState["TipoCaso"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/login.aspx");
                return;
            }

            IdAlumno = Convert.ToInt32(Session["UserId"]);

            if (!IsPostBack)
            {
                CargarNombreAlumno();
                VerificarEstado();
            }
        }

        private void CargarNombreAlumno()
        {
            using (var cn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"
                SELECT p.nombre + ' ' + p.apaterno + ' ' + p.amaterno 
                FROM Alumno a 
                INNER JOIN Persona p ON a.idpersona = p.idpersona
                WHERE a.idalumno = @id", cn))
            {
                cmd.Parameters.AddWithValue("@id", IdAlumno);

                cn.Open();

                object result = cmd.ExecuteScalar();
                lblNombreAlumno.Text = result != null && result != DBNull.Value
                    ? result.ToString()
                    : "Alumno";
            }
        }

        private void VerificarEstado()
        {
            if (TieneSolicitudPendiente())
            {
                MostrarPanel(pnlPendiente);
                return;
            }

            using (var cn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand("VM_SP_VerificarAlumno", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idalumno", IdAlumno);

                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.Read())
                    {
                        MostrarPanel(pnlError);
                        return;
                    }

                    string estado = dr["estado"].ToString();
                    string mensaje = dr["mensaje"].ToString();

                    TipoCaso = estado;

                    if (!dr.IsDBNull(dr.GetOrdinal("cicloSiguiente")))
                        CicloSiguiente = Convert.ToInt32(dr["cicloSiguiente"]);

                    if (!dr.IsDBNull(dr.GetOrdinal("idNivel")))
                        IdNivel = Convert.ToInt32(dr["idNivel"]);

                    if (!dr.IsDBNull(dr.GetOrdinal("idIdioma")))
                        IdIdioma = Convert.ToInt32(dr["idIdioma"]);

                    switch (estado)
                    {
                        case "YA_MATRICULADO":
                            MostrarPanel(pnlYaMatriculado);
                            break;

                        case "REQUIERE_AUTORIZACION":
                            lblMensajeAuth.Text = mensaje;
                            MostrarPanel(pnlRequiereAuth);
                            break;

                        case "CONTINUO":
                        case "REINCORPORADO":
                        case "AUTORIZADO":
                            CargarDatosMatricula(estado);
                            MostrarPanel(pnlVoucher);
                            break;

                        default:
                            MostrarPanel(pnlError);
                            break;
                    }
                }
            }
        }

        private void CargarDatosMatricula(string estado)
        {
            if (estado == "CONTINUO")
                lblEstado.Text = "Alumno Continuo";
            else if (estado == "AUTORIZADO")
                lblEstado.Text = "Alumno Autorizado";
            else
                lblEstado.Text = "Reincorporado";

            lblCiclo.Text = CicloSiguiente.ToString();

            using (var cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                using (var cmd = new SqlCommand(
                    "SELECT nombreIdioma FROM Idioma WHERE idIdioma = @idIdioma", cn))
                {
                    cmd.Parameters.AddWithValue("@idIdioma", IdIdioma);
                    object result = cmd.ExecuteScalar();
                    lblIdioma.Text = result != null && result != DBNull.Value ? result.ToString() : "-";
                }

                using (var cmd = new SqlCommand(
                    "SELECT nombreNivel FROM Nivel WHERE idNivel = @idNivel", cn))
                {
                    cmd.Parameters.AddWithValue("@idNivel", IdNivel);
                    object result = cmd.ExecuteScalar();
                    lblNivel.Text = result != null && result != DBNull.Value ? result.ToString() : "-";
                }
            }

            CargarGrupos();
        }

        private void CargarGrupos()
        {
            using (var cn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"
                SELECT 
                    g.idGrupo,
                    g.numero + ' | ' + g.diasClase + 
                    ' | ' + CONVERT(VARCHAR, CONVERT(TIME, g.horaInicio), 108) + 
                    ' - ' + CONVERT(VARCHAR, CONVERT(TIME, g.horaFinal), 108) +
                    ' | Vacantes: ' + 
                    CAST((30 - COUNT(ag.idAlumnoGrupo)) AS VARCHAR) AS descripcion,
                    (30 - COUNT(ag.idAlumnoGrupo)) AS vacantes
                FROM Grupo g
                LEFT JOIN AlumnoGrupo ag ON g.idGrupo = ag.idGrupo
                WHERE g.idIdioma = @idIdioma
                  AND g.idNivel = @idNivel
                  AND g.ciclo = @ciclo
                  AND g.estado = 'EN PROCESO'
                GROUP BY g.idGrupo, g.numero, g.diasClase, g.horaInicio, g.horaFinal
                HAVING (30 - COUNT(ag.idAlumnoGrupo)) > 0
                ORDER BY g.diasClase", cn))
            {
                cmd.Parameters.AddWithValue("@idIdioma", IdIdioma);
                cmd.Parameters.AddWithValue("@idNivel", IdNivel);
                cmd.Parameters.AddWithValue("@ciclo", CicloSiguiente);

                cn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                ddlGrupos.Items.Clear();

                if (dt.Rows.Count == 0)
                {
                    lblVacantes.Text = "No hay grupos disponibles para tu ciclo. Contáctate con recepción.";
                    btnSubir.Enabled = false;
                    ddlGrupos.Enabled = false;
                    return;
                }

                ddlGrupos.DataSource = dt;
                ddlGrupos.DataValueField = "idGrupo";
                ddlGrupos.DataTextField = "descripcion";
                ddlGrupos.DataBind();

                lblVacantes.Text = $"Se encontraron {dt.Rows.Count} grupo(s) disponibles.";
                btnSubir.Enabled = true;
                ddlGrupos.Enabled = true;
            }
        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            if (ddlGrupos.SelectedValue == "")
            {
                lblError.Text = "Por favor selecciona un grupo.";
                return;
            }

            int idGrupo = Convert.ToInt32(ddlGrupos.SelectedValue);
            string numeroRecibo = txtNumeroRecibo.Text.Trim();

            string resultado = CrearSolicitud(idGrupo, numeroRecibo);

            if (resultado == "OK")
            {
                MostrarPanel(pnlPendiente);
            }
            else
            {
                lblError.Text = resultado;
            }
        }


        private string CrearSolicitud(int idGrupo, string numeroRecibo)
        {
            using (var cn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand("VM_SP_CrearSolicitud", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idalumno", IdAlumno);
                cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                cmd.Parameters.AddWithValue("@tipoCaso", TipoCaso);
                cmd.Parameters.AddWithValue("@rutaBoucher", DBNull.Value);
                cmd.Parameters.AddWithValue("@numeroRecibo",
                    string.IsNullOrWhiteSpace(numeroRecibo) ? (object)DBNull.Value : numeroRecibo);

                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        bool ok = Convert.ToInt32(dr["ok"]) == 1;
                        return ok ? "OK" : dr["mensaje"].ToString();
                    }
                }
            }

            return "Error inesperado.";
        }

        private bool TieneSolicitudPendiente()
        {
            using (var cn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"
                SELECT COUNT(*) 
                FROM VM_SolicitudMatricula 
                WHERE idalumno = @id 
                  AND estadoSolicitud = 'PENDIENTE'", cn))
            {
                cmd.Parameters.AddWithValue("@id", IdAlumno);

                cn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private void MostrarPanel(Control panelVisible)
        {
            pnlVoucher.Visible = false;
            pnlPendiente.Visible = false;
            pnlYaMatriculado.Visible = false;
            pnlRequiereAuth.Visible = false;
            pnlError.Visible = false;

            panelVisible.Visible = true;
        }
    }
}