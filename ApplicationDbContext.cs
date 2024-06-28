using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using MinimalAPIPeliculas.Entidades;

namespace MinimalAPIPeliculas
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // Especificamos que en la BD va a haber una tabla que se llamara Generos y tendra la estructura de la clase Genero
        // Desde la consola de comandos de Nuget ejecutamos:
        // PM> Add-Migration Generos
        // PM> Update-Database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genero>().Property(p => p.Nombre).HasMaxLength(50);

            modelBuilder.Entity<Actor>().Property(p => p.Nombre).HasMaxLength(150);
            modelBuilder.Entity<Actor>().Property(p => p.Foto).IsUnicode();

            modelBuilder.Entity<Pelicula>().Property(p => p.Titulo).HasMaxLength(150);
            modelBuilder.Entity<Pelicula>().Property(p => p.Poster).IsUnicode();

            // Dos llaves primarias
            modelBuilder.Entity<GeneroPelicula>().HasKey(g => new { g.GeneroId, g.PeliculaId });

            modelBuilder.Entity<ActorPelicula>().HasKey(a => new { a.PeliculaId, a.ActorId });

        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<GeneroPelicula> GenerosPeliculas { get; set; }
        public DbSet<ActorPelicula> ActoresPeliculas { get; set; }
    }
}
