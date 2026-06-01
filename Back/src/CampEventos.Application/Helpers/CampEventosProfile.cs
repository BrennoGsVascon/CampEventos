using System;
using System.Globalization;
using AutoMapper;
using CampEventos.Application.Dtos;
using CampEventos.Domain;
using CampEventos.Domain.Identity;
using CampEventos.Domain.Enum;

namespace CampEventos.Application.Helpers
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

            CreateMap<User, UserUpdateDto>()
                .ForMember(dest => dest.Nivel, opt => opt.MapFrom(src => src.Nivel.ToString()))
                .ForMember(dest => dest.Funcao, opt => opt.MapFrom(src => src.Funcao.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.Nivel, opt => opt.MapFrom(src =>
                    string.IsNullOrWhiteSpace(src.Nivel)
                        ? Nivel.NaoInformado
                        : Enum.Parse<Nivel>(src.Nivel)
                ))
                .ForMember(dest => dest.Funcao, opt => opt.MapFrom(src =>
                    string.IsNullOrWhiteSpace(src.Funcao)
                        ? Funcao.NaoInformado
                        : Enum.Parse<Funcao>(src.Funcao)
                ));

            CreateMap<Apresentador, ApresentadorDto>().ReverseMap();
            CreateMap<Apresentador, ApresentadorAddDto>().ReverseMap();
            CreateMap<Apresentador, ApresentadorUpdateDto>().ReverseMap();

            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
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
