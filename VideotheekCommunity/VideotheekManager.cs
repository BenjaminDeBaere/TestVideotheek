using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;

namespace VideotheekCommunity
{
    public class VideotheekManager
    {
        public List<Video> selectVideos()
        {
            List<Video> Videos = new List<Video>();
            var manager = new VideotheekDbManager();
            using (var conVideo = manager.GetConnection())
            {
                using (var comSelect = conVideo.CreateCommand())
                {
                    comSelect.CommandType = CommandType.Text;
                    comSelect.CommandText = "select * from films";

                    conVideo.Open();
                    using (var rdrVideo = comSelect.ExecuteReader())
                    {
                        Int32 BandNrPos = rdrVideo.GetOrdinal("BandNr");
                        Int32 TitelPos = rdrVideo.GetOrdinal("Titel");
                        Int32 GenreNrPos = rdrVideo.GetOrdinal("GenreNr");
                        Int32 InVoorraadPos = rdrVideo.GetOrdinal("InVoorraad");
                        Int32 UitvoorraadPos = rdrVideo.GetOrdinal("UitVoorraad");
                        Int32 PrijsPos = rdrVideo.GetOrdinal("Prijs");
                        Int32 TotaalVerhuurdPos = rdrVideo.GetOrdinal("TotaalVerhuurd");

                        while(rdrVideo.Read())
                        {
                            Videos.Add(new Video(
                                rdrVideo.GetInt32(BandNrPos),
                                rdrVideo.GetString(TitelPos),
                                rdrVideo.GetInt32(GenreNrPos),
                                rdrVideo.GetInt32(InVoorraadPos),
                                rdrVideo.GetInt32(UitvoorraadPos),
                                rdrVideo.GetDecimal(PrijsPos),
                                rdrVideo.GetInt32(TotaalVerhuurdPos)));
                        }
                    }

                }
            }
            return Videos;
        }

        public List<Genre> GetGenres()
        {
            List<Genre> Genres = new List<Genre>();
            var manager = new VideotheekDbManager();
            using (var conVideo = manager.GetConnection())
            {
                using (var comSelectGenres = conVideo.CreateCommand())
                {
                  
                        comSelectGenres.CommandType = CommandType.Text;
                
            
                        comSelectGenres.CommandText = "Select * from genres";
                                      
            

                    conVideo.Open();
                    using (var rdrGenres = comSelectGenres.ExecuteReader())
                    {
                        var genreNaamPos = rdrGenres.GetOrdinal("genre");
                        var genreNrPos = rdrGenres.GetOrdinal("genreNr");
                        while(rdrGenres.Read())
                        {
                            Genres.Add(new Genre(
                                rdrGenres.GetInt32(genreNrPos),
                                rdrGenres.GetString(genreNaamPos)));
                        }
                    }
                   


                
                }
            }
            return Genres;
        }
        public void VideoToevoegen(string titel, int genrenr, int invoorraad, int uitvoorraad, decimal prijs, int totaalverhuurd)
        {
            var manager = new VideotheekDbManager();
            using (var conVideo = manager.GetConnection())
            {
                using (var comInsertNieuweVideo = conVideo.CreateCommand())
                {
                    comInsertNieuweVideo.CommandType = CommandType.Text;
                    comInsertNieuweVideo.CommandText = 
                        "Insert into films ( Titel, GenreNr, InVoorraad, UitVoorraad, Prijs, TotaalVerhuurd) values ( @titel, @genreNr, @inVoorraad, @uitVoorraad, @prijs, @totaalVerhuurd )";                    

                    var parTitel = comInsertNieuweVideo.CreateParameter();
                    parTitel.ParameterName = "@titel";
                    parTitel.Value = titel;
                    comInsertNieuweVideo.Parameters.Add(parTitel);

                    var parGenreNr = comInsertNieuweVideo.CreateParameter();
                    parGenreNr.ParameterName = "@genreNr";
                    parGenreNr.Value = genrenr;
                    comInsertNieuweVideo.Parameters.Add(parGenreNr);

                    var parInVoorraad = comInsertNieuweVideo.CreateParameter();
                    parInVoorraad.ParameterName = "@inVoorraad";
                    parInVoorraad.Value = invoorraad;
                    comInsertNieuweVideo.Parameters.Add(parInVoorraad);

                    var parUitVoorrad = comInsertNieuweVideo.CreateParameter();
                    parUitVoorrad.ParameterName = "@uitVoorraad";
                    parUitVoorrad.Value = uitvoorraad;
                    comInsertNieuweVideo.Parameters.Add(parUitVoorrad);

                    var parPrijs = comInsertNieuweVideo.CreateParameter();
                    parPrijs.ParameterName = "@prijs";
                    parPrijs.Value = prijs;
                    comInsertNieuweVideo.Parameters.Add(parPrijs);

                    var parTotaalVerhuurd = comInsertNieuweVideo.CreateParameter();
                    parTotaalVerhuurd.ParameterName = "@totaalVerhuurd";
                    parTotaalVerhuurd.Value = totaalverhuurd;
                    comInsertNieuweVideo.Parameters.Add(parTotaalVerhuurd);

                    conVideo.Open();
                    if(comInsertNieuweVideo.ExecuteNonQuery()==0)
                    {
                        throw new Exception("Kan de video niet toevoegen.");
                    }
                    

                }
            }
        }
        public void VideoVerwijderen(int bandNr)
        {
            var manager = new VideotheekDbManager();
            using (var conVideo = manager.GetConnection())
            {
                using (var comDelete = conVideo.CreateCommand())
                {
                    comDelete.CommandType = CommandType.Text;
                    comDelete.CommandText = "delete from films where bandnr = @bandnr";

                    var parBandNr = comDelete.CreateParameter();
                    parBandNr.ParameterName = "@bandnr";
                    parBandNr.Value = bandNr;
                    comDelete.Parameters.Add(parBandNr);

                    conVideo.Open();

                    comDelete.ExecuteNonQuery();


                }
            }
        }

        public List<Video> VeranderdeVideosOpslaan(List<Video> videos)
        {
            List<Video> NietVeranderdeVideos = new List<Video>();
            var manager = new VideotheekDbManager();
            using (var conVideo = manager.GetConnection())
            {
                using (var comUpdate = conVideo.CreateCommand())
                {
                    comUpdate.CommandType = CommandType.Text;
                    comUpdate.CommandText = "update films set Titel = @titel, GenreNr = @genreNr, InVoorraad = @inVoorraad, UitVoorraad = @uitVoorraad, Prijs = @prijs, TotaalVerhuurd= @totaalVerhuurd where bandNr = @bandNr";

                    var parBandNr = comUpdate.CreateParameter();
                    parBandNr.ParameterName = "@bandNr";
                    comUpdate.Parameters.Add(parBandNr);

                    var parTitel = comUpdate.CreateParameter();
                    parTitel.ParameterName = "@titel";
                    comUpdate.Parameters.Add(parTitel);

                    var parGenreNr = comUpdate.CreateParameter();
                    parGenreNr.ParameterName = "@genreNr";
                    comUpdate.Parameters.Add(parGenreNr);

                    var parInVoorraad = comUpdate.CreateParameter();
                    parInVoorraad.ParameterName = "@inVoorraad";
                    comUpdate.Parameters.Add(parInVoorraad);

                    var parUitVoorraad = comUpdate.CreateParameter();
                    parUitVoorraad.ParameterName = "@uitVoorraad";
                    comUpdate.Parameters.Add(parUitVoorraad);

                    var parPrijs = comUpdate.CreateParameter();
                    parPrijs.ParameterName = "@prijs";
                    comUpdate.Parameters.Add(parPrijs);

                    var parTotaalVerhuurd = comUpdate.CreateParameter();
                    parTotaalVerhuurd.ParameterName = "@totaalVerhuurd";
                    comUpdate.Parameters.Add(parTotaalVerhuurd);

                    conVideo.Open();

                    foreach(Video video in videos)
                    {
                        try
                        {
                            parBandNr.Value = video.BandNr;
                            parTitel.Value = video.Titel;
                            parGenreNr.Value = video.GenreNr;
                            parInVoorraad.Value = video.InVoorraad;
                            parUitVoorraad.Value = video.UitVoorraad;
                            parPrijs.Value = video.Prijs;
                            parTotaalVerhuurd.Value = video.TotaalVerhuurd;

                            comUpdate.ExecuteNonQuery();
                        }
                        catch(Exception)
                        {
                            NietVeranderdeVideos.Add(video);
                        }
                    }
                }
            }
            return NietVeranderdeVideos;
        }

        
    }
}
