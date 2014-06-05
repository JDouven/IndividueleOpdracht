//Code behind Login.aspx
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Configuration;
using System.Configuration;

namespace Tweakers
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                LoginOrOut.Text = "Logout";
                LoginOrOut.CssClass = "btn btn-lg btn-danger logout";
                FormsAuthentication.SignOut();
                Response.Redirect(FormsAuthentication.DefaultUrl);
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                if (this.tb_rfid.Text == "user" && this.tb_pw.Text == "password")
                {
                    FormsAuthentication.RedirectFromLoginPage(this.tb_rfid.Text, this.cb_remember.Checked);
                }
                else
                {
                    this.InvalidLogin.Visible = true;
                }
            }
        }

        protected void LoginOrOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}