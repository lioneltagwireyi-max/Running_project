using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoneFit.BackendServiceReference;

namespace PhoneFit
{
    public partial class ManagerProducts : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            // only a logged in manager may access product management
            if (Session["RoleName"] == null || Session["RoleName"].ToString() != "Manager")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                DisplayProducts();
            }


        }

        private void DisplayProducts()
        {
            // get all products from the WCF service
            ManagerProductInfo[] products = client.GetManagerProducts();

            // display empty message if there are no products
            if(products == null || products.Length == 0)
            {
                pnlNoProducts.Visible = true;
                pnlProducts.Visible = false;
                return;
            }

            // products exist - display the product table
            pnlNoProducts.Visible = false;
            pnlProducts.Visible = true;

            rptProducts.DataSource = products;
            rptProducts.DataBind();
        }

        protected void rptProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "ChangeStatus")
            {
                // get the phoneModelID and current status from the product row that was clicked
                string[] productDetails = e.CommandArgument.ToString().Split(',');

                int phoneModelID = Convert.ToInt32(productDetails[0]);
                bool currentStatus = Convert.ToBoolean(productDetails[1]);

                // change the status to the opposite of its current value
                bool newStatus = !currentStatus;

                // send the phoneModelID and the new ststus to the wcf service
                bool wasChanged = client.ChangeProductStatus(phoneModelID, newStatus);

                if (wasChanged)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;

                    if(newStatus == true)
                    {
                        lblMessage.Text = "The product has been reactivated.";
                    }
                    else
                    {
                        lblMessage.Text = "The product has been deactivated.";
                    }

                    // reload the table with the updated information
                    DisplayProducts();
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "The product status could not be changed";
                }
            }
        }
    }
}