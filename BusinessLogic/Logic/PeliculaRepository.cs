using AutoMapper;
using BusinessLogic.Data;
using Core.DTO;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class PeliculaRepository : IPeliculaRepository
    {
        private readonly ApiDisneyDbContext DbContext;
        private readonly IMapper _mapper;
        public PeliculaRepository(ApiDisneyDbContext _context, IMapper mapper)
        {
            DbContext = _context;
            _mapper = mapper;
        }

        public async Task<List<int>> GetListPeliIdsAsinc(PersonajeDataDto personaje)
        {
            var listPeli = await DbContext.Peliculas.Where(p => personaje.PeliculasId.Contains(p.PeliculaId)).Select(x => x.PeliculaId).ToListAsync();
            return listPeli;
        }

        public async Task<Pelicula> GetPeliculaByIdAsinc(int id)
        {
            return await DbContext.Peliculas.FindAsync(id); 
        }

        public async Task<IReadOnlyList<PeliculaDto>> GetPeliculasAsinc()
        {
            var pelis = await DbContext.Peliculas
                            .Include(g => g.Genero)
                            .Include(p => p.Personajes).ThenInclude(pi => pi.Personaje)
                            .ToListAsync();

            var peliculas =  _mapper.Map<List<Pelicula>, List<PeliculaDto>>(pelis);

            return peliculas;
        }

        public void RevisarPeli(ref Pelicula peli, PeliculaDataDto pelicula)
        {
            PropertyInfo[] lst = typeof(PeliculaDataDto).GetProperties();
            foreach (PropertyInfo oProperty in lst)
            {
                string NombreAtributo = oProperty.Name;
                string Valor = "7";

                if (oProperty.GetValue(pelicula) != null)
                {
                    Valor = oProperty.GetValue(pelicula).ToString();

                }

                switch (NombreAtributo)
                {
                    case "Titulo":
                        peli.Titulo = Valor != peli.Titulo && Valor != "7" ? pelicula.Titulo : peli.Titulo;
                        break;
                    case "Imagen":
                        peli.Imagen = Valor != peli.Imagen && Valor != "7" ? pelicula.Imagen : peli.Imagen;
                        break;
                    case "FechaCreacion":
                        peli.FechaCreacion = Valor != peli.FechaCreacion && Valor != "7" ? pelicula.FechaCreacion : peli.FechaCreacion;
                        break;
                    case "Calificacion":
                        if ((int)Convert.ToUInt32(Valor) != peli.Calificacion && Valor != "7") peli.Calificacion = (int)pelicula.Calificacion;
                        break;
                    case "GeneroId":
                        if ((int)Convert.ToUInt32(Valor) != peli.GeneroId && Valor != "7")  peli.GeneroId = (int)pelicula.GeneroId;
                        break;
                }

            }
        }
    }
}


