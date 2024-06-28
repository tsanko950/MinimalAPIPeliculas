using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using MinimalAPIPeliculas.DTOs;
using MinimalAPIPeliculas.Entidades;
using MinimalAPIPeliculas.Repositorios;
using System.Runtime.CompilerServices;

namespace MinimalAPIPeliculas.Endpoints
{
    public static class GenerosEndpoints
    {
        public static RouteGroupBuilder MapGeneros(this RouteGroupBuilder group)
        {
            group.MapGet("/", ObtenerGeneros).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("generos-get"));
            group.MapGet("/{id:int}", ObtenerGeneroPorID);
            group.MapPost("/", CrearGenero);
            group.MapPut("/{id:int}", ActualizarGenero);
            group.MapDelete("/", EliminarGenero);
            return group;
        }


        static async Task<Ok<List<GeneroDTO>>> ObtenerGeneros(IRepositorioGeneros repositorio, IMapper mapper)
        {
            var generos = await repositorio.GetAll();
            var generosDTO = mapper.Map<List<GeneroDTO>>(generos);
            return TypedResults.Ok(generosDTO);
        }

        static async Task<Results<Ok<GeneroDTO>, NotFound>> ObtenerGeneroPorID(IRepositorioGeneros repositorio, int id, IMapper mapper)
        {
            var genero = await repositorio.GetByID(id);

            var generoDTO = mapper.Map<GeneroDTO>(genero);

            return genero != null ? TypedResults.Ok(generoDTO) : TypedResults.NotFound();
        }

        static async Task<Created<GeneroDTO>> CrearGenero(CrearGeneroDTO crearGeneroDTO, 
            IRepositorioGeneros repositorio, 
            IOutputCacheStore outputCacheStore,
            IMapper mapper
            )
        {

            var genero = mapper.Map<Genero>(crearGeneroDTO);
            var id = await repositorio.Crear(genero);
            await outputCacheStore.EvictByTagAsync("generos-get", default); // Limpiar el cache
            var generoDTO = mapper.Map<GeneroDTO>(genero);

            return TypedResults.Created($"/generos/{id}", generoDTO);
        }

        static async Task<Results<NoContent, NotFound>> ActualizarGenero(int id, CrearGeneroDTO crearGeneroDTO, IRepositorioGeneros repositorio, IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var existe = await repositorio.Existe(id);

            if (!existe)
            {
                return TypedResults.NotFound();
            }

            var genero = mapper.Map<Genero>(crearGeneroDTO);

            await repositorio.Actualizar(genero);
            await outputCacheStore.EvictByTagAsync("generos-get", default); // Limpiar el cache
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound>> EliminarGenero(int id, IRepositorioGeneros repositorio, IOutputCacheStore outputCacheStore)
        {

            var existe = await repositorio.Existe(id);

            if (!existe)
            {
                return TypedResults.NotFound();
            }

            await repositorio.Eliminar(id);
            await outputCacheStore.EvictByTagAsync("generos-get", default); // Limpiar el cache
            return TypedResults.NoContent();
        }
    }
}
