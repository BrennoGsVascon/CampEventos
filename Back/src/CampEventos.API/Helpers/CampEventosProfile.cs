using System;
using System.Globalization;
using AutoMapper;
using CampEventos.Application.Dtos;
using CampEventos.Domain;
using CampEventos.Domain.Identity;

namespace CampEventos.API.helpers
{
    public class CampEventosProfile : Profile
    {
        public CampEventosProfile()
        {
            CreateMap<Evento, EventoDto>()
                .ForMember(dest => dest.DataEvento, opt => opt.MapFrom(src => FormatDateTime(src.DataEvento)))
                .ReverseMap()
                .ForMember(dest => dest.DataEvento, opt => opt.MapFrom(src => ParseNullableDateTime(src.DataEvento)));

            CreateMap<Lote, LoteDto>()
                .ForMember(dest => dest.DataInicio, opt => opt.MapFrom(src => FormatDate(src.DataInicio)))
                .ForMember(dest => dest.DataFim, opt => opt.MapFrom(src => FormatDate(src.DataFim)))
                .ReverseMap()
                .ForMember(dest => dest.DataInicio, opt => opt.MapFrom(src => ParseNullableDateTime(src.DataInicio)))
                .ForMember(dest => dest.DataFim, opt => opt.MapFrom(src => ParseNullableDateTime(src.DataFim)));

            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<Apresentador, ApresentadorDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
        }

        private static string FormatDateTime(DateTime? date)
        {
            return date.HasValue ? date.Value.ToString("yyyy-MM-ddTHH:mm:ss") : null;
        }

        private static string FormatDate(DateTime? date)
        {
            return date.HasValue ? date.Value.ToString("yyyy-MM-dd") : null;
        }

        private static DateTime? ParseNullableDateTime(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            var formats = new[]
            {
                "yyyy-MM-ddTHH:mm:ss",
                "yyyy-MM-ddTHH:mm",
                "yyyy-MM-dd",
                "dd/MM/yyyy HH:mm",
                "dd/MM/yyyy"
            };

            if (DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedExact))
            {
                return parsedExact;
            }

            if (DateTime.TryParse(value, new CultureInfo("pt-BR"), DateTimeStyles.None, out var parsedPtBr))
            {
                return parsedPtBr;
            }

            if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedInvariant))
            {
                return parsedInvariant;
            }

            return null;
        }
    }
}
