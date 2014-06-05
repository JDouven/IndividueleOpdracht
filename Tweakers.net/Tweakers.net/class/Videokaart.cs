//Class for videokaart object
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tweakers
{
    public class Videokaart : Product
    {
        public Videokaart(string naam, string merk, string afbeelding, List<Prijs> prijzen, List<ProductReview> reviews)
            : base(naam, merk, afbeelding, prijzen, reviews)
        {

        }
    }
}