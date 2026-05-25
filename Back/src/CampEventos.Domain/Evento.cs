using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Domain.Identity;

namespace CampEventos.Domain
{
    public class Evento
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime? DataEvento { get; set; }
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Descricao { get; set; }
        public string Modalidade { get; set; }
        public int UserId {get; set;}
        public User USer { get; set; }
        public IEnumerable<Lote> Lotes { get; set; }
        public IEnumerable<RedeSocial> RedesSociais  { get; set; }
        public IEnumerable<ApresentadorEvento> ApresentadoresEventos { get; set; }
    }
}