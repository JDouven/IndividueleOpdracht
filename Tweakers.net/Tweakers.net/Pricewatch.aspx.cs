//Code behind Pricewatch.aspx
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Tweakers
{
    public partial class Pricewatch : System.Web.UI.Page
    {
        private DBManager dbmngr = new DBManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                LoginOrOut.Text = "Logout";
                LoginOrOut.CssClass = "btn btn-lg btn-danger logout";
            }
            DataTable dt = dbmngr.GetAlleCategorien();
            DataRow dr = dt.NewRow();
            dr["id"] = 0;
            dr["Naam"] = "Alle producten";
            dt.Rows.Add(dr);
            ItemTable.DataSource = dt;
            ItemTable.DataBind();
        }

        protected void LoginOrOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void ItemTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ItemTable.SelectedRow;
            string hue = row.Cells.ToString();
        }
    }
}