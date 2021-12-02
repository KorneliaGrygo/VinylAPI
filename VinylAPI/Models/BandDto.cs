using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylAPI.Entities;

namespace VinylAPI.Models
{
    public class BandDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<MusicAlbumDto> Albums { get; set; }
    }
}
