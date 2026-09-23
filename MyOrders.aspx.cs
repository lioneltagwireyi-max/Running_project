using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoneFit.BackendServiceReference;

namespace PhoneFit
{
    public partial class MyOrders : System.Web.UI.Page
    {
        Service1Client orderClient = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            // my orders only available to logged in user
            if(Session["UserID"] == null || Session["RoleName"] == null || Session["RoleName"].ToString() != "Customer")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                DisplayOrders();
            }
        }

        private void DisplayOrders()
        {
            // get the userID from the session
            int userID = Convert.ToInt32(Session["UserID"]);

            // get orders belonging to customer
            CustomerOrderInfo[] orders = orderClient.GetCustomerOrders(userID);

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