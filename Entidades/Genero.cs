using System.ComponentModel.DataAnnotations;

namespace MinimalAPIPeliculas.Entidades
{
    public class Genero
    {
        public int id {  get; set; }
        public string Nombre { get; set; } = null!;
        public List<GeneroPelicula> GenerosPeliculas { get; set; } = new List<GeneroPelicula>();
    }
}
