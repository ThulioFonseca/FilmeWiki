using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FilmeWiki.Models
{
    public class FilmeGeneroViewModel
    {
        public List<Filme> filmes;
        public SelectList generos;
        public SelectList filmesDisponiveis;
        public string filmeGenero { get; set; }

    }
}
