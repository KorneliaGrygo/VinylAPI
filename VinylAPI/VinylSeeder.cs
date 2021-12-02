using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylAPI.Data;
using VinylAPI.Entities;

namespace VinylAPI
{
    public class VinylSeeder
    {
        private readonly VinylAPIDbContext _dbContext;

        public VinylSeeder(VinylAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Bands.Any())
                {
                    var bands = GetBands();
                    _dbContext.Bands.AddRange(bands);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Band> GetBands()
        {
            var bands = new List<Band>()
            {
                new Band()
                {
                    Name = "Queen",
                    Description = "Brytyjski zespół rockowy.",
                    Albums = new List<MusicAlbum>()
                    {
                        new MusicAlbum()
                        {
                            Name = "Queen",
                            AmountOfSongs = 10,
                            PublicationYear = 1973,
                            Genre = "rock",
                            Songs = new List<Song>()
                            {
                                new Song()
                                {
                                    Name = "Keep Yourself Alive",
                                    Lenght = 3.48,
                                },
                                new Song()
                                {
                                    Name = "Doing All Right",
                                    Lenght = 4.10,
                                },
                                new Song()
                                {
                                    Name = "Great King Rat",
                                    Lenght = 5.43,
                                },
                                new Song()
                                {
                                    Name = "My Fairy King",
                                    Lenght = 4.08,
                                },
                                new Song()
                                {
                                    Name = "Liar",
                                    Lenght = 6.25,
                                },
                                new Song()
                                {
                                    Name = "The Night Comes Down",
                                    Lenght = 4.23,
                                },
                                new Song()
                                {
                                    Name = "Modern Times Rock ’n’ Roll",
                                    Lenght = 1.48,
                                },
                                new Song()
                                {
                                    Name = "Son and Daughter",
                                    Lenght = 3.20,
                                },
                                new Song()
                                {
                                    Name = "Jesus",
                                    Lenght = 3.44,
                                },
                                new Song()
                                {
                                    Name = "Seven Seas of Rhye",
                                    Lenght = 1.15,
                                },
                            }
                        }
                    }

                }
            };

            return bands;
        }
    }
}
