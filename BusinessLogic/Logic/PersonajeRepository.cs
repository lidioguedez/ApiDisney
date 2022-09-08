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
    public class PersonajeRepository : IPersonajeRepository
    {
        public ApiDisneyDbContext _context { get; }
        public PersonajeRepository(ApiDisneyDbContext context)
        {
            _context = context;
        }

        public async Task<List<int>> GetListPersIdsAsinc(PeliculaDataDto pelicula)
        {
            var listPers = await _context.Personaje.Where(p => pelicula.PersonajeID.Contains(p.PersonajeId)).Select(x => x.PersonajeId).ToListAsync();
            return listPers;
        }

        public void RevisarPerson(ref Personaje person, PersonajeDataDto personData)
        {
            PropertyInfo[] lst = typeof(PersonajeDataDto).GetProperties();
            foreach (PropertyInfo oProperty in lst)
            {
                string NombreAtributo = oProperty.Name;
                string Valor = "7";

                if (oProperty.GetValue(personData) != null)
                {
                    Valor = oProperty.GetValue(personData).ToString();

                }

                switch (NombreAtributo)
                {
                    case "Nombre":
                        person.Nombre = Valor != person.Nombre && Valor != "7" ? personData.Nombre : person.Nombre;
                        break;
                    case "Edad":
                        if ((int)Convert.ToUInt32(Valor) != person.Edad && Valor != "7") person.Edad = (int)personData.Edad;
                        break;
                    case "Peso":
                        if ((int)Convert.ToUInt32(Valor) != person.Peso && Valor != "7") person.Peso = (int)personData.Peso;
                        break;
                    case "Historia":
                        person.Historia = Valor != person.Historia && Valor != "7" ? personData.Historia : person.Historia;
                        break;
                    case "Imagen":
                        person.Imagen = Valor != person.Imagen && Valor != "7" ? personData.Imagen : person.Imagen;
                        break;
                   
                }

            }
        }
    }
}
