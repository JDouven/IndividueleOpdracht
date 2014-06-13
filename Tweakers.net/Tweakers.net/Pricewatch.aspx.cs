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
            DataTable dt = dbmngr.GetAlleCategorien();
            DataRow dr = dt.NewRow();
            dr["id"] = 0;
            dr["Naam"] = "Alle producten";
            dt.Rows.Add(dr);
            ItemTable.DataSource = dt;
            ItemTable.DataBind();
        }
    }
}