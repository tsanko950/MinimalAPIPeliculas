namespace MinimalAPIPeliculas.Entidades
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Cuerpo { get; set; }
        public int PeliculaId { get; set; }
    }
}
