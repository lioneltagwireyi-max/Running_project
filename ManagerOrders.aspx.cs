using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoneFit.BackendServiceReference;

namespace PhoneFit
{
    public partial class ManagerOrders : System.Web.UI.Page
    {
        Service1Client orderClient = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            // only managers can access
            if(Session["RoleName"] == null || Session["RoleName"].ToString() != "Manager")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                DisplayOrder();
            }
        }

        private void DisplayOrder()
        {
            // get all customer orders
            ManagerOrderInfo[] orders = orderClient.GetManagerOrders();

            if(orders == null || orders.Length == 0)
            {
                pnlNoOrders.Visible = true;
                pnlOrders.Visible = false;
                return;
            }

            pnlNoOrders.Visible = false;
            pnlOrders.Visible = true;
            rptOrders.DataSource = orders;
            rptOrders.DataBind();
        }
    }
}