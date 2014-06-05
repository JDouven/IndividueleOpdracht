using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Tweakers
{
    public partial class Product1 : System.Web.UI.Page
    {
        private DBManager dbmngr = new DBManager();
        private Product product;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                LoginOrOut.Text = "Logout";
                LoginOrOut.CssClass = "btn btn-lg btn-danger logout";
            }
            string PNaam = "";
            try
            {
                PNaam = Convert.ToString(Request.QueryString["p"]);
            }
            catch { Response.Redirect("Default.aspx"); }
            PNaam.Replace("%20", " ");
            DataTable dt = dbmngr.GetProductInfo(PNaam);
            DataTable dt2 = new DataTable();
            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                dt2.Columns.Add();
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt2.Rows.Add();
                dt2.Rows[i][0] = dt.Columns[i].ColumnName;
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dt2.Rows[i][j + 1] = dt.Rows[j][i];
                }
            }
            ItemTable.DataSource = dt2;
            ItemTable.ShowHeader = false;
            ItemTable.DataBind();

            product = dbmngr.GetProduct(PNaam);
            TitleProd.Text = product.Naam;

            PricesTable.DataSource = product.Prijzen;
            PricesTable.DataBind();

            ReviewTable.DataSource = product.Reviews;
            ReviewTable.DataBind();
        }

        protected void LoginOrOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}