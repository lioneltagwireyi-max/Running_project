using System;
using PhoneFit.BackendServiceReference;

namespace PhoneFit
{
    public partial class Home : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayFeaturedPhones();
            }
        }

        private void DisplayFeaturedPhones()
        {
            // get the active phones from WCF.
            PhoneCatalogue[] phones = client.GetActivePhones();

            if (phones == null || phones.Length == 0)
            {
                pnlNoPhones.Visible = true;
                rptFeaturedPhones.Visible = false;
                return;
            }

            pnlNoPhones.Visible = false;
            rptFeaturedPhones.Visible = true;

            // display 5 phones
            int phoneCount = 5;

            if (phones.Length < 5)
            {
                phoneCount = phones.Length;
            }

            PhoneCatalogue[] featuredPhones = new PhoneCatalogue[phoneCount];

            for (int i = 0; i < phoneCount; i++)
            {
                featuredPhones[i] = phones[i];
            }

            rptFeaturedPhones.DataSource = featuredPhones;
            rptFeaturedPhones.DataBind();
        }
    }
}