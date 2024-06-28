using Microsoft.EntityFrameworkCore;
using MinimalAPIPeliculas.Entidades;

namespace MinimalAPIPeliculas.Repositorios
{
    public class RepositorioGeneros : IRepositorioGeneros
    {
        private readonly ApplicationDbContext context;
        public RepositorioGeneros(ApplicationDbContext conext)
        {
            this.context = conext;
        }


        public async Task<List<Genero>> GetAll()
        {
            return await context.Generos.OrderBy(x => x.Nombre).ToListAsync();
        }


        public async Task<Genero?> GetByID(int id)
        {
            return await context.Generos.FirstOrDefaultAsync(g => g.id == id);
        }



        public async Task<bool> Existe(int id)
        {
            return await context.Generos.AnyAsync(g => g.id == id);
        }

        public async Task<List<int>> Existen (List<int> ids)
        {
            return await context.Generos.Where(g => ids.Contains(g.id)).Select(g => g.id).ToListAsync();
        }

        public async Task<int> Crear(Genero genero)
        {
            context.Add(genero);
            await context.SaveChangesAsync();
            return genero.id;
        }

        public async Task Actualizar(Genero genero)
        {
            context.Update(genero);
            await context.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            await context.Generos.Where(g => g.id == id).ExecuteDeleteAsync();
        }
    }
}
