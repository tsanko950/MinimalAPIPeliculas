using MinimalAPIPeliculas.Entidades;

namespace MinimalAPIPeliculas.Repositorios
{
    public interface IRepositorioGeneros
    {
        Task<List<Genero>> GetAll();
        Task<Genero?> GetByID(int id);
        Task<int> Crear(Genero genero);
        Task<bool> Existe(int id);
        Task<List<int>> Existen(List<int> ids);
        Task Actualizar(Genero genero);
        Task Eliminar(int id);
    }
}
