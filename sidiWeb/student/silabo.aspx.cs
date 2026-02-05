using System;
using System.Web.UI;

namespace sidiWeb.student
{
    public partial class silabo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Aquí podrías cargar información del curso desde la BD al iniciar
        }

        // Este es el método que le falta a tu código para que no de error
        protected void btnDescargarSilabo_Click(object sender, EventArgs e)
        {
            try
            {
                // Nombre del archivo que subió el profesor (ejemplo)
                string nombreArchivo = "Silabo_Ingles_Basico_2.pdf";

                // Buscamos la ruta física en el servidor
                string rutaFisica = Server.MapPath("~/uploads/silabos/" + nombreArchivo);

                if (System.IO.File.Exists(rutaFisica))
                {
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreArchivo);
                    Response.TransmitFile(rutaFisica);
                    Response.End();
                }
                else
                {
                    // Si el archivo no existe, mandamos un mensaje al alumno
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El sílabo aún no está disponible para descarga.');", true);
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores básicos
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al intentar descargar: " + ex.Message + "');", true);
            }
        }
    }
}