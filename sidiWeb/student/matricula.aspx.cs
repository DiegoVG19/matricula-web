using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace sidiWeb.student
{
    public partial class matricula : System.Web.UI.Page
    {
       
        protected void btnIniciarMatricula_Click(object sender, EventArgs e)
        {
            // Redirige al estudiante al formulario real de matrícula
            Response.Redirect("formMatricula.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTablaMatricula();

                // LÓGICA FUTURA: 
                // bool cicloTerminado = VerificarBaseDeDatos();
                // if (cicloTerminado) 
                // {
                //     string script = "var miModal = new bootstrap.Modal(document.getElementById('modalAvisoMatricula')); miModal.show();";
                //     ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModalMatricula", script, true);
                // }
            }
        }

        private void CargarTablaMatricula()
        {
            var datosMatricula = ObtenerDatosMatricula();
            StringBuilder htmlFilas = new StringBuilder();

            foreach (var item in datosMatricula)
            {
                htmlFilas.Append(@"<tr>");
                htmlFilas.AppendFormat(@"<td>{0}</td>", item.Idioma);
                htmlFilas.AppendFormat(@"<td>{0}</td>", item.Ciclo);
                htmlFilas.AppendFormat(@"<td>{0}</td>", item.Nivel);
                htmlFilas.AppendFormat(@"<td>{0}</td>", item.Vez);
                htmlFilas.Append(@"</tr>");
            }

            litFilasTabla.Text = htmlFilas.ToString();
        }

        private List<DatosMatricula> ObtenerDatosMatricula()
        {
            return new List<DatosMatricula>
            {
                new DatosMatricula { Idioma = "Inglés", Ciclo = "2024-1", Nivel = "Básico", Vez = "Primera vez" },
                new DatosMatricula { Idioma = "Francés", Ciclo = "2024-1", Nivel = "Intermedio", Vez = "Segunda vez" }
            };
        }
    }

    public class DatosMatricula
    {
        public string Idioma { get; set; }
        public string Ciclo { get; set; }
        public string Nivel { get; set; }
        public string Vez { get; set; }
    }
}