using AutoMapper;
using BusinessLogic.Data;
using Core.DTO;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly IGenericRepository<Personaje> _PersonGenericRepository;
        private readonly IMapper _mapper;
        private readonly IPeliculaRepository _peliculaRepository;

        public CharactersController(IMapper mapper, IGenericRepository<Personaje> PersonGenericRepository, IPeliculaRepository peliculaRepository)
        {
            _PersonGenericRepository = PersonGenericRepository;
            _mapper = mapper;
            _peliculaRepository = peliculaRepository;

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<PersonajeDto>>> GetPersonaje(string? name, int? age, int? movie)
        {
            var spec = new PersonajeWithPersonajePeliculaAndSpesification(name, age, movie);
            var Listperson = await _PersonGenericRepository.GetAllWithSpec(spec);

            if (Listperson.Count == 0)
            {
                return NotFound();
            }

            var mRresponse = _mapper.Map< IReadOnlyList<Personaje>, IReadOnlyList<PersonajeDto>>(Listperson);

            return Ok(mRresponse);
        }

      
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PersonajeDetalleDto>> GetPersonajeDetalle(int id)
        {
            var spec = new PersonajeWithPersonajePeliculaAndSpesification(id);
            var person = await _PersonGenericRepository.GetByIdWithSpec(spec);

            var mRresponse = _mapper.Map<Personaje, PersonajeDetalleDto>(person);


            return Ok(mRresponse);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PersonajeDto>> AddCharacter(PersonajeDataDto personaje)
        {
            var listPelis = await _peliculaRepository.GetListPeliIdsAsinc(personaje);

            if (personaje.PeliculasId.Count != listPelis.Count)
            {
                return BadRequest("No Existe Uno De Los Id de Peliculas Enviadas");
            }

            var person = _mapper.Map<Personaje>(personaje);
            var resp = await _PersonGenericRepository.CreateAsinc(person);
            var spec = new PersonajeWithPersonajePeliculaAndSpesification(person.PersonajeId);
            await _PersonGenericRepository.GetByIdWithSpec(spec);

            if (resp == 0)
            {
                return BadRequest("No Se Pudo Crear la Pelicula");
            }

            var mpersonDto = _mapper.Map<Personaje, PersonajeDto>(person);

            return Ok(mpersonDto);


        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteCharacter(int id)
        {
            var person = await _PersonGenericRepository.GetByIdAsync(id);

            if (person == null)
            {
                return NotFound("Pelicula No encontrada");
            }

            _PersonGenericRepository.DeleteAsinc(person);

            return Content("Personaje Eliminado");
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> EditarPersonaje(int id, PersonajeDataDto personajeDataDto)
        {
            var spec = new PersonajeWithPersonajePeliculaAndSpesification(id);
            var person = await _PersonGenericRepository.GetByIdWithSpec(spec);

            if (person == null)
            {
                return NotFound("No existe el Personaje que desea Editar");
            }

            person = _mapper.Map(personajeDataDto, person);

            var resp = await _PersonGenericRepository.UpdateAsinc(person);

            if (resp == 0)
            {
                return NotFound("Eror al guardar la pelicula");
            }

            var upPerso = await _PersonGenericRepository.GetByIdWithSpec(spec);
            var personDet = _mapper.Map<Personaje, PersonajeDetalleDto>(upPerso);

            return Ok(personDet);
        }

    }
}
