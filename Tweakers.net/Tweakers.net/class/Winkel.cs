//Class for winkel object
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tweakers
{
    public class Winkel
    {
        public string Naam
        {
            get;
            private set;
        }
        public string Locatie
        {
            get;
            private set;
        }
        public string Awards
        {
            get;
            private set;
        }
        public List<WinkelReview> Reviews
        {
            get;
            private set;
        }

        public Winkel(string naam, string locatie, string awards, List<WinkelReview> reviews)
        {
            Naam = naam;
            Locatie = locatie;
            Awards = awards;
            Reviews = reviews;
        }
    }
}