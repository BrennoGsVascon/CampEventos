using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CampEventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }

        [Required(ErrorMessage ="O campo {0} é obrigatório."),
        StringLength(50,MinimumLength =4,
                         ErrorMessage ="O {0} deve ter entre 4 a 50 caracteres.")]
        public string Tema { get; set; }

        [Display(Name ="Quantidade de pessoas")]
        [Range(1, 10000, ErrorMessage ="{0} não pode ser menor que 1 e maior que 10.000")]
        public int QtdPessoas { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png|webp|avif)$",
            ErrorMessage ="{0} Não é uma imagem válida. (gif, jpg, jpeg, bmp, png, webp ou avif)")]
        public string ImagemURL { get; set; }

        [Required(ErrorMessage ="O campo {0} deve ser preenchido.")]
        [Phone(ErrorMessage ="{0} é inválido")]
        public string Telefone { get; set; }

        [Required(ErrorMessage ="O campo {0} deve ser preenchido.")]
        [Display(Name = "e-email")]
        [EmailAddress(ErrorMessage ="O {0} deve ser válido.")]
        public string Email { get; set; }

        [Display(Name ="Descrição")]
        [Required(ErrorMessage = "O campo {0} deve ser preenchido."),
        StringLength(500, MinimumLength = 10, 
                          ErrorMessage = "A {0} deve ter entre 10 e 500 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido.")]
        public string Modalidade { get; set; }

        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais  { get; set; }
        public IEnumerable<ApresentadorDto> Apresentadores { get; set; }
    }
}