using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylAPI.Models
{
    public class GetMusicAlbumDto
    {
        public string Name { get; set; }
        public int AmountOfSongs { get; set; }
        public int PublicationYear { get; set; }
        public string Genre { get; set; }
    }
}
