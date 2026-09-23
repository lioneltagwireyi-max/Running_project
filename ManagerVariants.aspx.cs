using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoneFit.BackendServiceReference;

namespace PhoneFit
{
    public partial class ManagerVariants : System.Web.UI.Page
    {
        Service1Client variantClient = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            // only managers can access variant management
            if(Session["RoleName"] == null || Session["RoleName"].ToString() != "Manager")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadVariants();
            }
        }

        private void LoadVariants()
        {
            // ensure the url contains a phonemodel id
            if(Request.QueryString["id"] == null)
            {
                Response.Redirect("ManagerProducts.aspx");
                return;
            }

            int phoneModelID;
            if(!int.TryParse(Request.QueryString["id"], out phoneModelID))
            {
                Response.Redirect("ManagerProducts.aspx");
                return;
            }

            // load the phone info so the manager knows which phone is being managed
            ManagerProductDetails product = variantClient.GetManagerProductByID(phoneModelID);

            if(product == null)
            {
                lblMessage.Text = "The selected product could not be found.";
                pnlNoVariants.Visible = false;
                return;
            }

            lblPhoneName.Text = product.ModelName + " - Variant Management";

            // get all variants belonging to the phone
            ManagerVariantInfo[] variants = variantClient.GetManagerVariants(phoneModelID);

            if(variants == null || variants.Length == 0)
            {
                pnlNoVariants.Visible = true;
                pnlVariants.Visible = false;
                return;
            }

            pnlNoVariants.Visible = false;
            pnlVariants.Visible = true;

            rptVariants.DataSource = variants;
            rptVariants.DataBind();
        }

        protected void btnAddVariant_Click(object sender, EventArgs e)
        {
            Page.Validate("VariantGroup");
            if (!Page.IsValid)
            {
                return;
            }

            int phoneModelID;
            if(!int.TryParse(Request.QueryString["id"], out phoneModelID))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "The selected product is invalid.";
                return;
            }

            // convert the variant info from the form
            int ram = Convert.ToInt32(txtRAM.Text);
            int storage = Convert.ToInt32(txtStorage.Text);
            int stockQuantity = Convert.ToInt32(txtStockQuantity.Text);
            int lowStockLevel = Convert.ToInt32(txtLowStockLevel.Text);
            decimal price = Convert.ToDecimal(txtPrice.Text.Replace(".", ","));

            ManagerVariantInfo newVariant = new ManagerVariantInfo
            {
                PhoneModelID = phoneModelID,
                RAMGB = ram,
                StorageGB = storage,
                Colour = txtColour.Text.Trim(),
                Price = price,
                StockQuantity = stockQuantity,
                LowStockLevel = lowStockLevel,
                IsActive = true
            };

            int result = variantClient.AddVariant(phoneModelID, newVariant);

            if(result == 0)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "The variant was added successfully.";

                // clear the add variant form
                txtRAM.Text = "";
                txtStorage.Text = "";
                txtColour.Text = "";
                txtPrice.Text = "";
                txtStockQuantity.Text = "";
                txtLowStockLevel.Text = "";

                // reload the page
                LoadVariants();
            }
            else if(result == 2)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "This variant already exists for this phone (same RAM, storage and colour).";
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "The variant could not be added.";
            }
        }

        protected void rptVariants_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ChangeStatus")
            {
                // get the VariantID and current status from the selected variant
                string[] variantDetails = e.CommandArgument.ToString().Split(',');

                int variantID = Convert.ToInt32(variantDetails[0]);
                bool currentStatus = Convert.ToBoolean(variantDetails[1]);

                // Change the status to the opposite of its current value
                bool newStatus = !currentStatus;

                bool wasChanged = variantClient.ChangeVariantStatus(variantID, newStatus);

                if (wasChanged)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;

                    if (newStatus == true)
                    {
                        lblMessage.Text = "The variant has been reactivated.";
                    }
                    else
                    {
                        lblMessage.Text = "The variant has been deactivated.";
                    }

                    // reload
                    LoadVariants();
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "The variant status could not be changed.";
                }
            }

        }
    }
}