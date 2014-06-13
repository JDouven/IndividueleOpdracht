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
        private string PNaam;
        protected void Page_Load(object sender, EventArgs e)
        {
            PNaam = "";
            try
            {
                PNaam = Convert.ToString(Request.QueryString["p"]);
            }
            catch { Response.Redirect("Default.aspx"); }
            UpdateGridViews();
        }

        private void UpdateGridViews()
        {
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

        protected void Submit_Click(object sender, EventArgs e)
        {
            InvalidEntry.Visible = false;
            if (Request.IsAuthenticated)
            {
                try
                {
                    if (Convert.ToInt32(tb_beoordeling.Text) > 5 || Convert.ToInt32(tb_beoordeling.Text) < 0)
                    {
                        InvalidEntry.Visible = true;
                        InvalidEntryText.Text = "Beoordeling must be between 0 and 5 (inclusive)";
                    }
                    else
                    {
                        dbmngr.AddProductReview((string)Session["UserName"], tb_onderwerp.Text, tb_tekst.Text, Convert.ToInt32(tb_beoordeling.Text), PNaam);
                    }
                }
                catch
                { }
                UpdateGridViews();
            }
            else
            {
                Response.Redirect("Login.aspx?ReturnUrl=" + Request.Url);
            }
        }
    }
}