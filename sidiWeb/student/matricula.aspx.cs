using System;
using System.Web.UI;

namespace sidiWeb.student
{
    public partial class matricula : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            if (fuVoucher.HasFile)
            {
                pnlVoucher.Visible = false;
                pnlPendiente.Visible = true;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, seleccione un archivo primero.');", true);
            }
        }
    }
}