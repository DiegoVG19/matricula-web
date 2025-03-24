using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sidiWeb
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si el usuario está autenticado
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Limpiar la sesión
            Session.Clear();
            Session.Abandon();

            if (Request.Cookies["YourAuthCookie"] != null)
            {
                Response.Cookies["YourAuthCookie"].Expires = DateTime.Now.AddDays(-1);
            }

            // Redirigir a la página de inicio de sesión
            Response.Redirect("Login.aspx");
        }

        protected void btnVerPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void btnVerGrupos_Click(object sender, EventArgs e)
        {
            Response.Redirect("grupos.aspx");
        }

        protected void btnVerPagos_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagos.aspx");
        }
    }
}