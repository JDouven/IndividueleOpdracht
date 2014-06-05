//Class for prijs object
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tweakers
{
    public class Prijs
    {
        public string Product
        {
            get;
            private set;
        }

        public string Winkel
        {
            get;
            private set;
        }

        public double PrijsDouble
        {
            get;
            private set;
        }

        public Prijs(string product, string winkel, double prijs)
        {
            Product = product;
            Winkel = winkel;
            PrijsDouble = prijs;
        }
    }
}