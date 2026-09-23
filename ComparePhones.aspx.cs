using System;
using System.Web.UI.WebControls;
using PhoneFit.BackendServiceReference;

namespace PhoneFit
{
    public partial class ComparePhones : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPhones();
            }
        }

        private void LoadPhones()
        {
            PhoneCatalogue[] phones = client.GetActivePhonesByName();

            if (phones == null || phones.Length == 0)
            {
                lblMessage.Text = "No smartphones are currently available for comparison.";

                btnCompare.Enabled = false;
                return;
            }

            ddlPhone1.Items.Clear();
            ddlPhone2.Items.Clear();

            ddlPhone1.Items.Add(new ListItem("Select Phone 1", "0"));

            ddlPhone2.Items.Add(new ListItem("Select Phone 2", "0"));

            foreach (PhoneCatalogue phone in phones)
            {
                string phoneName = phone.BrandName + " " + phone.ModelName;
                string phoneID = phone.PhoneModelID.ToString();
                ddlPhone1.Items.Add(new ListItem(phoneName, phoneID));
                ddlPhone2.Items.Add(new ListItem(phoneName, phoneID));
            }
        }

        protected void btnCompare_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            pnlComparison.Visible = false;

            int phoneID1 = Convert.ToInt32(ddlPhone1.SelectedValue);

            int phoneID2 = Convert.ToInt32(ddlPhone2.SelectedValue);

            if (phoneID1 == 0 || phoneID2 == 0)
            {
                lblMessage.Text = "Please select two smartphones to compare.";
                return;
            }

            if (phoneID1 == phoneID2)
            {
                lblMessage.Text = "Please select two different smartphones.";
                return;
            }

            PhoneCatalogue phone1 = client.GetPhoneByID(phoneID1);

            PhoneCatalogue phone2 = client.GetPhoneByID(phoneID2);

            PhoneSpecification specification1 = client.GetSpecificationByPhoneID(phoneID1);

            PhoneSpecification specification2 = client.GetSpecificationByPhoneID(phoneID2);

            if (phone1 == null || phone2 == null || specification1 == null || specification2 == null)
            {
                lblMessage.Text = "The selected smartphones could not be compared.";
                return;
            }

            DisplayPhone1(phone1, specification1);
            DisplayPhone2(phone2, specification2);

            pnlComparison.Visible = true;
        }


        private void DisplayPhone1(PhoneCatalogue phone, PhoneSpecification specification)
        {
            lblBrand1.Text = phone.BrandName;
            lblModel1.Text = phone.ModelName;
            lblPrice1.Text = phone.StartingPrice.ToString("N2");

            lblProcessor1.Text = specification.Processor;

            lblDisplay1.Text = specification.ScreenSize.ToString().Replace(",", ".") + " inch " + specification.ScreenType;

            lblRefreshRate1.Text = specification.RefreshRate.ToString() + " Hz";

            lblBattery1.Text = specification.BatteryCapacity.ToString() + " mAh";

            lblRearCamera1.Text = specification.RearCameraMP.ToString().Replace(",", ".") + " MP";

            lblFrontCamera1.Text = specification.FrontCameraMP.ToString().Replace(",", ".") + " MP";

            lbl5G1.Text = specification.Supports5G ? "Yes" : "No";

            lblDualSIM1.Text = specification.DualSIM ? "Yes" : "No";

            lblExpandable1.Text = specification.ExpandableStorage ? "Yes" : "No";

            lblWater1.Text = specification.WaterResistance;
        }


        private void DisplayPhone2(PhoneCatalogue phone, PhoneSpecification specification)
        {
            lblBrand2.Text = phone.BrandName;
            lblModel2.Text = phone.ModelName;
            lblPrice2.Text = phone.StartingPrice.ToString("N2");

            lblProcessor2.Text = specification.Processor;

            lblDisplay2.Text =specification.ScreenSize.ToString().Replace(",", ".") + " inch " + specification.ScreenType;

            lblRefreshRate2.Text = specification.RefreshRate.ToString() + " Hz";

            lblBattery2.Text = specification.BatteryCapacity.ToString() + " mAh";

            lblRearCamera2.Text = specification.RearCameraMP.ToString().Replace(",", ".") + " MP";

            lblFrontCamera2.Text = specification.FrontCameraMP.ToString().Replace(",", ".") + " MP";

            lbl5G2.Text = specification.Supports5G ? "Yes" : "No";

            lblDualSIM2.Text = specification.DualSIM ? "Yes" : "No";

            lblExpandable2.Text = specification.ExpandableStorage ? "Yes" : "No";

            lblWater2.Text = specification.WaterResistance;
        }
    }
}
