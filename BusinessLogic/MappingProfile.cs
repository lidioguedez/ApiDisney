using AutoMapper;
using Core.DTO;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pelicula, PeliculaDto>();
            CreateMap<Pelicula,PeliculaDetalleDto>()
                .ForMember(d => d.data, opc => opc.MapFrom(src => src.Personajes));
            CreateMap<PeliculaPersonaje, PeliculaPersonajeDto>();
            CreateMap<PeliculaPersonaje, PersonajePeliculaDto>();
            CreateMap<Personaje, PersonajeDto>();
            CreateMap<Personaje, PersonajeDetalleDto>()
                .ForMember(d => d.data, opc => opc.MapFrom(src => src.Peliculas));
            CreateMap<Pelicula, PeliDto>();
            CreateMap<Genero, GeneroDto>();
            CreateMap<PeliculaDataDto, Pelicula>()
                .ForMember(m => m.Personajes, opc => opc.MapFrom(MapPersonajesMovies));
            CreateMap<PersonajeDataDto, Personaje>()
               .ForMember(m => m.Peliculas, opc => opc.MapFrom(MapPeliculasCharacters));

        }

        private List<PeliculaPersonaje> MapPeliculasCharacters(PersonajeDataDto personajeDto, Personaje personaje)
        {
            var resultado = new List<PeliculaPersonaje>();

            if (personajeDto.PeliculasId == null) return resultado;

            foreach (var peliculaID in personajeDto.PeliculasId)
            {
                resultado.Add(new PeliculaPersonaje() { PeliculaId = peliculaID });
            }

            return resultado;
        }

        private List<PeliculaPersonaje> MapPersonajesMovies(PeliculaDataDto peliculaDataDto, Pelicula pelicula)
        {
            var resultado = new List<PeliculaPersonaje>();

            if (peliculaDataDto.PersonajeID == null)  return resultado; 

            foreach (var personajeId in peliculaDataDto.PersonajeID)
            {
                resultado.Add(new PeliculaPersonaje() { PersonajeId = personajeId });
            }

            return resultado;
        }
    }
}
