using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
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

                if(!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
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

                },
                new Band()
                {
                    Name = "ACDC",
                    Description = " australijski zespół hardrockowy założony w Sydney w 1973 roku przez braci Angusa i Malcolma Youngów. Zespół jest uznawany m.in. za pioniera muzyki hardrockowej[3]. Mimo tego, członkowie zespołu zawsze klasyfikowali swoją muzykę jako rock & roll.",
                    Albums = new List<MusicAlbum>()
                    {
                        new MusicAlbum()
                        {
                            Name = "Highway to Hell",
                            AmountOfSongs = 8,
                            Genre = "rock",
                            PublicationYear = 1979,
                            Songs = new List<Song>()
                            {
                                new Song()
                                {
                                    Name = "Highway to Hell",
                                    Lenght = 3.28,
                                },
                                new Song()
                                {
                                    Name = "Girls Got Rhythm",
                                    Lenght = 3.23
                                },
                                new Song()
                                {
                                    Name = "Girdasls Got Rhythm",
                                    Lenght = 3.3
                                },
                                new Song()
                                {
                                    Name = "Walk All Over You",
                                    Lenght = 5.10
                                },

                            }

                        }
                        ,new MusicAlbum()
                        {
                            Name = "Back in Black",
                            AmountOfSongs = 10,
                            Genre = "rock",
                            PublicationYear = 1980,
                            Songs = new List<Song>()
                            {
                                new Song()
                                {
                                    Name = "Piosenka",
                                    Lenght = 23.5
                                },
                                new Song()
                                {
                                    Name = "Piosenka2",
                                    Lenght = 22
                                },
                                new Song()
                                {
                                    Name = "ADoSortowania",
                                    Lenght = 23.5
                                }
                            }
                        }
                    }



                }
            };

            return bands;
        }
    }
}
