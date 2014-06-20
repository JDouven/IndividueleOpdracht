using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container_Shipping_Company
{
    public enum ContainerType
    {
        N, V, C
    }

    public class Container
    {
        public int ID
        {
            get;
            private set;
        }

        public string Containertruckingbedrijf
        {
            get;
            private set;
        }

        public Bestemming CBestemming
        {
            get;
            private set;
        }

        public int Gewicht
        {
            get;
            private set;
        }

        public ContainerType Type
        {
            get;
            private set;
        }

        public bool Ingeplanned
        {
            get;
            set;
        }

        public Container(int id, string bedrijf, Bestemming bestemming, int gewicht, ContainerType type, bool ingeplanned)
        {
            this.ID = id;
            this.Containertruckingbedrijf = bedrijf;
            this.CBestemming = bestemming;
            this.Gewicht = gewicht;
            this.Type = type;
            this.Ingeplanned = ingeplanned;
        }

        public override string ToString()
        {
            return ID.ToString() + ", " + 
                Containertruckingbedrijf + ", " + 
                CBestemming.ToString() + ", " + 
                Type.ToString() + ", " + 
                Ingeplanned.ToString();
        }
    }
}
