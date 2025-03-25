

namespace Agenda.Application.DTOS.InputModels
{
    public class UpdateContatoInput
    {
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Site { get; set; }
        public string TelefoneComercial { get; set; }
    }
}
