using sidiWeb.Code.Api;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace sidiWeb.student


{
    public partial class matricula : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdAlumno"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            CargarGruposParaMatricula();
        }

        private void CargarGruposParaMatricula()
        {
            int idAlumno = ObtenerIdAlumnoDesdeSesion();
            string carnetId = ObtenerCarnetAlumno(idAlumno);

            if (string.IsNullOrEmpty(carnetId))
                return;

            phIdiomas.Controls.Clear();

            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            phIdiomas.Controls.Add(new Literal
            {
                Text = "<div class='contenedor-idiomas'>"
            });

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("obtener_idiomas_alumno", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idAlumno", SqlDbType.Int).Value = idAlumno;

                SqlDataReader dr = cmd.ExecuteReader();

                DataTable dtIdiomas = new DataTable();
                dtIdiomas.Load(dr);
                dr.Close();

                foreach (DataRow row in dtIdiomas.Rows)
                {
                    int idIdioma = Convert.ToInt32(row["idIdioma"]);
                    string nombreIdioma = row["nombreIdioma"].ToString();

                    string claseIdioma = ObtenerClaseIdioma(nombreIdioma);
                    GrupoInfo ultimo = ObtenerUltimoGrupoAlumno(idAlumno, idIdioma);

                    if (ultimo.IdGrupo == 0)
                        continue;

                    string nombreNivel = ObtenerNombreNivel(ultimo.IdNivel);
                    string iconoBandera = ObtenerIconoBanderaHtml(nombreIdioma);

                    bool habilitado = EstaHabilitado(ultimo.IdGrupo.ToString(), carnetId);

                    int cicloACargar = habilitado ? ultimo.Ciclo + 1 : ultimo.Ciclo;

                    if (!ExisteCicloEnNivel(idIdioma, ultimo.IdNivel, cicloACargar))
                        cicloACargar = ultimo.Ciclo;

                    string mensajeCurso = "";

                    if (TieneMatriculaActiva(idAlumno, idIdioma))
                    {
                        mensajeCurso = @"
                <div class='alert alert-info'>
                    Actualmente estás en curso. Puedes matricularte al siguiente ciclo.
                </div>";
                    }

                    phIdiomas.Controls.Add(new Literal
                    {
                        Text = $@"
<div class='idioma-card {claseIdioma}'>
    <div class='idioma-header'>
        {iconoBandera}
        <div class='idioma-header-texto'>
            <span class='idioma-header-titulo'>{nombreIdioma.ToUpper()}</span>
            <span class='idioma-header-subtitulo'>{nombreNivel.ToUpper()}</span>
        </div>
    </div>
    <div class='idioma-body'>
        {mensajeCurso}
"
                    });



                    CargarTarjetasGrupos(idAlumno, idIdioma, ultimo.IdNivel, cicloACargar);

                    phIdiomas.Controls.Add(new Literal
                    {
                        Text = "</div></div>"
                    });
                }
            }

            phIdiomas.Controls.Add(new Literal
            {
                Text = "</div>"
            });
        }

        private void CargarTarjetasGrupos(int idAlumno, int idIdioma, int idNivel, int ciclo)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("obtener_grupos_disponibles_matricula", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idIdioma", idIdioma);
                cmd.Parameters.AddWithValue("@idNivel", idNivel);
                cmd.Parameters.AddWithValue("@ciclo", ciclo);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                StringBuilder disponibles = new StringBuilder();
                StringBuilder enProceso = new StringBuilder();
                StringBuilder opciones = new StringBuilder();

                DateTime hoy = DateTime.Now;
                bool hayGruposVigentes = false;

                while (dr.Read())
                {
                    int idGrupo = Convert.ToInt32(dr["idGrupo"]);
                    string codigoGrupo = dr["numero"]?.ToString() ?? "Sin código";
                    int cicloGrupo = Convert.ToInt32(dr["ciclo"]);
                    string dias = dr["diasClase"]?.ToString() ?? "Sin días";

                    DateTime horaIni = Convert.ToDateTime(dr["horaInicio"]);
                    DateTime horaFin = Convert.ToDateTime(dr["horaFinal"]);
                    DateTime fechaInicio = Convert.ToDateTime(dr["fechaInicio"]);
                    DateTime fechaFinal = Convert.ToDateTime(dr["fechaFinal"]);

                    bool vigente = hoy.Date <= fechaFinal.Date;
                    bool yaInicio = hoy.Date >= fechaInicio.Date;

                    if (!vigente)
                        continue;

                    hayGruposVigentes = true;

                    string horario = $"{dias} {horaIni:hh:mm tt} - {horaFin:hh:mm tt}";
                    string docente = dr["docente"]?.ToString() ?? "Por asignar";
                    string modalidad = dr["modalidad"]?.ToString() ?? "";

                    string iconoModalidad = "fas fa-desktop";

                    if (modalidad.ToUpper().Contains("PRESENCIAL"))
                        iconoModalidad = "fas fa-school";
                    else if (modalidad.ToUpper().Contains("VIRTUAL"))
                        iconoModalidad = "fas fa-desktop";
                    else if (modalidad.ToUpper().Contains("CASA") || modalidad.ToUpper().Contains("REMOTO"))
                        iconoModalidad = "fas fa-home";

                    string tarjeta = $@"
<div class='horario-card'>
    <div class='grupo-header'>
        {codigoGrupo} - Ciclo {cicloGrupo}
    </div>

    <div class='linea-info'>
        <i class='fas fa-clock'></i>
        <span><strong>Horario:</strong> {horario}</span>
    </div>

    <div class='linea-info'>
        <i class='fas fa-user'></i>
        <span><strong>Docente:</strong> {docente}</span>
    </div>

    <div class='linea-info'>
        <i class='{iconoModalidad}'></i>
        <span><strong>Modalidad:</strong> {modalidad}</span>
    </div>
</div>";

                    if (yaInicio)
                        enProceso.Append(tarjeta);
                    else
                        disponibles.Append(tarjeta);

                    if (!YaEstaEnEsteCiclo(idAlumno, idIdioma, cicloGrupo))
                    {
                        opciones.Append($@"<option value='{idGrupo}'>
{codigoGrupo} | Ciclo {cicloGrupo} | {horario} | {modalidad}
</option>");
                    }
                }

                dr.Close();

                if (disponibles.Length > 0)
                {
                    phIdiomas.Controls.Add(new Literal
                    {
                        Text = "<h5 class='titulo-grupo'>Disponible</h5>"
                    });

                    phIdiomas.Controls.Add(new Literal
                    {
                        Text = disponibles.ToString()
                    });
                }

                if (enProceso.Length > 0)
                {
                    phIdiomas.Controls.Add(new Literal
                    {
                        Text = "<h5 class='titulo-grupo en-proceso'>En proceso</h5>"
                    });

                    phIdiomas.Controls.Add(new Literal
                    {
                        Text = enProceso.ToString()
                    });
                }

                bool yaMatriculado = YaEstaEnEsteCiclo(idAlumno, idIdioma, ciclo);

                if (opciones.Length > 0)
                {
                    phIdiomas.Controls.Add(new Literal
                    {
                        Text = $@"
<div class='matricula-footer'>
    <button type='button' class='btn-matricular'
        onclick=""abrirFormulario(this)"">
        Matricularme
    </button>

    <select class='combo-horarios' style='display:none'>
        {opciones}
    </select>
</div>"
                    });
                }
                else
                {
                    if (yaMatriculado)
                    {
                        phIdiomas.Controls.Add(new Literal
                        {
                            Text = @"<div class='alert alert-warning'>
                    Ya estás matriculado en este ciclo.
                </div>"
                        });
                    }
                    else if (!hayGruposVigentes)
                    {
                        phIdiomas.Controls.Add(new Literal
                        {
                            Text = @"<div class='alert alert-info'>
                    No hay grupos vigentes para este ciclo actualmente.
                </div>"
                        });
                    }
                    else
                    {
                        phIdiomas.Controls.Add(new Literal
                        {
                            Text = @"<div class='alert alert-info'>
                    No hay horarios disponibles para matrícula en este momento.
                </div>"
                        });
                    }
                }
            }
        }

        private bool ExisteCicloEnNivel(int idIdioma, int idNivel, int ciclo)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connect))
            {
                string query = @"
                    SELECT COUNT(*)
                    FROM [Grupo]
                    WHERE idIdioma = @idIdioma
                    AND idNivel = @idNivel
                    AND ciclo = @ciclo";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.Add("@idIdioma", SqlDbType.Int).Value = idIdioma;
                cmd.Parameters.Add("@idNivel", SqlDbType.Int).Value = idNivel;
                cmd.Parameters.Add("@ciclo", SqlDbType.Int).Value = ciclo;

                con.Open();

                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }

        private bool TieneMatriculaActiva(int idAlumno, int idIdioma)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connect))
            {
                string query = @"
        SELECT COUNT(*)
        FROM AlumnoGrupo ag
        INNER JOIN Grupo g ON ag.idGrupo = g.idGrupo
        WHERE ag.idAlumno = @idAlumno
        AND g.idIdioma = @idIdioma
        AND CAST(GETDATE() AS DATE) BETWEEN CAST(g.fechaInicio AS DATE) AND CAST(g.fechaFinal AS DATE)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@idAlumno", SqlDbType.Int).Value = idAlumno;
                cmd.Parameters.Add("@idIdioma", SqlDbType.Int).Value = idIdioma;

                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                return count > 0;
            }
        }

        private string ObtenerCarnetAlumno(int idAlumno)
        {
            string carnet = "";

            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connect))
            {
                string query = @"SELECT numeroCarnet FROM Alumno WHERE idAlumno = @idAlumno";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@idAlumno", SqlDbType.Int).Value = idAlumno;

                con.Open();

                object result = cmd.ExecuteScalar();

                if (result != null)
                    carnet = result.ToString();
            }

            return carnet;
        }

        private string ObtenerClaseIdioma(string idioma)
        {
            idioma = idioma.Trim().ToLower();

            if (idioma.Contains("ing"))
                return "idioma-ingles";

            if (idioma.Contains("port"))
                return "idioma-portugues";

            if (idioma.Contains("ita"))
                return "idioma-italiano";

            if (idioma.Contains("fra"))
                return "idioma-frances";

            if (idioma.Contains("chin"))
                return "idioma-chino";

            if (idioma.Contains("que"))
                return "idioma-quechua";

            return "idioma-default";
        }

        private string ObtenerNombreNivel(int idNivel)
        {
            string nombreNivel = "";

            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connect))
            {
                string query = @"SELECT nombreNivel FROM Nivel WHERE idNivel = @idNivel";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@idNivel", SqlDbType.Int).Value = idNivel;

                con.Open();

                object result = cmd.ExecuteScalar();
                if (result != null)
                    nombreNivel = result.ToString();
            }

            return string.IsNullOrWhiteSpace(nombreNivel) ? "SIN NIVEL" : nombreNivel;
        }

        private bool EstaHabilitado(string idGrupo, string carnetId)
        {
            if (string.IsNullOrEmpty(idGrupo) || string.IsNullOrEmpty(carnetId))
                return false;

            int faltas = 0;
            int faltasPermitidas = 3;

            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("listar_asistencia_alumno", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@nrocarnet", SqlDbType.VarChar).Value = carnetId;
                cmd.Parameters.Add("@idgrupo", SqlDbType.Int).Value = Convert.ToInt32(idGrupo);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (dr["nombreAsistencia"].ToString() == "F")
                        faltas++;
                }

                dr.Close();
            }

            return faltas <= faltasPermitidas;
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensajeGeneral.Visible = false;

                if (Session["IdAlumno"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                    return;
                }

                if (string.IsNullOrWhiteSpace(hfGrupoSeleccionado.Value))
                {
                    MostrarMensaje("Debe seleccionar un horario.", false);
                    return;
                }

                if (!int.TryParse(hfGrupoSeleccionado.Value, out int idGrupo))
                {
                    MostrarMensaje("Grupo inválido.", false);
                    return;
                }

                int idAlumno = ObtenerIdAlumnoDesdeSesion();

                string tipoAlumno = Request.Form["tipoAlumno"];
                string esReincorporacion = Request.Form["esReincorporacion"] ?? "no";
                bool reincorporacion = esReincorporacion == "si";

                if (string.IsNullOrWhiteSpace(tipoAlumno))
                {
                    MostrarMensaje("Debe seleccionar el tipo de alumno.", false);
                    return;
                }

                if (tipoAlumno == "faustiniano")
                {
                    MostrarMensaje("Debe subir la ficha de matrícula.", false);
                    return;
                }

                if (tipoAlumno == "faustiniano")
                {
                    MostrarMensaje("La ficha de matrícula debe ser un archivo PDF.", false);
                    return;
                }

                int idSolicitudGenerada = 0;

                using (SqlConnection con = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_RegistrarSolicitudMatricula", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@idAlumno", SqlDbType.Int).Value = idAlumno;
                        cmd.Parameters.Add("@idGrupo", SqlDbType.Int).Value = idGrupo;
                        cmd.Parameters.Add("@tipoAlumno", SqlDbType.VarChar, 20).Value = tipoAlumno;
                        cmd.Parameters.Add("@esReincorporacion", SqlDbType.Bit).Value = reincorporacion;

                        // Mantener si el SP los exige
                        cmd.Parameters.Add("@rutaVoucher", SqlDbType.VarChar, 300).Value = DBNull.Value;
                        cmd.Parameters.Add("@rutaFicha", SqlDbType.VarChar, 300).Value = DBNull.Value;
                        cmd.Parameters.Add("@rutaVoucherReincorporacion", SqlDbType.VarChar, 300).Value = DBNull.Value;

                        con.Open();

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int.TryParse(result.ToString(), out idSolicitudGenerada);
                        }
                    }
                }

                MostrarPanelResultado(
                    idSolicitudGenerada > 0
                        ? "Solicitud enviada correctamente. Será revisada por el área encargada."
                        : "La solicitud fue enviada, pero no se pudo confirmar el identificador generado.",
                    true
                );
            }
            catch (Exception ex)
            {
                MostrarPanelResultado("Error al enviar la solicitud: " + ex.Message, false);
            }
        }

        private void MostrarMensaje(string mensaje, bool exito)
        {
            lblMensajeGeneral.Text = mensaje;
            lblMensajeGeneral.CssClass = exito
                ? "alert alert-success mensaje-general"
                : "alert alert-danger mensaje-general";

            lblMensajeGeneral.Visible = true;
        }

        private void MostrarPanelResultado(string mensaje, bool exito)
        {
            hfResultadoTipo.Value = exito ? "exito" : "error";
            hfResultadoMensaje.Value = mensaje;
            pnlResultadoSolicitud.Style["display"] = "block";
        }

        private bool YaEstaEnEsteCiclo(int idAlumno, int idIdioma, int ciclo)
        {
            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connect))
            {
                string query = @"
        SELECT COUNT(*)
        FROM AlumnoGrupo ag
        INNER JOIN Grupo g ON ag.idGrupo = g.idGrupo
        WHERE ag.idAlumno = @idAlumno
        AND g.idIdioma = @idIdioma
        AND g.ciclo = @ciclo
        AND CAST(GETDATE() AS DATE) BETWEEN CAST(g.fechaInicio AS DATE) AND CAST(g.fechaFinal AS DATE)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@idAlumno", SqlDbType.Int).Value = idAlumno;
                cmd.Parameters.Add("@idIdioma", SqlDbType.Int).Value = idIdioma;
                cmd.Parameters.Add("@ciclo", SqlDbType.Int).Value = ciclo;

                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                return count > 0;
            }
        }

        private GrupoInfo ObtenerUltimoGrupoAlumno(int idAlumno, int idIdioma)
        {
            GrupoInfo info = new GrupoInfo();

            string connect = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand("obtener_ultimo_grupo_por_idioma", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@idAlumno", SqlDbType.Int).Value = idAlumno;
                cmd.Parameters.Add("@idIdioma", SqlDbType.Int).Value = idIdioma;

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    info.IdGrupo = Convert.ToInt32(dr["idGrupo"]);
                    info.Ciclo = Convert.ToInt32(dr["ciclo"]);
                    info.IdNivel = Convert.ToInt32(dr["idNivel"]);
                    info.Modalidad = dr["modalidad"].ToString();
                    info.FechaFinal = Convert.ToDateTime(dr["fechaFinal"]);
                }

                dr.Close();
            }

            return info;
        }

        private int ObtenerIdAlumnoDesdeSesion()
        {
            if (Session["IdAlumno"] == null)
                throw new Exception("La sesión del alumno ha expirado.");

            if (!int.TryParse(Session["IdAlumno"].ToString(), out int idAlumno))
                throw new Exception("El identificador del alumno en sesión no es válido.");

            return idAlumno;
        }

        private string ObtenerIconoBanderaHtml(string idioma)
        {
            idioma = idioma.Trim().ToLower();

            if (idioma.Contains("ing"))
                return "<span class='fi fi-us flag-icono' aria-hidden='true'></span>";

            if (idioma.Contains("ita"))
                return "<span class='fi fi-it flag-icono' aria-hidden='true'></span>";

            if (idioma.Contains("port"))
                return "<span class='fi fi-br flag-icono' aria-hidden='true'></span>";

            if (idioma.Contains("fra"))
                return "<span class='fi fi-fr flag-icono' aria-hidden='true'></span>";

            if (idioma.Contains("chin"))
                return "<span class='fi fi-cn flag-icono' aria-hidden='true'></span>";

            if (idioma.Contains("que"))
                return "<i class='fas fa-mountain flag-icono' aria-hidden='true'></i>";

            return "<i class='fas fa-globe-americas flag-icono' aria-hidden='true'></i>";
        }

        [WebMethod]
        public static RespuestaPreciosDto ObtenerPreciosPorGrupo(int idGrupo)
        {
            System.Diagnostics.Debug.WriteLine(
                "ENTRO ObtenerPreciosPorGrupo"
            );

            System.Diagnostics.Debug.WriteLine(
                "ID GRUPO: " + idGrupo
            );


            RespuestaPreciosDto respuesta = new RespuestaPreciosDto
            {
                faustiniano = 0,
                particular = 0
            };


            string connect =
                ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;


            using (SqlConnection con = new SqlConnection(connect))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "sp_ObtenerPreciosMatriculaPorGrupo",
                    con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@idGrupo", SqlDbType.Int)
                       .Value = idGrupo;


                    con.Open();


                    System.Diagnostics.Debug.WriteLine(
                        "BD ABIERTA"
                    );


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        bool encontroDatos = false;


                        while (dr.Read())
                        {
                            encontroDatos = true;


                            string tipoAlumno =
                                dr["tipoAlumno"]
                                .ToString()
                                .Trim()
                                .ToLower();


                            decimal monto =
                                Convert.ToDecimal(dr["monto"]);

                            string tributo = dr["tributo"].ToString();

                            System.Diagnostics.Debug.WriteLine(
                                $"TIPO: {tipoAlumno} MONTO: {monto}"
                            );


                            if (tipoAlumno == "faustiniano")
                            {
                                respuesta.faustiniano = monto;
                                respuesta.tributoFaustiniano = tributo;
                            }
                            else if (tipoAlumno == "particular")
                            {
                                respuesta.particular = monto;
                                respuesta.tributoParticular = tributo;
                            }
                        }


                        if (!encontroDatos)
                        {
                            System.Diagnostics.Debug.WriteLine(
                                "SP NO DEVOLVIO REGISTROS"
                            );
                        }
                    }
                }
            }


            System.Diagnostics.Debug.WriteLine(
                $"RESULTADO FINAL F:{respuesta.faustiniano} P:{respuesta.particular}"
            );


            return respuesta;
        }

        private string ObtenerDniDesdeSesion()
        {
            if (Session["Dni"] == null)
                return string.Empty;

            return Session["Dni"].ToString();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object ObtenerEstadoAlumno()
        {
            System.Diagnostics.Debug.WriteLine("ENTRO AL WEBMETHOD");

            try
            {
                string dni = HttpContext.Current.Session["Dni"]?.ToString();

                System.Diagnostics.Debug.WriteLine("DNI SESSION: " + dni);


                if (string.IsNullOrWhiteSpace(dni))
                {
                    return new
                    {
                        exito = false,
                        mensaje = "No existe el DNI en la sesión."
                    };
                }


                ApiService api = new ApiService();

                System.Diagnostics.Debug.WriteLine("Consultando API...");


                AlumnoApiResponse alumno =
                    api.ObtenerEstadoAlumno(dni);


                System.Diagnostics.Debug.WriteLine("API respondió");


                if (alumno == null || alumno.datosAcademicos == null)
                {
                    return new
                    {
                        exito = false,
                        mensaje = "No fue posible obtener datos académicos del alumno."
                    };
                }


                string codigoUniversitario =
                    alumno.datosAcademicos.codigoUniversitario;


                string estadoAcademico =
                    alumno.datosAcademicos.estadoAcademico;


                bool tieneCodigoUniversitario =
                    !string.IsNullOrWhiteSpace(codigoUniversitario);


                bool esEgresado =
                    string.Equals(
                        estadoAcademico,
                        "EGRESADO",
                        StringComparison.OrdinalIgnoreCase
                    );


                string tipoAlumno =
                    tieneCodigoUniversitario && !esEgresado
                        ? "Faustiniano"
                        : "Particular";


                System.Diagnostics.Debug.WriteLine(
                    "Tipo alumno: " + tipoAlumno
                );


                return new
                {
                    exito = true,

                    datos = new
                    {
                        dni = alumno.dni,
                        nombres = alumno.datosAcademicos.nombresCompletos,
                        codigoUniversitario,
                        estadoAcademico,
                        tipoAlumno
                    }
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    "ERROR WEBMETHOD: " + ex.Message
                );

                return new
                {
                    exito = false,
                    mensaje = ex.Message
                };
            }
        }
    }



    public class GrupoInfo
    {
        public int IdGrupo { get; set; }
        public int Ciclo { get; set; }
        public int IdNivel { get; set; }
        public string Modalidad { get; set; }
        public DateTime FechaFinal { get; set; }
    }

    public class RespuestaPreciosDto
    {
        public decimal faustiniano { get; set; }
        public decimal particular { get; set; }

        public string tributoFaustiniano { get; set; }
        public string tributoParticular { get; set; }
    }
}