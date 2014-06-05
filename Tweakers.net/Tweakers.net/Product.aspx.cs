using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tweakers
{
    public partial class Product1 : System.Web.UI.Page
    {
        private DBManager dbmngr = new DBManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                LoginOrOut.Text = "Logout";
                LoginOrOut.CssClass = "btn btn-lg btn-danger logout";
            }
            string PNaam = "";
            //try
            //{
                PNaam = Convert.ToString(Request.QueryString["p"]);
            //}
            //catch { Response.Redirect("Default.aspx"); }
            PNaam.Replace("%20", " ");
            ItemTable.DataSource = dbmngr.GetProductInfo(PNaam);
            ItemTable.DataBind();
        }

        protected void LoginOrOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}