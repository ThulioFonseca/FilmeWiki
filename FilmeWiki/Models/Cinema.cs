using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmeWiki.Models
{
    public class Cinema
    {
        public int ID { get; set; }

        [Display(Name = "Nome"), StringLength(50, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 50 caracteres"), Required(ErrorMessage = "O nome é obrigatório")]
        public string Descricao { get; set; }
        [Display(Name = "Cidade"), StringLength(50, MinimumLength = 3, ErrorMessage = "O endereço deve ter entre 3 e 50 caracteres"), Required(ErrorMessage = "O endereço é obrigatório")]
        public string Cidade { get; set; }
        [Display(Name = "Estado"), StringLength(2, MinimumLength = 2, ErrorMessage = "O estado deve ter 2 caracteres"), RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Formato inválido, Somente duas letras permitidas.Ex:'MG'"), Required(ErrorMessage = "O estado é obrigatório")]
        public string UF { get; set; }

        public ICollection<Cartaz> Cartaz { get; set; }
    }
}
