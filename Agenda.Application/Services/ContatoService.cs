
using Agenda.Application.DTOS.InputModels;
using Agenda.Application.DTOS.ViewModels;
using Agenda.Domain.Entities;
using Agenda.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

namespace Agenda.Application.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly IValidator<CreateContatoInputModel> _createValidator;
        private readonly IValidator<UpdateContatoInput> _updateValidator;
        private readonly IMapper _mapper;

        public ContatoService(IContatoRepository contatoRepository, IValidator<CreateContatoInputModel> createValidator, IValidator<UpdateContatoInput> updateValidator, IMapper mapper)
        {
            _contatoRepository = contatoRepository ?? throw new ArgumentNullException(nameof(contatoRepository));
            _createValidator = createValidator ?? throw new ArgumentNullException(nameof(createValidator));
            _updateValidator = updateValidator ?? throw new ArgumentNullException(nameof(updateValidator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ContatoViewModel>> GetAllAsync()
        {
            var contatos = await _contatoRepository.GetAllAsync();

            if (contatos == null || !contatos.Any())
            {
                return Enumerable.Empty<ContatoViewModel>();
            }

            var contatoViewModels = _mapper.Map<IEnumerable<ContatoViewModel>>(contatos);
            foreach (var contato in contatoViewModels)
            {
                Console.WriteLine($"Contato: {contato.Id}, {contato.Nome}");
            }

            return contatoViewModels;
        }

        public async Task<ContatoDetailsViewModel> GetByIdAsync(int id)
        {
            var contato = await _contatoRepository.GetByIdAsync(id);
            return _mapper.Map<ContatoDetailsViewModel>(contato);
        }

        public async Task<ContatoViewModel> AddAsync(CreateContatoInputModel contatoInput)
        {
            var validationResult = await _createValidator.ValidateAsync(contatoInput);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);


            var contato = _mapper.Map<Contato>(contatoInput);
            contato = await _contatoRepository.AddAsync(contato);
            return _mapper.Map<ContatoViewModel>(contato);
        }

        public async Task<ContatoViewModel> UpdateAsync(int id, UpdateContatoInput contatoInput)
        {
            var validationResult = await _updateValidator.ValidateAsync(contatoInput);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

             var contatoExistente = await _contatoRepository.GetByIdAsync(id);
             if (contatoExistente == null)
             throw new KeyNotFoundException($"Contato com o ID {id} não encontrado.");


   
                _mapper.Map(contatoInput, contatoExistente);

                contatoExistente = await _contatoRepository.UpdateAsync(contatoExistente);
    
                if (contatoExistente == null)
                    throw new InvalidOperationException("Erro ao atualizar o contato.");

                return _mapper.Map<ContatoViewModel>(contatoExistente);
           }

         public async Task<bool> DeleteAsync(int id)
          {
                return await _contatoRepository.DeleteAsync(id);
          }
    }
}
