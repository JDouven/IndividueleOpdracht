//Class for winkelreview
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tweakers
{
    public class WinkelReview : IReview
    {public int ReviewID
        {
            get;
            private set;
        }

        public string Auteur
        {
            get;
            private set;
        }

        public string Onderwerp
        {
            get;
            private set;
        }

        public string Review_tekst
        {
            get;
            private set;
        }

        public int Beoordeling
        {
            get;
            private set;
        }

        public WinkelReview(int reviewID, string auteur, string onderwerp, string review_Tekst, int beoordeling)
        {
            ReviewID = reviewID;
            Auteur = auteur;
            Onderwerp = onderwerp;
            Review_tekst = review_Tekst;
            Beoordeling = beoordeling;
        }
    }
}