using System;
using System.Web.UI.WebControls;
using PhoneFit.BackendServiceReference;

namespace PhoneFit
{
    public partial class ManagerReports : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();


        protected void Page_Load(object sender, EventArgs e)
        {
            // only Managers may access reports
            if (Session["RoleName"] == null || Session["RoleName"].ToString() != "Manager")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadProductFilter();
                LoadRegistrationFilter();

                DisplaySummary();
                DisplayProductSales();
                DisplayRegistrations();
            }
        }


        private void DisplaySummary()
        {
            // isplay the summary report values
            lblTotalRevenue.Text = "R" + client.GetTotalRevenue().ToString("N2");
            lblTotalOrders.Text = client.GetTotalOrders().ToString();
            lblTotalUnitsSold.Text = client.GetTotalUnitsSold().ToString();
            lblDifferentProductsSold.Text =  client.GetDifferentProductsSold().ToString();
            lblProductsOnHand.Text = client.GetProductsOnHand().ToString();
            lblLowStockVariants.Text = client.GetLowStockVariantCount().ToString();
        }


        private void LoadProductFilter()
        {
            // use the existing product sales report to populate the product dropdown list
            ProductSalesInfo[] productSales = client.GetProductSales();

            if (productSales == null)
            {
                return;
            }

            foreach (ProductSalesInfo product in productSales)
            {
                ListItem item = new ListItem(
                        product.ProductName,
                        product.PhoneModelID.ToString());

                ddlProductFilter.Items.Add(item);
            }
        }

        private void LoadRegistrationFilter()
        {
            // use the registrations report to populate the available registration dates
            UserRegistrationInfo[] registrations = client.GetUserRegistrationsPerDay();

            if (registrations == null)
            {
                return;
            }

            foreach (UserRegistrationInfo registration in registrations)
            {
                ListItem item = new ListItem(
                        registration.RegistrationDate.ToString("dd MMM yyyy"),
                        registration.RegistrationDate.ToString("yyyy-MM-dd"));

                ddlRegistrationFilter.Items.Add(item);
            }
        }

        private void DisplayProductSales()
        {
            ProductSalesInfo[] productSales;

            // all Products selected
            if (ddlProductFilter.SelectedValue == "All")
            {
                productSales = client.GetProductSales();
            }
            else
            {
                // send the selected PhoneModelID to wcf
                int phoneModelID = Convert.ToInt32(ddlProductFilter.SelectedValue);
                productSales = client.GetProductSalesByPhoneID(phoneModelID);
            }


            if (productSales == null || productSales.Length == 0)
            {
                pnlNoProductSales.Visible = true;
                pnlProductSales.Visible = false;

                hfProductLabels.Value = "";
                hfProductValues.Value = "";

                return;
            }

            pnlNoProductSales.Visible = false;
            pnlProductSales.Visible = true;


            // display Product Sales table.
            rptProductSales.DataSource = productSales;
            rptProductSales.DataBind();

            // prepare the returned wcf data for Chart.js.
            string productLabels = "";
            string productValues = "";


            foreach (ProductSalesInfo product in productSales)
            {
                if (productLabels != "")
                {
                    productLabels += "|";
                    productValues += "|";
                }

                productLabels += product.ProductName;

                productValues += product.UnitsSold.ToString();
            }

            hfProductLabels.Value = productLabels;
            hfProductValues.Value = productValues;
        }

        private void DisplayRegistrations()
        {
            UserRegistrationInfo[] registrations;

            // all Dates selected.
            if (ddlRegistrationFilter.SelectedValue == "All")
            {
                registrations = client.GetUserRegistrationsPerDay();
            }
            else
            {
                // send the selected date to wcf
                DateTime registrationDate = Convert.ToDateTime(ddlRegistrationFilter.SelectedValue);
                registrations = client.GetUserRegistrationsByDate(registrationDate);
            }

            if (registrations == null || registrations.Length == 0)
            {
                pnlNoRegistrations.Visible = true;
                pnlRegistrations.Visible = false;

                hfRegistrationLabels.Value = "";
                hfRegistrationValues.Value = "";

                return;
            }

            pnlNoRegistrations.Visible = false;
            pnlRegistrations.Visible = true;

            // display registrations table
            rptRegistrations.DataSource = registrations;
            rptRegistrations.DataBind();

            // prepare the returned wcf data for Chart.js
            string registrationLabels = "";
            string registrationValues = "";


            foreach (UserRegistrationInfo registration in registrations)
            {
                if (registrationLabels != "")
                {
                    registrationLabels += "|";
                    registrationValues += "|";
                }

                registrationLabels += registration.RegistrationDate.ToString("dd MMM");
                registrationValues += registration.UsersRegistered.ToString();
            }

            hfRegistrationLabels.Value = registrationLabels;
            hfRegistrationValues.Value = registrationValues;
        }

        protected void ddlProductFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayProductSales();
        }

        protected void ddlRegistrationFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayRegistrations();
        }
    }
}