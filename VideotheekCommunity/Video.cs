using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideotheekCommunity
{
    public class Video
    {
        private int bandNrValue;

        public int BandNr
        {
            get { return bandNrValue; }
        }

        private String titelValue;

        public String Titel
        {
            get { return titelValue; }
            set
            {
                titelValue = value;
                this.Changed = true;
            }
        }

        private int genreNrValue;

        public int GenreNr
        {
            get { return genreNrValue; }
            set
            {
                genreNrValue = value;
                this.Changed = true;
            }
        }

        private int inVoorraadValue;

        public int InVoorraad
        {
            get { return inVoorraadValue; }
            set
            {
                inVoorraadValue = value;
                this.Changed = true;
            }
        }

        private int uitVoorraadValue;

        public int UitVoorraad
        {
            get { return uitVoorraadValue; }
            set
            {
                uitVoorraadValue = value;
                this.Changed = true;
            }
        }

        private Decimal prijsValue;

        public Decimal Prijs
        {
            get { return prijsValue; }
            set
            {
                prijsValue = value;
                this.Changed = true;
            }
        }

        private int totaalverhuurdValue;

        public int TotaalVerhuurd
        {
            get { return totaalverhuurdValue; }
            set
            {
                totaalverhuurdValue = value;
                this.Changed = true;
            }
        }

        public bool Changed { get; set; }
        public Video(int bandnr, string titel, int genrenr, int invoorraad, int uitvoorraad, decimal prijs, int totaalverhuurd)
        {
            bandNrValue = bandnr;
            this.Titel = titel;
            this.GenreNr = genrenr;
            this.InVoorraad = invoorraad;
            this.UitVoorraad = uitvoorraad;
            this.Prijs = prijs;
            this.TotaalVerhuurd = totaalverhuurd;
            this.Changed = false;
        }
        public Video() { }
    }
}
