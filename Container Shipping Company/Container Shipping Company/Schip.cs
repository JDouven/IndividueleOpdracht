using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container_Shipping_Company
{
    public class Schip
    {
        public string Type
        {
            get;
            private set;
        }

        public int Hoogte
        {
            get;
            private set;
        }

        public int Rijen
        {
            get;
            private set;
        }

        public int ContainersPerRij
        {
            get;
            private set;
        }

        public int StroomAansluitingen
        {
            get;
            private set;
        }

        public Schip(string type,
            int hoogte,
            int rijen,
            int containersperrij,
            int stroomaansluitingen)
        {
            this.Type = type;
            this.Hoogte = hoogte;
            this.Rijen = rijen;
            this.ContainersPerRij = containersperrij;
            this.StroomAansluitingen = stroomaansluitingen;
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
