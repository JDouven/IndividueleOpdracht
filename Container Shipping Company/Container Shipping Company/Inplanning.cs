using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container_Shipping_Company
{
    /// <summary>
    /// Maakt een inplanning op basis van een gegeven schip en bestemming
    /// </summary>
    public class Inplanning
    {
        /// <summary>
        /// Bestemming van de inplanning
        /// </summary>
        public Bestemming PBestemming
        {
            get;
            private set;
        }

        /// <summary>
        /// Het schip waarop de planning gebaseerd is
        /// </summary>
        public Schip PSchip
        {
            get;
            private set;
        }

        /// <summary>
        /// Een 3D array met de ingedeelde containers
        /// </summary>
        public Container[, ,] SchipLading
        {
            get;
            private set;
        }

        /// <summary>
        /// Het gewicht op de linkerhelft van het schip
        /// </summary>
        public int GewichtLinks
        {
            get;
            private set;
        }

        /// <summary>
        /// Het gewicht op de rechterhelft van het schip
        /// </summary>
        public int GewichtRechts
        {
            get;
            private set;
        }

        /// <summary>
        /// Het maximum verticaal gewicht op een container
        /// </summary>
        public int MaxVerticaalGewicht
        {
            get;
            private set;
        }

        private Beheer beheer = new Beheer();

        /// <summary>
        /// Constructor voor Inplanning
        /// </summary>
        /// <param name="bestemming">Bestemming voor deze inplanning</param>
        /// <param name="schip">Schip om op in te plannen</param>
        public Inplanning(Bestemming bestemming, Schip schip)
        {
            this.PBestemming = bestemming;
            this.PSchip = schip;

            DeelIn();
        }

        /// <summary>
        /// Deelt containers naar de bestemming in op basis van type en gewicht.
        /// </summary>
        private void DeelIn()
        {
            //3D array welke de laadruimte van het schip voorstelt
            SchipLading = new Container[PSchip.Hoogte, PSchip.Rijen, PSchip.ContainersPerRij];

            //Alle containers voor deze bestemming gesorteerd op soort
            List<Container> waardevolle = new List<Container>();
            int waardeCounter = 0;
            List<Container> gekoelde = new List<Container>();
            int koelCounter = 0;
            List<Container> normale = new List<Container>();
            int normaalCounter = 0;
            foreach (Container c in beheer.Containers)
            {
                if (c.Type == ContainerType.V && c.CBestemming.Naam == PBestemming.Naam)
                {
                    waardevolle.Add(c);
                }
                else if (c.Type == ContainerType.C && c.CBestemming.Naam == PBestemming.Naam)
                {
                    gekoelde.Add(c);
                }
                else if (c.Type == ContainerType.N && c.CBestemming.Naam == PBestemming.Naam)
                {
                    normale.Add(c);
                }
            }

            int stroomPlekken = PSchip.StroomAansluitingen;
            int maxWaardevol = (PSchip.Hoogte - 1) * (PSchip.Rijen - 2) * (PSchip.ContainersPerRij - 2);

            //STAP 1
            //Gekoelde containers indelen.
            for (int hoogte = 0; hoogte < PSchip.Hoogte; hoogte++)
            {
                for (int breedte = 0; breedte < PSchip.Rijen; breedte++)
                {
                    if (stroomPlekken > 0)
                    {
                        if (koelCounter < gekoelde.Count)
                        {
                            SchipLading[hoogte, breedte, 0] = gekoelde[koelCounter];
                            stroomPlekken--; koelCounter++;
                        }
                    }
                }
            }

            //STAP 2
            //Waardevolle containers indelen

            //Bovenste laag niet meetellen, normale containers moeten boven op waardevolle staan.
            for (int hoogte = 0; hoogte < PSchip.Hoogte - 1; hoogte++)
            {
                //De buitenste rijen(links naar rechts) overslaan
                for (int diepte = 1; diepte < PSchip.ContainersPerRij - 1; diepte++)
                {
                    for (int breedte = 1; breedte < PSchip.Rijen - 1; breedte++)
                    {
                        //Check verticaal gewicht
                        int gewicht = GetGewicht(hoogte, diepte, breedte);

                        //Zorgen dat er genoeg normale containers zijn om bovenop de waardevolle te plaatsen. --- 
                        //Dubbelcheck om te zorgen dat waardevolle containers niet op verkeerde plaatsen worden ingedeeld.
                        if ((normale.Count - normaalCounter) > (waardevolle.Count - waardeCounter) && (maxWaardevol > 0) && gewicht < 120000)
                        {
                            if (SchipLading[hoogte, breedte, diepte] == null && waardeCounter < waardevolle.Count && normaalCounter < normale.Count)
                            {
                                SchipLading[hoogte, breedte, diepte] = waardevolle[waardeCounter];
                                waardeCounter++; maxWaardevol--;
                            }
                        }
                    }
                }
            }

            //STAP 3
            //Normale containers inladen bovenop waardevolle

            for (int hoogte = 0; hoogte < PSchip.Hoogte; hoogte++)
            {
                for (int diepte = 0; diepte < PSchip.ContainersPerRij; diepte++)
                {
                    for (int breedte = 0; breedte < PSchip.Rijen; breedte++)
                    {
                        //Check verticaal gewicht
                        int gewicht = GetGewicht(hoogte, diepte, breedte);

                        //Controleren of er normale containers over zijn
                        if (SchipLading[hoogte, breedte, diepte] != null && normaalCounter < normale.Count && gewicht < 120000)
                        {
                            if (SchipLading[hoogte, breedte, diepte].Type == ContainerType.V)
                            {
                                int bovenste = hoogte;
                                while (SchipLading[bovenste, breedte, diepte] != null && bovenste < PSchip.Hoogte - 1)
                                {
                                    bovenste++;
                                }
                                SchipLading[bovenste, breedte, diepte] = normale[normaalCounter];
                                normaalCounter++;
                            }
                        }
                    }
                }
            }

            //STAP 4
            //De rest van de normale containers inladen

            for (int hoogte = 0; hoogte < PSchip.Hoogte; hoogte++)
            {
                for (int diepte = 0; diepte < PSchip.ContainersPerRij; diepte++)
                {
                    for (int breedte = PSchip.Rijen - 1; breedte >= 0; breedte--)
                    {
                        //Check verticaal gewicht
                        int gewicht = GetGewicht(hoogte, diepte, breedte);

                        if (normaalCounter < normale.Count && SchipLading[hoogte, breedte, diepte] == null && gewicht < 120000)
                        {
                            SchipLading[hoogte, breedte, diepte] = normale[normaalCounter];
                            normaalCounter++;
                        }
                    }
                }
            }

            //STAP 5
            //Controleer balans
            for (int diepte = 0; diepte < PSchip.ContainersPerRij; diepte++)
            {
                for (int hoogte = 0; hoogte < PSchip.Hoogte; hoogte++)
                {
                    for (int breedte = 0; breedte < PSchip.Rijen; breedte++)
                    {
                        if (SchipLading[hoogte, breedte, diepte] != null)
                        {
                            if (IsOneven(PSchip.Rijen))
                            {
                                if (breedte < PSchip.Rijen / 2)
                                {
                                    GewichtLinks += SchipLading[hoogte, breedte, diepte].Gewicht;
                                }
                                else if (breedte > PSchip.Rijen / 2)
                                {
                                    GewichtRechts += SchipLading[hoogte, breedte, diepte].Gewicht;
                                }
                            }
                            else
                            {
                                if (breedte <= PSchip.Rijen / 2)
                                {
                                    GewichtLinks += SchipLading[hoogte, breedte, diepte].Gewicht;
                                }
                                else
                                {
                                    GewichtRechts += SchipLading[hoogte, breedte, diepte].Gewicht;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Telt het verticale gewicht op vanaf een gegeven positie op het schip
        /// </summary>
        /// <param name="hoogte">hoogte coordinaat</param>
        /// <param name="diepte">diepte coordinaat</param>
        /// <param name="breedte">breedte coordinaat</param>
        /// <returns>De som ven het gewicht van de containers boven de gegeven container</returns>
        private int GetGewicht(int hoogte, int diepte, int breedte)
        {
            int gewicht = 0;
            if (SchipLading[hoogte, breedte, diepte] != null)
            {
                int omhoog = hoogte;
                while (omhoog < PSchip.Hoogte)
                {
                    if (SchipLading[omhoog, breedte, diepte] != null)
                    {
                        gewicht += SchipLading[omhoog, breedte, diepte].Gewicht;
                        if (gewicht > MaxVerticaalGewicht)
                            MaxVerticaalGewicht = gewicht;
                    }
                    omhoog++;
                }
            }
            return gewicht;
        }

        /// <summary>
        /// True/false ofwel het getal oneven is.
        /// </summary>
        /// <param name="getal"></param>
        /// <returns></returns>
        private bool IsOneven(int getal)
        {
            return getal % 2 != 0;
        }
    }
}
