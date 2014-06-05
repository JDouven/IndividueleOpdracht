//Class for abstract product object
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tweakers
{
    public class Product
    {
        public string Naam
        {
            get;
            private set;
        }

        public string Merk
        {
            get;
            private set;
        }

        public string Afbeelding
        {
            get;
            private set;
        }

        public List<Prijs> Prijzen
        {
            get;
            private set;
        }

        public List<ProductReview> Reviews
        {
            get;
            private set;
        }

        public Product(string naam, string merk, string afbeelding, List<Prijs> prijzen, List<ProductReview> reviews)
        {
            Naam = naam;
            Merk = merk;
            Afbeelding = afbeelding;
            Prijzen = prijzen;
            Reviews = reviews;
        }

    }
}