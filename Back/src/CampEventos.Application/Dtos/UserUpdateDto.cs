using CampEventos.Domain.Enum;

namespace CampEventos.Application.Dtos
{
    public class UserUpdateDto
    {
        public string Nivel { get; set; }
        public string UserName { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Funcao { get; set; }
        public string Descricao { get; set; }
        public string ImagemURL { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
