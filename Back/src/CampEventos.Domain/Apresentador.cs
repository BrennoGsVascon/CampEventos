using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Domain.Identity;

namespace CampEventos.Domain
{
    public class Apresentador
    {
        public int Id { get; set; }
        public string MiniCurriculo  { get; set; }
        public int UserId  { get; set; }
        public User User { get; set; }
        public IEnumerable<RedeSocial> RedesSociais  { get; set; }
        public IEnumerable<ApresentadorEvento> ApresentadoresEventos { get; set; }
    }
}