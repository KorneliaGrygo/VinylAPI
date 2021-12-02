using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylAPI.Entities
{
    public class MusicAlbum
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AmountOfSongs { get; set; }
        public int PublicationYear { get; set; }
        public string Genre { get; set; }
        public int BandId { get; set; }
        public ICollection<Song> Songs { get; set; }

    }
}
