using System;
using System.Web.UI;

namespace sidiWeb.student
{
    public partial class matricula : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Aquí podrías cargar datos del alumno de la BD si quisieras
        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            if (fuVoucher.HasFile)
            {
                // Simulación: Aquí guardarías el archivo en el servidor o BD
                // string nombreArchivo = fuVoucher.FileName;

                // Cambiamos de panel para simular el envío
                pnlVoucher.Visible = false;
                pnlPendiente.Visible = true;
            }
            else
            {
                // Opcional: Script para avisar que falta el archivo
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, seleccione un archivo primero.');", true);
            }
        }
    }
}