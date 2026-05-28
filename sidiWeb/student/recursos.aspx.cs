using System;

namespace sidiWeb.student
{
    public partial class recursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Aquí puedes verificar si el alumno ha iniciado sesión antes de mostrar los recursos
            if (!IsPostBack)
            {
                // Lógica de carga inicial si fuera necesaria
            }
        }
    }
}