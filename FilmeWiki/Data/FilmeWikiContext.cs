using Microsoft.EntityFrameworkCore;

namespace FilmeWiki.Models
{
    public class FilmeWikiContext : DbContext
    {
        public FilmeWikiContext(DbContextOptions<FilmeWikiContext> options)
            : base(options)
        {
        }

        public DbSet<FilmeWiki.Models.Filme> Filme { get; set; }

        public DbSet<FilmeWiki.Models.Cinema> Cinema { get; set; }
        public DbSet<FilmeWiki.Models.Cartaz> Cartaz { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cartaz>()
                .HasKey(c => new { c.FilmeId, c.CinemaId });
        }

    }
}
