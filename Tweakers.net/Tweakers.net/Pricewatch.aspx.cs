//Code behind Pricewatch.aspx
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tweakers
{
    public partial class Pricewatch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                LoginOrOut.Text = "Logout";
                LoginOrOut.CssClass = "btn btn-lg btn-danger logout";
            }

        }

        protected void LoginOrOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}