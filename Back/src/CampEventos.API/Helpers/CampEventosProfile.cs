using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CampEventos.Application.Dtos;
using CampEventos.Domain;

namespace CampEventos.API.helpers
{
    public class CampEventosProfile : Profile
    {
        public CampEventosProfile()
        {
            CreateMap<Evento, EventoDto>().ReverseMap();
        }
    }
}