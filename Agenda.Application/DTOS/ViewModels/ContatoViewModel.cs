
namespace Agenda.Application.DTOS.ViewModels
{
    public class ContatoViewModel
    {
        public ContatoViewModel(int id, string nome, string email, string telefone)
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

    }
}
