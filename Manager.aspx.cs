using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhoneFit
{
    public partial class Manager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // only managers may access the dashboard
            if(Session["RoleName"] == null || Session["RoleName"].ToString() != "Manager")
            {
                Response.Redirect("Login.aspx");
                return;
            }
        }
    }
}