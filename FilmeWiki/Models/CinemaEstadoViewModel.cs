using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FilmeWiki.Models
{
    public class CinemaEstadoViewModel
    {

        public Cinema Cinema = new Cinema();

        public Cartaz Cartaz = new Cartaz();

        public SelectList filmesCartaz;

        public List<Cinema> cinemas;

        public SelectList estados;
        public string cinemaEstado { get; set; }


    }
}
