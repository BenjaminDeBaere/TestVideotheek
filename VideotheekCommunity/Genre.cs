using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideotheekCommunity
{
    public class Genre
    {
        private int genreNrValue;

        public int GenreNr
        {
            get { return genreNrValue; }
            set { genreNrValue = value; }
        }

        private string genreNaamValue;

        public string GenreNaam
        {
            get { return genreNaamValue; }
            set { genreNaamValue = value; }
        }

        public Genre(int genrenr, string genrenaam)
        {
            this.GenreNr = genrenr;
            this.GenreNaam = genrenaam;
        }

        public Genre() { }

    }
}
