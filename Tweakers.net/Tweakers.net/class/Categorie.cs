using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tweakers
{
    public class Categorie
    {
        public int CatID
        {
            get;
            private set;
        }

        public string Naam
        {
            get;
            private set;
        }

        public List<Product> Producten
        {
            get;
            private set;
        }

        public int ParentCatID
        {
            get;
            private set;
        }

        public Categorie(int catid, string naam, List<Product> producten, int parentid)
        {
            CatID = catid;
            Naam = naam;
            Producten = producten;
            ParentCatID = parentid;
        }
    }
}