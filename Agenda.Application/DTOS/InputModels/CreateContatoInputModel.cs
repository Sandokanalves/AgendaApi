using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Application.DTOS.InputModels
{
    public class CreateContatoInputModel
    {   
        public string Nome { get; set; } 
        public string Email { get; set; } 
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Site { get; set; }
        public string TelefoneComercial { get; set; }
    }
}