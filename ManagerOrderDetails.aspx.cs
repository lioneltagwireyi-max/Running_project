using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoneFit.BackendServiceReference;

namespace PhoneFit
{
    public partial class ManagerOrderDetails : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            // only Managers may view all customer transaction details
            if (Session["RoleName"] == null || Session["RoleName"].ToString() != "Manager")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                DisplayTransaction();
            }
        }

        private void DisplayTransaction()
        {
            int orderID;
            if (!int.TryParse(Request.QueryString["id"], out orderID))
            {
                lblMessage.Text = "The selected transaction is invalid.";
                pnlTransaction.Visible = false;
                return;
            }

            // get transaction details based on orderID
            InvoiceInfo transaction = client.GetManagerOrderDetails(orderID);

            if (transaction == null)
            {
                lblMessage.Text = "The selected transaction could not be found.";
                pnlTransaction.Visible = false;
                return;
            }

            pnlTransaction.Visible = true;

            lblOrderID.Text = transaction.OrderID.ToString();
            lblOrderDate.Text = transaction.OrderDate.ToString("dd MMM yyyy HH:mm");
            lblOrderStatus.Text = transaction.OrderStatus;
            lblCustomerName.Text = transaction.CustomerName;
            lblCustomerEmail.Text = transaction.CustomerEmail;

            rptOrderItems.DataSource = transaction.Items;
            rptOrderItems.DataBind();

            lblSubtotal.Text = "R" + transaction.Subtotal.ToString("N2");

            if (transaction.DiscountAmount > 0)
            {
                lblDiscount.Text = "-R" + transaction.DiscountAmount.ToString("N2");
            }
            else
            {
                lblDiscount.Text = "R0.00";
            }

            if (transaction.ShippingAmount == 0)
            {
                lblShipping.Text = "Free";
            }
            else
            {
                lblShipping.Text = "R" + transaction.ShippingAmount.ToString("N2");
            }

            lblVAT.Text = "R" + transaction.VATAmount.ToString("N2");
            lblTotal.Text = "R" + transaction.TotalAmount.ToString("N2");
        }
    }

}