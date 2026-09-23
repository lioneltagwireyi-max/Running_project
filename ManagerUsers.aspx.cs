using System;
using System.Linq;
using PhoneFit.BackendServiceReference;

namespace PhoneFit
{
    public partial class ManagerUsers : System.Web.UI.Page
    {
        Service1Client managerclient = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            // only Managers may access User Management
            if (Session["RoleName"] == null || Session["RoleName"].ToString() != "Manager")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                DisplayUsers();
            }
        }


        private void DisplayUsers()
        {
            // get all registered users from the wcf service
            UserInfo[] users = managerclient.GetUsers();

            // no users were found
            if (users == null || users.Length == 0)
            {
                pnlNoUsers.Visible = true;
                pnlUsers.Visible = false;

                lblTotalUsers.Text = "0";
                lblActiveUsers.Text = "0";
                lblInactiveUsers.Text = "0";

                return;
            }

            pnlNoUsers.Visible = false;
            pnlUsers.Visible = true;

            // display the registered users
            rptUsers.DataSource = users;
            rptUsers.DataBind();


            int activeUsers = 0;
            int inactiveUsers = 0;

            // count active and inactive accounts
            foreach (UserInfo user in users)
            {
                if (user.UserIsActive)
                {
                    activeUsers++;
                }
                else
                {
                    inactiveUsers++;
                }
            }


            // display the summary values
            lblTotalUsers.Text = users.Length.ToString();

            lblActiveUsers.Text = activeUsers.ToString();

            lblInactiveUsers.Text = inactiveUsers.ToString();
        }


        protected string DisplayPhoneNumber(object phoneNumber)
        {
            if (phoneNumber == null || string.IsNullOrWhiteSpace(phoneNumber.ToString()))
            {
                return "Not provided";
            }

            return phoneNumber.ToString();
        }


        protected string GetStatusText(object isActive)
        {
            bool active = Convert.ToBoolean(isActive);

            if (active)
            {
                return "Active";
            }

            return "Inactive";
        }


        protected string GetStatusClass(object isActive)
        {
            bool active = Convert.ToBoolean(isActive);

            if (active)
            {
                return "status-active";
            }

            return "status-inactive";
        }
    }
}