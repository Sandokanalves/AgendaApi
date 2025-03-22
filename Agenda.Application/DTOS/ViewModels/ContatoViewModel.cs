using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Application.DTOS.ViewModels
{
    public class ContatoViewModel
    {
        public ContatoViewModel(int id, string nome)
        {
            Id = id;
            Nome = nome;


        }
        public int Id { get; set; }
        public string Nome { get; set; }

    }
}
