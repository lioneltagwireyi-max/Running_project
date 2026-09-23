using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoneFit.BackendServiceReference;

namespace PhoneFit
{
    public partial class ManagerEditBrand : System.Web.UI.Page
    {
        Service1Client brandClient = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            // only managers may access edit brand
            if(Session["RoleName"] == null || Session["RoleName"].ToString() != "Manager")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadBrand();
            }
        }

        private void LoadBrand()
        {
            // ensure a brandid is supplied in the url
            if(Request.QueryString["id"] == null)
            {
                Response.Redirect("ManagerBrands.aspx");
                return;
            }

            int brandID;
            if(!int.TryParse(Request.QueryString["id"], out brandID))
            {
                Response.Redirect("ManagerBrands.aspx");
                return;
            }

            // retrieve the selected brand from wcf
            ManagerBrandInfo brand = brandClient.GetManagerBrandByID(brandID);

            if(brand == null)
            {
                lblMessage.Text = "The selected brand could not be found.";
                return;
            }

            // load the esisting values to the form
            txtBrandName.Text = brand.BrandName;
            txtBrandDescription.Text = brand.BrandDescription;
        }

        protected void btnSaveChanges_Click(object sender, EventArgs E)
        {
            Page.Validate("EditBrandGroup");
            if (!Page.IsValid)
            {
                return;
            }

            int brandID;
            if(!int.TryParse(Request.QueryString["id"], out brandID))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "The selected brand is invalid";
                return;
            }

            // build the updated brand object
            ManagerBrandInfo updateBrand = new ManagerBrandInfo
            {
                BrandID = brandID,
                BrandName = txtBrandName.Text.Trim(),
                BrandDescription = txtBrandDescription.Text.Trim()
            };

            // send update to wcf
            int result = brandClient.UpdateBrand(brandID, updateBrand);

            if(result == 0)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "The brand was updated successfully.";
            }
            else if(result == 2)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Another brand already exists with this name.";
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "The brand could not be updated.";
            }
        }
    }
}