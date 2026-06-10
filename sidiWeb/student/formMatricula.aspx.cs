using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sidiWeb.student
{
    public partial class formMatricula : System.Web.UI.Page
    {
        // Propiedad para mantener la lista de cursos validados en memoria entre recargas de página
        private List<ConceptoAcademico> CursosValidados
        {
            get
            {
                if (ViewState["CursosValidados"] == null)
                {
                    ViewState["CursosValidados"] = new List<ConceptoAcademico>();
                }
                return (List<ConceptoAcademico>)ViewState["CursosValidados"];
            }
            set
            {
                ViewState["CursosValidados"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatosEstudiante();
                VerificarContinuidadEstudiante();
                ActualizarInterfazCarrito();
            }
            lblMensajeError.Visible = false; // Ocultar errores previos en cada recarga
        }

        private void CargarDatosEstudiante()
        {
            lblNombreCompleto.Text = "Juan Pérez García";
            lblCodigo.Text = "20240001";
            lblEstudianteNombre.Text = "Juan Pérez";
        }

        private void VerificarContinuidadEstudiante()
        {
            int mesesInactivo = 0; // Cambiado a 0 para pruebas limpias
            ClientScript.RegisterHiddenField("hdfMesesInactivo", mesesInactivo.ToString());

            if (mesesInactivo == 0)
            {
                MostrarEstadoContinuidad("success", "Estudiante Continuo", "Puedes continuar normalmente.");
            }
            else
            {
                MostrarEstadoContinuidad("danger", "Requiere Evaluación", "Acércate al área académica.");
                btnConfirmarMatricula.Enabled = false;
                txtNumeroOperacion.Enabled = false;
                btnValidarPago.Enabled = false;
            }
        }

        private void MostrarEstadoContinuidad(string tipo, string titulo, string mensaje)
        {
            divEstadoContinuidad.Visible = true;
            estadoContinuidadText.InnerHtml = $@"
                    <div class='alert alert-{tipo} border-start border-4 border-{tipo} mb-0'>
                        <h6 class='mb-2'>{titulo}</h6><p class='mb-0'>{mensaje}</p>
                    </div>";
        }

        protected void btnValidarPago_Click(object sender, EventArgs e)
        {
            string numeroOperacion = txtNumeroOperacion.Text.Trim();

            if (string.IsNullOrEmpty(numeroOperacion))
            {
                MostrarErrorLocal("Por favor ingrese el número de operación.");
                return;
            }

            // Evitar agregar el mismo voucher dos veces
            if (CursosValidados.Any(c => c.NumeroOperacion == numeroOperacion))
            {
                MostrarErrorLocal("Este número de operación ya fue agregado a la lista.");
                txtNumeroOperacion.Text = "";
                return;
            }

            // Buscar en BD
            ConceptoAcademico conceptoNuevo = ValidarPagoConBanco(numeroOperacion);

            if (conceptoNuevo != null)
            {
                // Agregarlo a la "Cesta" de memoria
                var listaTemporal = CursosValidados;
                listaTemporal.Add(conceptoNuevo);
                CursosValidados = listaTemporal;

                // Limpiar input y actualizar vista
                txtNumeroOperacion.Text = "";
                ActualizarInterfazCarrito();
                MostrarMensajeExitoJs($"Curso de {conceptoNuevo.NombreIdioma} agregado correctamente.");
            }
            else
            {
                MostrarErrorLocal("Número de operación no encontrado o inválido en el sistema bancario.");
            }
        }

        protected void rptCursosAgregados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Quitar")
            {
                string numOperacionQuitar = e.CommandArgument.ToString();
                var listaTemporal = CursosValidados;

                var itemRemover = listaTemporal.FirstOrDefault(c => c.NumeroOperacion == numOperacionQuitar);
                if (itemRemover != null)
                {
                    listaTemporal.Remove(itemRemover);
                    CursosValidados = listaTemporal;
                    ActualizarInterfazCarrito();
                }
            }
        }

        private void ActualizarInterfazCarrito()
        {
            var lista = CursosValidados;

            // Actualizar tabla visual
            rptCursosAgregados.DataSource = lista;
            rptCursosAgregados.DataBind();

            // Mostrar u ocultar paneles vacíos
            bool hayCursos = lista.Count > 0;
            pnlCarritoVacio.Visible = !hayCursos;
            rptCursosAgregados.Visible = hayCursos;
            btnConfirmarMatricula.Enabled = hayCursos;

            // Calcular Sumatoria Total
            decimal sumaTotal = lista.Sum(c => c.CostoCiclo);
            lblTotalPagar.Text = $"S/ {sumaTotal.ToString("0.00")}";

            // Opcional: Agregar costo de reinscripción al total si mesesInactivo > 0
        }

        // SIMULADOR DE BASE DE DATOS ESTRUCTURADA
        private ConceptoAcademico ValidarPagoConBanco(string numeroOperacion)
        {
            var baseDatosPagos = new Dictionary<string, ConceptoAcademico>
            {
                { "111", new ConceptoAcademico { NumeroOperacion = "111", CodigoIdioma = "ID-ING", NombreIdioma = "Inglés", CodigoNivel = "NV-BAS", NombreNivel = "Básico", CodigoModalidad = "MOD-PART", NombreModalidad = "Particular", CostoCiclo = 100m } },
                { "222", new ConceptoAcademico { NumeroOperacion = "222", CodigoIdioma = "ID-ING", NombreIdioma = "Inglés", CodigoNivel = "NV-BAS", NombreNivel = "Básico", CodigoModalidad = "MOD-FAUS", NombreModalidad = "Faustiniano", CostoCiclo = 80m } },
                { "333", new ConceptoAcademico { NumeroOperacion = "333", CodigoIdioma = "ID-FRA", NombreIdioma = "Francés", CodigoNivel = "NV-INT", NombreNivel = "Intermedio", CodigoModalidad = "MOD-CONV", NombreModalidad = "Convenio", CostoCiclo = 90m } },
                { "444", new ConceptoAcademico { NumeroOperacion = "444", CodigoIdioma = "ID-ALE", NombreIdioma = "Alemán", CodigoNivel = "NV-AVA", NombreNivel = "Avanzado", CodigoModalidad = "MOD-PART", NombreModalidad = "Particular", CostoCiclo = 120m } }
            };

            return baseDatosPagos.ContainsKey(numeroOperacion) ? baseDatosPagos[numeroOperacion] : null;
        }

        protected void btnConfirmarMatricula_Click(object sender, EventArgs e)
        {
            // Aquí enviarás toda la lista 'CursosValidados' a la Base de Datos con un bucle foreach.
            MostrarMensajeExitoJs("¡Matrícula múltiple confirmada exitosamente!");
            CursosValidados.Clear(); // Limpiar carrito

            string script = "setTimeout(function() { window.location.href = 'inicioStudent.aspx'; }, 3000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirigir", script, true);
        }

        private void MostrarErrorLocal(string mensaje)
        {
            lblMensajeError.Text = $"<i class='fas fa-exclamation-circle me-1'></i> {mensaje}";
            lblMensajeError.Visible = true;
        }

        private void MostrarMensajeExitoJs(string mensaje)
        {
            // Llama a tu función mostrarAlerta de tu archivo matricula.js
            string script = $"mostrarAlerta('{mensaje.Replace("'", "\\'")}', 'success');";
            ClientScript.RegisterStartupScript(this.GetType(), "MostrarExito", script, true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            CursosValidados.Clear();
            Response.Redirect("~/student/matricula.aspx");
        }
    }

    [Serializable] // Requisito vital para guardar la lista en el ViewState
    public class ConceptoAcademico
    {
        public string NumeroOperacion { get; set; } // Agregado para poder eliminar de la lista
        public string CodigoIdioma { get; set; }
        public string NombreIdioma { get; set; }
        public string CodigoNivel { get; set; }
        public string NombreNivel { get; set; }
        public string CodigoModalidad { get; set; }
        public string NombreModalidad { get; set; }
        public decimal CostoCiclo { get; set; }
    }
}