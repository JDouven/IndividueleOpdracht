﻿//Code behind Categorie.aspx
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tweakers
{
    public partial class Categorie1 : System.Web.UI.Page
    {
        private DBManager dbmngr = new DBManager();
        private Categorie cat;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                LoginOrOut.Text = "Logout";
                LoginOrOut.CssClass = "btn btn-lg btn-danger logout";
            }
            int link = -1;
            try
            {
                link = Convert.ToInt32(Request.QueryString["c"]);
                
            }
            catch { Response.Redirect("Pricewatch.aspx"); }
            cat = dbmngr.GetCategorie(link);
            TitleCat.Text = cat.Naam;
            ItemTable.DataSource = dbmngr.GetCatProducts(link);
            ItemTable.DataBind();
        }

        protected void LoginOrOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}