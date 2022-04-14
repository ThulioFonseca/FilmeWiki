using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmeWiki.Models
{
    public class Filme
    {
        public int ID { get; set; }
        
        [Display(Name = "Título"), StringLength(50, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 50 caracteres"), Required(ErrorMessage = "O nome é obrigatório")]
        public string Titulo { get; set; }

        [Display(Name = "Data de Lançamento"), DataType(DataType.Date), Required(ErrorMessage = "A Data é obrigatório")]
        public DateTime Lancamento { get; set; }
        [Display(Name = "Gênero"),Required(ErrorMessage = "O Gênero é obrigatório"), StringLength(30)]
        public string Genero { get; set; }
        [Display(Name = "Preço"), Range(1.00, 100.00), Required(ErrorMessage = "É necessário informar um valor"), DataType(DataType.Currency)]
        public decimal Preco { get; set; }
        [Required(ErrorMessage = "Informe a classificação"), StringLength(5)]
        public string Classificacao { get; set; }
        public ICollection<Cartaz> Cartaz { get; set; }

    }
}
