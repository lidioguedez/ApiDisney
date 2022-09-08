using Core.Entities;
using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPeliculaRepository
    {
        Task<Pelicula> GetPeliculaByIdAsinc(int id);
        Task<IReadOnlyList<PeliculaDto>> GetPeliculasAsinc();

        void RevisarPeli(ref Pelicula peli,PeliculaDataDto peliData);

        Task<List<int>> GetListPeliIdsAsinc(PersonajeDataDto personaje);


    }
}
