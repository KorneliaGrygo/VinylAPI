using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylAPI.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Lenght { get; set; }
        public int MusicAlbumId { get; set; }

    }
}
