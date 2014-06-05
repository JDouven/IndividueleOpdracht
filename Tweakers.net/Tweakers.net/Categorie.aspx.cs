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
            cat = dbmngr.GetCategorie(Request.QueryString["c"]);
        }

        protected void LoginOrOut_Click(object sender, EventArgs e)
        {

        }
    }
}