namespace VinylAPI.Models
{

    public class BandQuery : Query
    {

    }
    public class MusicAlbumQuery : Query
    {

    }
    public class SongQuery : Query { 
    }
    public abstract class Query
    {
        public string SearchPhrase { get; set; }
        public int PageSize { get; set; } = 20;
        public int PageNumber { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
