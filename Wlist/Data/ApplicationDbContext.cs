using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wlist.Models;

namespace Wlist.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FilmUtilisateur>()
            .HasKey(t => new { t.IdUtilisateur, t.IdFilm });
        }

        public DbSet<Film> Films { get; set; }
        public DbSet<FilmUtilisateur> FilmsUtilisteur { get; set; }
        public DbSet<Wlist.Models.ModeleVueFilm> ModeleVueFilm { get; set; }
    }
}