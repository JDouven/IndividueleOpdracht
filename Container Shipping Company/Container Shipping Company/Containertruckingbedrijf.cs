using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container_Shipping_Company
{
    public class Containertruckingbedrijf
    {
        public string Naam
        {
            get;
            private set;
        }

        public string ContactpersoonNaam
        {
            get;
            private set;
        }

        public int KvKNummer
        {
            get;
            private set;
        }

        public Containertruckingbedrijf(string naam, string contactpersoonnaam, int kvknummer)
        {
            this.Naam = naam;
            this.ContactpersoonNaam = contactpersoonnaam;
            this.KvKNummer = kvknummer;
        }

        public override string ToString()
        {
            return Naam;
        }
    }
}
