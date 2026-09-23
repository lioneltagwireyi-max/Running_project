using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoneFit.BackendServiceReference;

namespace PhoneFit
{
    public partial class ManagerEditVariant : System.Web.UI.Page
    {
        Service1Client varientClient = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            // only managers may access edit variant
            if(Session["RoleName"] == null || Session["RoleName"].ToString() != "Manager")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadVariant();
            }
        }

        private void LoadVariant()
        {
            int variantID;
            if(!int.TryParse(Request.QueryString["id"], out variantID))
            {
                Response.Redirect("ManagerProducts.aspx");
                return;
            }

            ManagerVariantInfo variant = varientClient.GetManagerVariantByID(variantID);

            if(variant == null)
            {
                lblMessage.Text = "The selected variant could not be found.";
                return;
            }

            txtRAM.Text = variant.RAMGB.ToString();
            txtStorage.Text = variant.StorageGB.ToString();
            txtColour.Text = variant.Colour;
            txtPrice.Text = variant.Price.ToString().Replace(",", ".");
            txtStockQuantity.Text = variant.StockQuantity.ToString();
            txtLowStockLevel.Text = variant.LowStockLevel.ToString();

            // return to the variants list page
            lnkBackToVariants.NavigateUrl = "ManagerVariants.aspx?id=" + variant.PhoneModelID;
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            Page.Validate("EditVariantGroup");
            if (!Page.IsValid)
            {
                return;
            }

            int variantID;

            if (!int.TryParse(Request.QueryString["id"], out variantID))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "The selected variant is invalid.";
                return;
            }

            // get the existing variants phoneModelID
            ManagerVariantInfo existingVariant = varientClient.GetManagerVariantByID(variantID);

            if (existingVariant == null)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "The selected variant could not be found.";
                return;
            }

            decimal price = Convert.ToDecimal(txtPrice.Text.Replace(".", ","));

            ManagerVariantInfo updatedVariant = new ManagerVariantInfo
            {
                VariantID = variantID,
                PhoneModelID = existingVariant.PhoneModelID,
                RAMGB = Convert.ToInt32(txtRAM.Text),
                StorageGB = Convert.ToInt32(txtStorage.Text),
                Colour = txtColour.Text.Trim(),
                Price = price,
                StockQuantity = Convert.ToInt32(txtStockQuantity.Text),
                LowStockLevel = Convert.ToInt32(txtLowStockLevel.Text),
                IsActive = existingVariant.IsActive
            };

            int result = varientClient.UpdateVariant(variantID, updatedVariant);

            if (result == 0)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "The variant was updated successfully.";
            }
            else if (result == 2)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Another variant with the same RAM, storage and colour exists.";
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "The variant could not be updated.";
            }
        }
    }
}