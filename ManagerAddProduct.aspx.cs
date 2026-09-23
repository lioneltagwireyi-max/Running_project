using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoneFit.BackendServiceReference;

namespace PhoneFit
{
    public partial class ManagerAddProduct : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            // only managers may access the add product page
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
            // get all active brands from the wcf service
            BrandInfo[] brands = client.GetActiveBrands();

            // no active brands found
            if(brands == null || brands.Length == 0)
            {
                lblMessage.Text = "No active brands are available.";
                ddlBrand.Enabled = false;
                return;
            }

            // display the brands names in the dropdown list
            ddlBrand.DataSource = brands;
            ddlBrand.DataTextField = "BrandName";
            ddlBrand.DataValueField = "BrandID";
            ddlBrand.DataBind();
        }

        protected void btnSaveProduct_Click(object sender, EventArgs e)
        {
            // validate all fields belonging to the product group
            Page.Validate("ProductGroup");

            if (!Page.IsValid)
            {
                return;
            }

            // get the brandID of the brand selected
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

            // create one object containing all info required to create the new product
            NewProductInfo newProduct = new NewProductInfo
            {
                // PhoneModel info
                BrandID = brandID,
                ModelName = txtModelName.Text.Trim(),
                OperatingSystem = txtOperatingSystem.Text.Trim(),
                ReleaseYear = releaseYear,
                Description = txtDescription.Text.Trim(),
                ImagePath = txtImagePath.Text.Trim(),

                // PhoneSpecification info
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
                WaterResistance = txtWaterResistance.Text.Trim(),

                // PhoneVariant info
                RAMGB = Convert.ToInt32(txtRAM.Text),
                StorageGB = Convert.ToInt32(txtStorage.Text),
                Colour = txtColour.Text.Trim(),
                Price = Convert.ToDecimal(txtPrice.Text.Replace(".", ",")),
                StockQuantity = Convert.ToInt32(txtStockQuantity.Text),
                LowStockLevel = Convert.ToInt32(txtLowStockLevel.Text)
            };

            // send the product info to the WCF service
            int result = client.AddProduct(newProduct);

            if(result == 0)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "The product was added successfully.";

                ClearForm();
            }
            else if(result == 2)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "This phone model already exists for the selected brand.";
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "The product could not be added";
            }
        }

        private void ClearForm()
        {
            ddlBrand.SelectedIndex = 0;

            // Phone information
            txtModelName.Text = "";
            txtOperatingSystem.Text = "";
            txtReleaseYear.Text = "";
            txtDescription.Text = "";
            txtImagePath.Text = "";

            // Specifications
            txtProcessor.Text = "";
            txtScreenSize.Text = "";
            txtScreenType.Text = "";
            txtRefreshRate.Text = "";
            txtBatteryCapacity.Text = "";
            txtRearCameraMP.Text = "";
            txtFrontCameraMP.Text = "";
            txtWaterResistance.Text = "";

            chkSupports5G.Checked = false;
            chkDualSIM.Checked = false;
            chkExpandableStorage.Checked = false;

            // Initial variant
            txtRAM.Text = "";
            txtStorage.Text = "";
            txtColour.Text = "";
            txtPrice.Text = "";
            txtStockQuantity.Text = "";
            txtLowStockLevel.Text = "";
        }
    }
}