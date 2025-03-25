using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Application.DTOS.ViewModels
{
    public class ContatoDetailsViewModel
    {
        public ContatoDetailsViewModel(int id, string nome, string email, string telefone)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;


        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Site { get; set; }
        public string TelefoneComercial { get; set; }
    }
}
