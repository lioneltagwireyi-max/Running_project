using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoneFit.BackendServiceReference;

namespace PhoneFit
{
    public partial class Checkout : System.Web.UI.Page
    {
        Service1Client checkoutClient = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            // checkout is only available to a logged in customer
            if (Session["UserID"] == null || Session["RoleName"] == null || Session["RoleName"].ToString() != "Customer")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                DisplayCheckout();
            }
        }

        private void DisplayCheckout()
        {
            // get the logged in customer's UserID from Session
            int userID = Convert.ToInt32(Session["UserID"]);

            // get the customers cart
            CartItemInfo[] cartItems = checkoutClient.GetCartItems(userID);

            // the customer cannot checkout with an empty cart
            if (cartItems == null || cartItems.Length == 0)
            {
                pnlEmptyCart.Visible = true;
                pnlCheckout.Visible = false;
                return;
            }

            pnlEmptyCart.Visible = false;
            pnlCheckout.Visible = true;

            // display all cart items in the checkout summary.
            rptCheckoutItems.DataSource = cartItems;
            rptCheckoutItems.DataBind();

            int itemCount = 0;
            decimal subTotal = 0;

            // calculate the total quantity and subtotal.
            foreach (CartItemInfo item in cartItems)
            {
                itemCount += item.Quantity;
                subTotal += item.LineTotal;
            }

            if (itemCount == 1)
            {
                lblItemCount.Text = "1 item";
            }
            else
            {
                lblItemCount.Text = itemCount + " items";
            }

            // Transaction processing rule 1: 5% discount for orders >= R20 000
            decimal discount = 0;
            if (subTotal >= 20000)
            {
                discount = subTotal * 0.05m;
            }

            // Transaction processing rule 2: Shipping - standard is R150 with free
            // shipping if order >= R10 000
            decimal shipping = 150;
            if (subTotal >= 10000)
            {
                shipping = 0;
            }

            // calculate the final amount
            decimal total = subTotal - discount + shipping;

            // Transaction processing rule 3: 15% VAT - already included with final amount
            decimal vat = total * 15 / 115;

            // display transaction info
            lblSubtotal.Text = "R" + subTotal.ToString("N2");

            if(discount > 0)
            {
                lblDiscount.Text = "- R" + discount.ToString("N2");
            }
            else
            {
                lblDiscount.Text = "R0.00";
            }

            if(shipping == 0)
            {
                lblShipping.ForeColor = System.Drawing.Color.Green;
                lblShipping.Text = "Free";
            }
            else
            {
                lblShipping.Text = "R" + shipping.ToString("N2");
            }

            lblVAT.Text = "R" + vat.ToString("N2");

            lblTotal.Text = "R" + total.ToString("N2");

        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            // ensure a customer is stil logged in
            if(Session["UserID"] == null || Session["RoleName"] == null || Session["RoleName"].ToString() != "Customer")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            // get the logged in customer id
            int userID = Convert.ToInt32(Session["UserID"]);

            // place the order through wcf
            OrderResult result = checkoutClient.PlaceOrder(userID);

            if(result.OrderStatus == "Success")
            {
                // redirect to the invoice page
                Response.Redirect("Invoice.aspx?id=" + result.OrderID);
            }
            else if(result.OrderStatus == "EmptyCart")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Your cart is empty.";
            }
            else if(result.OrderStatus == "InsufficientStock")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "One or more products no longer have enough stock. Please review your cart.";
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Failed to process order. Please try again.";
            }
        }

    }
}