using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoneFit.BackendServiceReference;

namespace PhoneFit
{
    public partial class Invoice : System.Web.UI.Page
    {
        Service1Client invoiceClient = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            // the customer invoice is only available to the logged in customer
            if(Session["UserID"] == null || Session["RoleName"] == null || Session["RoleName"].ToString() != "Customer")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                DisplayInvoice();
            }
        }

        private void DisplayInvoice()
        {
            // ensure the orderID was supplied in the url
            int orderID;
            if(!int.TryParse(Request.QueryString["id"], out orderID))
            {
                lblMessage.Text = "The selected invoice is invalid.";
                pnlInvoice.Visible = false;
                return;
            }

            // get the logged in user customer id
            int userID = Convert.ToInt32(Session["UserID"]);

            // prevent customer from viewing another customers invoice
            // wcf checks the orderID and userID
            InvoiceInfo invoice = invoiceClient.GetInvoice(orderID, userID);

            if(invoice == null)
            {
                lblMessage.Text = "The invoice could not be found or does not belong to your account.";
                pnlInvoice.Visible = false;
                return;
            }

            pnlInvoice.Visible = true;

            // invoice heading and customer info
            lblOrderID.Text = invoice.OrderID.ToString();
            lblOrderDate.Text = invoice.OrderDate.ToString("dd MMM yyyy HH:mm");
            lblOrderStatus.Text = invoice.OrderStatus;
            lblCustomerName.Text = invoice.CustomerName;
            lblCustomerEmail.Text = invoice.CustomerEmail;

            // purchased items
            rptInvoiceItems.DataSource = invoice.Items;
            rptInvoiceItems.DataBind();

            // stored transaction amounts
            lblSubtotal.Text = "R" + invoice.Subtotal.ToString("N2");

            if(invoice.DiscountAmount > 0)
            {
                lblDiscount.Text = "-R" + invoice.DiscountAmount.ToString("N2");
            }
            else
            {
                lblDiscount.Text = "R0.00";
            }

            if(invoice.ShippingAmount == 0)
            {
                lblShipping.Text = "Free";
            }
            else
            {
                lblShipping.Text = "R" + invoice.ShippingAmount.ToString("N2");
            }

            lblVAT.Text = "R" + invoice.VATAmount.ToString("N2");
            lblTotal.Text = "R" + invoice.TotalAmount.ToString("N2");
        }
    }
}