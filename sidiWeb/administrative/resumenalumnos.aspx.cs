using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sidiWeb.administrative
{
    public partial class resumenalumnos : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string termino = txtBuscar.Text;

            if (string.IsNullOrEmpty(termino))
            {
                MostrarMensaje("Por favor, ingresa un término de búsqueda", "warning");
                return;
            }

            try
            {
                List<Alumno> resultados = BuscarAlumnos(termino);
                MostrarResultados(resultados, termino);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al realizar la búsqueda: " + ex.Message, "error");
                System.Diagnostics.Debug.WriteLine($"Error en búsqueda: {ex.Message}");
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            LimpiarResultados();
        }

        private List<Alumno> BuscarAlumnos(string termino)
        {
            List<Alumno> alumnos = new List<Alumno>();
            alumnos = BuscarEnBaseDatos(termino);

            return alumnos;
        }

        private List<Alumno> BuscarEnBaseDatos(string termino)
        {
            List<Alumno> alumnos = new List<Alumno>();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["dbSidi"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("buscar_alumno_consultar", conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@txt", termino);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                alumnos.Add(new Alumno
                                {
                                    Dni = reader["DNI"].ToString(),
                                    Nombre = reader["Alumno"].ToString(),
                                    NCarnet = reader["N° carnet"].ToString(),
                                    TipoAlumno = reader["Tipo de Alumno"].ToString(),
                                });
                            }
                        }
                    }
                }
            
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en BuscarEnBaseDatos: {ex.Message}");
                throw;
            }

            return alumnos;
        }

        private void MostrarResultados(List<Alumno> resultados, string termino)
        {
            // Ocultar estado inicial
            pnlEstadoInicial.Visible = false;

            if (resultados.Count == 0)
            {
                // Mostrar panel sin resultados
                pnlSinResultados.Visible = true;
                rptResultados.Visible = false;
                MostrarMensaje($"No se encontraron resultados para \"{termino}\"", "warning");
            }
            else
            {
                // Mostrar resultados
                pnlSinResultados.Visible = false;
                rptResultados.Visible = true;
                rptResultados.DataSource = resultados;
                rptResultados.DataBind();
                MostrarMensaje($"Se encontraron {resultados.Count} resultado(s) para \"{termino}\"", "info");
            }
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            pnlSearchInfo.Visible = true;
            lblSearchInfo.Text = mensaje;

            switch (tipo)
            {
                case "warning":
                    pnlSearchInfo.CssClass = "search-info warning";
                    break;
                case "error":
                    pnlSearchInfo.CssClass = "search-info warning";
                    break;
                default:
                    pnlSearchInfo.CssClass = "search-info";
                    break;
            }
        }

        private void LimpiarResultados()
        {
            pnlEstadoInicial.Visible = true;
            pnlSinResultados.Visible = false;
            pnlSearchInfo.Visible = false;
            rptResultados.Visible = false;
            rptResultados.DataSource = null;
            rptResultados.DataBind();
        }
    }

    // Clase modelo para Alumno
    public class Alumno
    {
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string NCarnet { get; set; }
        public string TipoAlumno { get; set; }
    }

}