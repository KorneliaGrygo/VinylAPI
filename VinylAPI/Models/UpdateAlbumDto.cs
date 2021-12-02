namespace VinylAPI.Models
{
    public class UpdateAlbumDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AmountOfSongs { get; set; }
        public int? PublicationYear { get; set; }
        public string Genre { get; set; }
    }
}
