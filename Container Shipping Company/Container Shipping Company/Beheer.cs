using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container_Shipping_Company
{
    public class Beheer
    {
        public List<Container> Containers
        {
            get;
            private set;
        }

        public List<Bestemming> Bestemmingen
        {
            get;
            private set;
        }

        public List<Schip> Schepen
        {
            get;
            private set;
        }

        public List<Containertruckingbedrijf> Bedrijven
        {
            get;
            private set;
        }

        private DatabaseManager database = new DatabaseManager();

        public Beheer()
        {
            Containers = database.GetContainers();
            Bestemmingen = database.GetBestemmingen();
            Schepen = database.GetSchepen();
            Bedrijven = database.GetContainertruckingbedrijven();
        }

        public void Refresh()
        {

        }
    }
}
