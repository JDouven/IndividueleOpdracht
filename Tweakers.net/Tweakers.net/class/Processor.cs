//Class for processor object
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tweakers
{
    public class Processor : Product
    {
        public Processor(string naam, string merk, string afbeelding, List<Prijs> prijzen, List<ProductReview> reviews)
            : base(naam, merk, afbeelding, prijzen, reviews)
        {

        }
    }
}