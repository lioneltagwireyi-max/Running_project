using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoneFit.BackendServiceReference;

namespace PhoneFit
{
    public partial class ManagerEditProduct : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            // only managers can acess the edit product page
            if(Session["RoleName"] == null || Session["RoleName"].ToString() != "Manager")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                DisplayBrands();
                LoadProduct();
            }
        }

        private void DisplayBrands()
        {
            // get all active brands from the wcf service
            BrandInfo[] brands = client.GetActiveBrands();

            // no braands found
            if(brands == null || brands.Length == 0)
            {
                lblMessage.Text = "No active brands found.";
                ddlBrand.Enabled = false;
                return;
            }

            // display the brands names in the dropdown list
            ddlBrand.DataSource = brands;
            ddlBrand.DataTextField = "BrandName";
            ddlBrand.DataValueField = "BrandID";
            ddlBrand.DataBind();
        }

        private void LoadProduct()
        {
            // ensure the url contains a product id
            if(Request.QueryString["id"] == null)
            {
                Response.Redirect("ManagerProducts.aspx");
                return;
            }

            int phoneModelID;
            // convert the id from the url to an integer
            if(!int.TryParse(Request.QueryString["id"], out phoneModelID))
            {
                Response.Redirect("ManagerProducts.aspx");
                return;
            }

            // get the selected product from the wcf service
            ManagerProductDetails product = client.GetManagerProductByID(phoneModelID);

            if(product == null)
            {
                lblMessage.Text = "The selected product could not be found.";
                return;
            }

            // phonemodel info
            ddlBrand.SelectedValue = product.BrandID.ToString();
            txtModelName.Text = product.ModelName;
            txtOperatingSystem.Text = product.OperatingSystem;
            txtDescription.Text = product.Description;
            txtImagePath.Text = product.ImagePath;

            if (product.ReleaseYear.HasValue)
            {
                txtReleaseYear.Text = product.ReleaseYear.Value.ToString();
            }

            // phone specification info
            txtProcessor.Text = product.Processor;
            txtScreenType.Text = product.ScreenType;
            txtWaterResistance.Text = product.WaterResistance;

            if (product.ScreenSize.HasValue)
            {
                txtScreenSize.Text = product.ScreenSize.Value.ToString().Replace(",", ".");
            }

            if (product.RefreshRate.HasValue)
            {
                txtRefreshRate.Text = product.RefreshRate.Value.ToString();
            }

            if (product.BatteryCapacity.HasValue)
            {
                txtBatteryCapacity.Text = product.BatteryCapacity.Value.ToString();
            }

            if (product.RearCameraMP.HasValue)
            {
                txtRearCameraMP.Text = product.RearCameraMP.Value.ToString().Replace(",", ".");
            }

            if (product.FrontCameraMP.HasValue)
            {
                txtFrontCameraMP.Text = product.FrontCameraMP.Value.ToString().Replace(",", ".");
            }

            chkSupports5G.Checked = product.Supports5G;
            chkDualSIM.Checked = product.DualSIM;
            chkExpandableStorage.Checked = product.ExpandableStorage;
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            // get the phonemodel id from the url
            int phoneModelID;

            if(!int.TryParse(Request.QueryString["id"], out phoneModelID))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "The selected product is invalid.";
                return;
            }

            // get the selected brandID
            int brandID = Convert.ToInt32(ddlBrand.SelectedValue);

            // release year is optional
            int? releaseYear = null;

            if (!string.IsNullOrWhiteSpace(txtReleaseYear.Text))
            {
                releaseYear = Convert.ToInt32(txtReleaseYear.Text);
            }

            // screen size is optional
            decimal? screenSize = null;

            if (!string.IsNullOrWhiteSpace(txtScreenSize.Text))
            {
                screenSize = Convert.ToDecimal(txtScreenSize.Text.Replace(".", ","));
            }

            // refresh rate is optional
            int? refreshRate = null;

            if (!string.IsNullOrWhiteSpace(txtRefreshRate.Text))
            {
                refreshRate = Convert.ToInt32(txtRefreshRate.Text);
            }

            // baterry capacity is optional
            int? batteryCapacity = null;

            if (!string.IsNullOrWhiteSpace(txtBatteryCapacity.Text))
            {
                batteryCapacity = Convert.ToInt32(txtBatteryCapacity.Text);
            }

            // rear camera resolution is optional
            decimal? rearCameraMP = null;

            if (!string.IsNullOrWhiteSpace(txtRearCameraMP.Text))
            {
                rearCameraMP = Convert.ToDecimal(txtRearCameraMP.Text.Replace(".", ","));
            }

            // front camera resolution is optional
            decimal? frontCameraMP = null;

            if (!string.IsNullOrWhiteSpace(txtFrontCameraMP.Text))
            {
                frontCameraMP = Convert.ToDecimal(txtFrontCameraMP.Text.Replace(".", ","));
            }

            // create an object containing the updated information
            ManagerProductDetails updatedProduct = new ManagerProductDetails
            {
                PhoneModelID = phoneModelID,
                BrandID = brandID,
                ModelName = txtModelName.Text.Trim(),
                OperatingSystem = txtOperatingSystem.Text.Trim(),
                ReleaseYear = releaseYear,
                Description = txtDescription.Text.Trim(),
                ImagePath = txtImagePath.Text.Trim(),

                Processor = txtProcessor.Text.Trim(),
                ScreenSize = screenSize,
                ScreenType = txtScreenType.Text.Trim(),
                RefreshRate = refreshRate,
                BatteryCapacity = batteryCapacity,
                RearCameraMP = rearCameraMP,
                FrontCameraMP = frontCameraMP,

                Supports5G = chkSupports5G.Checked,
                DualSIM = chkDualSIM.Checked,
                ExpandableStorage = chkExpandableStorage.Checked,
                WaterResistance = txtWaterResistance.Text.Trim()
            };

            // send the updated information to the wcf service
            int result = client.UpdateProduct(phoneModelID, updatedProduct);

            if(result == 0)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "The product was updated successfully.";
            }
            else if(result == 2)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Another phone already exists with this brand and model.";
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "The product could not be updated.";
            }
        }
    }
}