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
            if (Session["UserId"] == null)
            {
                Response.Redirect("/");
                return;
            }
        }

        protected void btnEditarPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("perfil.aspx");
        }

        protected void btnCambiarContra_Click(object sender, EventArgs e)
        {
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            if (Request.Cookies["YourAuthCookie"] != null)
            {
                Response.Cookies["YourAuthCookie"].Expires = DateTime.Now.AddDays(-1);
            }

            Response.Redirect("/");
        }

        protected void btnVerInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/student/inicioStudent.aspx");
        }

        protected void btnVerPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("perfilStudent.aspx");
        }

        protected void btnVerGrupos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/student/grupos.aspx");
        }

        protected void btnVerPagos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/student/pagos.aspx");
        }

        protected void btnMatricula_Click(object sender, EventArgs e)
        {
            Response.Redirect("matricula.aspx");
        }

        protected void btnSilabo_Click(object sender, EventArgs e)
        {
            Response.Redirect("silabo.aspx");
        }

        protected void btnPagalo_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagalo.aspx");
        }

        protected void btnRecordAsis_Click(object sender, EventArgs e)
        {
            Response.Redirect("recordasis.aspx");
        }

        protected void btnrecursos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/student/recursos.aspx");
        }

        protected void btnVerAyuda_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/student/ayuda.aspx");
        }
    }
}