namespace FilmeWiki.Models
{
    public class Cartaz
    {
        public int FilmeId { get; set; }
        public Filme Filme { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }

    }
}
