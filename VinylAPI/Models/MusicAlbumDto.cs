using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylAPI.Entities;

namespace VinylAPI.Models
{
    public class MusicAlbumDto
    {
        public string Name { get; set; }
        public int AmountOfSongs { get; set; }
        public int PublicationYear { get; set; }
        public string Genre { get; set; }
        public List<SongDto> Songs { get; set; }
    }
}
