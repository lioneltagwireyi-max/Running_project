using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoneFit.BackendServiceReference;

namespace PhoneFit
{
    public partial class ManagerBrands : System.Web.UI.Page
    {
        Service1Client brandclient = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            // only managers can acess Brand management
            if(Session["RoleName"] == null || Session["RoleName"].ToString() != "Manager")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                DisplayBrands();

            }
        }

        private void DisplayBrands()
        {
            // get all brands from wcf
            ManagerBrandInfo[] brands = brandclient.GetManagerBrands();

            // no brandss found
            if(brands == null || brands.Length == 0)
            {
                pnlNoBrands.Visible = true;
                pnlBrands.Visible = false;
                return;
            }

            // display the brands
            pnlNoBrands.Visible = false;
            pnlBrands.Visible = true;

            rptBrands.DataSource = brands;
            rptBrands.DataBind();
        }

        protected void btnAddBrand_Click(object sender, EventArgs e)
        {
            Page.Validate("BrandGroup");

            if (!Page.IsValid)
            {
                return;
            }

            string brandname = txtBrandName.Text.Trim();
            string brandDescription = txtBrandDescription.Text.Trim();

            int result = brandclient.AddBrand(brandname, brandDescription);

            if(result == 0)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "The brand was added successfully.";

                // clear the form
                txtBrandName.Text = "";
                txtBrandDescription.Text = "";

                // reload the brand table with the newly added brand
                DisplayBrands();
            }
            else if(result == 2)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "This brand already exists.";
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "The brand could not be added.";
            }
        }

        protected void rptBrands_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "ChangeStatus")
            {
                // get the brandID and current status from the selected row
                string[] brandDetails = e.CommandArgument.ToString().Split(',');

                int brandID = Convert.ToInt32(brandDetails[0]);
                bool currentStatus = Convert.ToBoolean(brandDetails[1]);

                // change the current status to its opposite
                bool newStatus = !currentStatus;

                // update the brand through wcf
                bool wasChanged = brandclient.ChangeBrandStatus(brandID, newStatus);

                if (wasChanged)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    
                    if(newStatus == true)
                    {
                        lblMessage.Text = "The brand has been reactivated.";
                    }
                    else
                    {
                        lblMessage.Text = "The brand has been deactivated.";
                    }

                    // reload the table
                    DisplayBrands();
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "The brand status could not be changed.";
                }
            }
        }
    }


}