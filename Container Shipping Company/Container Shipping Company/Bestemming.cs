using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container_Shipping_Company
{
    public class Bestemming
    {
        public string Naam
        {
            get;
            private set;
        }

        public string Land
        {
            get;
            private set;
        }

        public Bestemming(string naam, string land)
        {
            this.Naam = naam;
            this.Land = land;
        }

        public override string ToString()
        {
            return Naam + ", " + Land;
        }
    }
}
