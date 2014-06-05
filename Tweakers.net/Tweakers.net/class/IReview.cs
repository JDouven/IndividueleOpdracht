//Interface for reviews
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweakers
{
    public interface IReview
    {
        int ReviewID
        {
            get;
        }

        string Auteur
        {
            get;
        }

        string Onderwerp
        {
            get;
        }

        string Review_tekst
        {
            get;
        }

        int Beoordeling
        {
            get;
        }
    }
}
