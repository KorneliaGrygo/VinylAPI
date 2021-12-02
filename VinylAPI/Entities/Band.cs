using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VinylAPI.Entities
{
    public class Band
    {
        public int Id { get; set; }
        [MaxLength(50,ErrorMessage = "Nazwa zespołu przekracza 50 znaków.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<MusicAlbum> Albums { get; set; }

    }
}
