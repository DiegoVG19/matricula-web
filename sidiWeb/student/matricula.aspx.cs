using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace sidiWeb.student
{
    public partial class matricula : System.Web.UI.Page
    {
        // ─── Conexión ───────────────────────────────────────────
        private string ConnStr =>
            ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

        // ─── Estado guardado en ViewState ───────────────────────
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

        // ─── Page Load ──────────────────────────────────────────
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar sesión
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

        // ─── Cargar nombre del alumno ────────────────────────────
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
                var nombre = cmd.ExecuteScalar()?.ToString() ?? "Alumno";
                lblNombreAlumno.Text = nombre;
            }
        }

        // ─── Verificar estado con SP ─────────────────────────────
        private void VerificarEstado()
        {
            // Primero verificar si ya tiene solicitud pendiente
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
                            CargarDatosVoucher(estado);
                            MostrarPanel(pnlVoucher);
                            break;

                        default:
                            MostrarPanel(pnlError);
                            break;
                    }
                }
            }
        }

        // ─── Cargar info y grupos disponibles ───────────────────
        private void CargarDatosVoucher(string estado)
        {
            // Labels informativos
            lblEstado.Text = estado == "CONTINUO" ? "Alumno Continuo" : "Reincorporado";
            lblCiclo.Text = CicloSiguiente.ToString();

            // Nombre del idioma y nivel
            using (var cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                using (var cmd = new SqlCommand(
                    "SELECT nombreIdioma FROM Idioma WHERE idIdioma=@i", cn))
                {
                    cmd.Parameters.AddWithValue("@i", IdIdioma);
                    lblIdioma.Text = cmd.ExecuteScalar()?.ToString() ?? "-";
                }

                using (var cmd = new SqlCommand(
                    "SELECT nombreNivel FROM Nivel WHERE idNivel=@n", cn))
                {
                    cmd.Parameters.AddWithValue("@n", IdNivel);
                    lblNivel.Text = cmd.ExecuteScalar()?.ToString() ?? "-";
                }
            }

            // Cargar grupos disponibles
            CargarGrupos();
        }

        private void CargarGrupos()
        {
            using (var cn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"
                SELECT 
                    g.idGrupo,
                    g.numero + ' | ' + g.diasClase + 
                    ' | ' + CONVERT(VARCHAR,CONVERT(TIME,g.horaInicio),108) + 
                    ' - ' + CONVERT(VARCHAR,CONVERT(TIME,g.horaFinal),108) +
                    ' | Vacantes: ' + 
                    CAST((30 - COUNT(ag.idAlumnoGrupo)) AS VARCHAR) AS descripcion,
                    (30 - COUNT(ag.idAlumnoGrupo)) AS vacantes
                FROM Grupo g
                LEFT JOIN AlumnoGrupo ag ON g.idGrupo = ag.idGrupo
                WHERE g.idIdioma  = @idIdioma
                  AND g.idNivel   = @idNivel
                  AND g.ciclo     = @ciclo
                  AND g.estado    = 'EN PROCESO'
                GROUP BY g.idGrupo, g.numero, g.diasClase, g.horaInicio, g.horaFinal
                HAVING (30 - COUNT(ag.idAlumnoGrupo)) > 0
                ORDER BY g.diasClase", cn))
            {
                cmd.Parameters.AddWithValue("@idIdioma", IdIdioma);
                cmd.Parameters.AddWithValue("@idNivel", IdNivel);
                cmd.Parameters.AddWithValue("@ciclo", CicloSiguiente);
                cn.Open();

                var dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                if (dt.Rows.Count == 0)
                {
                    lblVacantes.Text = "⚠️ No hay grupos disponibles para tu ciclo. Contáctate con recepción.";
                    btnSubir.Enabled = false;
                    ddlGrupos.Enabled = false;
                }
                else
                {
                    ddlGrupos.DataSource = dt;
                    ddlGrupos.DataBind();
                    lblVacantes.Text = $"Se encontraron {dt.Rows.Count} grupo(s) disponibles.";
                }
            }
        }

        // ─── Click: Enviar solicitud ─────────────────────────────
        protected void btnSubir_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            // Validar archivo
            if (!fuVoucher.HasFile)
            {
                lblError.Text = "⚠️ Por favor selecciona tu comprobante de pago.";
                return;
            }

            // Validar extensión
            string ext = Path.GetExtension(fuVoucher.FileName).ToLower();
            if (ext != ".jpg" && ext != ".jpeg" && ext != ".png" && ext != ".pdf")
            {
                lblError.Text = "⚠️ Solo se permiten archivos JPG, PNG o PDF.";
                return;
            }

            // Validar tamaño (5MB)
            if (fuVoucher.PostedFile.ContentLength > 5 * 1024 * 1024)
            {
                lblError.Text = "⚠️ El archivo no debe superar los 5MB.";
                return;
            }

            // Validar grupo seleccionado
            if (ddlGrupos.SelectedValue == "")
            {
                lblError.Text = "⚠️ Por favor selecciona un grupo.";
                return;
            }

            // Guardar archivo localmente
            string rutaRelativa = GuardarVoucher(ext);
            if (rutaRelativa == null)
            {
                lblError.Text = "⚠️ Error al guardar el archivo. Intenta nuevamente.";
                return;
            }

            // Crear solicitud en BD
            int idGrupo = Convert.ToInt32(ddlGrupos.SelectedValue);
            string numeroRecibo = txtNumeroRecibo.Text.Trim();

            string resultado = CrearSolicitud(idGrupo, rutaRelativa, numeroRecibo);

            if (resultado == "OK")
            {
                MostrarPanel(pnlPendiente);
            }
            else
            {
                lblError.Text = "⚠️ " + resultado;
            }
        }

        // ─── Guardar voucher en servidor ─────────────────────────
        private string GuardarVoucher(string ext)
        {
            try
            {
                // Carpeta: ~/Vouchers/2026/04/
                string anio = DateTime.Now.Year.ToString();
                string mes = DateTime.Now.Month.ToString("D2");
                string carpeta = Server.MapPath($"~/Vouchers/{anio}/{mes}/");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                // Nombre único: idalumno_timestamp.ext
                string nombreArchivo = $"{IdAlumno}_{DateTime.Now:yyyyMMddHHmmss}{ext}";
                string rutaFisica = Path.Combine(carpeta, nombreArchivo);

                fuVoucher.SaveAs(rutaFisica);

                // Ruta relativa para guardar en BD
                return $"Vouchers/{anio}/{mes}/{nombreArchivo}";
            }
            catch
            {
                return null;
            }
        }

        // ─── Llamar SP CrearSolicitud ────────────────────────────
        private string CrearSolicitud(int idGrupo, string rutaVoucher, string numeroRecibo)
        {
            using (var cn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand("VM_SP_CrearSolicitud", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idalumno", IdAlumno);
                cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                cmd.Parameters.AddWithValue("@tipoCaso", TipoCaso);
                cmd.Parameters.AddWithValue("@rutaBoucher", rutaVoucher);
                cmd.Parameters.AddWithValue("@numeroRecibo",
                    string.IsNullOrEmpty(numeroRecibo) ? (object)DBNull.Value : numeroRecibo);
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

        // ─── Verificar solicitud pendiente ───────────────────────
        private bool TieneSolicitudPendiente()
        {
            using (var cn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"
                SELECT COUNT(*) FROM VM_SolicitudMatricula 
                WHERE idalumno = @id AND estadoSolicitud = 'PENDIENTE'", cn))
            {
                cmd.Parameters.AddWithValue("@id", IdAlumno);
                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        // ─── Helper: mostrar solo un panel ──────────────────────
        private void MostrarPanel(System.Web.UI.Control panelVisible)
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