using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.DTO;
using Core.Entities;
using Core.Specifications;
using AutoMapper;
using BusinessLogic.Data;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IGenericRepository<Pelicula> _MovieRepository;
        private readonly IPersonajeRepository _PersonRepository;
        private readonly IPeliculaRepository _PeliRepository;
        private readonly IMapper _mapper;

        public MoviesController(IGenericRepository<Pelicula> MovieRepository, IMapper mapper, IGenericRepository<Pelicula> genericRepository, IPersonajeRepository PersonRepository, IPeliculaRepository PeliRepository)
        {
            _MovieRepository = MovieRepository;
            _mapper = mapper;
            _PersonRepository = PersonRepository;
            _PeliRepository = PeliRepository;
    }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<PeliculaDto>>> GetMovies(string? name,int? genre, string? order)
        {
            var spec = new PeliculaWithGeneroPersonajeSpecification(name, genre, order);
            var movies = await _MovieRepository.GetAllWithSpec(spec);

            var _movies =_mapper.Map<IReadOnlyList<Pelicula>, IReadOnlyList<PeliculaDto>>(movies);

            return Ok(_movies);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PeliculaDetalleDto>> GetMoviesDetalle(int id)
        {
            var spec = new PeliculaWithGeneroPersonajeSpecification(id);
            var movie = await _MovieRepository.GetByIdWithSpec(spec);

            var _movies = _mapper.Map<Pelicula, PeliculaDetalleDto>(movie);

            return Ok(_movies);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PeliculaDetalleDto>> AddMovies(PeliculaDataDto pelicula)
        {
            var listPers = await _PersonRepository.GetListPersIdsAsinc(pelicula);

            if (pelicula.PersonajeID.Count != listPers.Count)
            {
                return BadRequest("No Existe Uno De Los Personajes Enviados");
            }

            var movie = _mapper.Map<Pelicula>(pelicula);
            var resp = await _MovieRepository.CreateAsinc(movie);
            var spec = new PeliculaWithGeneroPersonajeSpecification(movie.PeliculaId);
            await _MovieRepository.GetByIdWithSpec(spec);

            if (resp == 0)
            {
                return BadRequest("No Se Pudo Crear la Pelicula");
            }

            var movieDto = _mapper.Map<Pelicula, PeliculaDetalleDto>(movie);

            return Ok(movieDto);

        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteMovies(int id)
        {
            var peli = await _MovieRepository.GetByIdAsync(id);

            if (peli == null)
            {
                return NotFound("Pelicula No encontrada");
            }
            
            _MovieRepository.DeleteAsinc(peli);

            return Content("Pelicula Eliminada");
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> EditarPelicula(int id, PeliculaDataDto peliculaDataDto)
        {
            var spec = new PeliculaWithGeneroPersonajeSpecification(id);
            var pelicula =  await _MovieRepository.GetByIdWithSpec(spec);

            if (pelicula == null)
            {
                return NotFound("No existe la Pelicula que desea Editar");
            }

            pelicula = _mapper.Map(peliculaDataDto, pelicula);

            var resp = await _MovieRepository.UpdateAsinc(pelicula);

            if (resp == 0)
            {
                return NotFound("Eror al guardar la pelicula");
            }

            var upPeli = await _MovieRepository.GetByIdWithSpec(spec);
            var movie = _mapper.Map<Pelicula, PeliculaDetalleDto>(upPeli);

            return Ok(movie);

        }
    }

}
