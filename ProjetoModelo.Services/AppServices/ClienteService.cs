using ProjetoModelo.Data.Repositories;
using ProjetoModelo.Domain.Exceptions;
using ProjetoModelo.Domain.Interfaces.Repositories;
using ProjetoModelo.Domain.Interfaces.Services;
using ProjetoModelo.Domain.Models;
using ProjetoModelo.Domain.Models.ViewModels;

namespace ProjetoModelo.Services.AppServices
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ILogAcessoService _logAcessoService;

        public ClienteService(IClienteRepository clienteRepostory,
                              ILogAcessoService logAcessoService)
        {
            _clienteRepository = clienteRepostory;
            _logAcessoService = logAcessoService;   
        }
        public async Task<ClienteViewModel> GetById(int id)
        {
            try
            {
                var cliente = await _clienteRepository.GetById(id);

                if (cliente == null)
                    throw new NotFoundException("Cliente não encontrado");
                await _logAcessoService.Save("GetById", "Obtido dados do cliente pelo id", cliente.Id);
                return new ClienteViewModel(cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<bool> ValidaCliente(string dado, string tipo)
        {
            try
            {
                bool valido = true;
                Cliente cliente;
                if (tipo!= null && tipo.ToLower() == "cpf")
                {
                    cliente = await _clienteRepository.GetByCpf(dado);
                    valido = cliente == null;
                }
                else if (tipo != null && tipo.ToLower() == "email")
                {
                    cliente = await _clienteRepository.GetByEmail(dado);
                    valido = cliente == null;
                }
                else
                {
                    throw new BadRequestException("Tipo inválido");
                }
                return valido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<bool> Validador(string cpf, string email)
        {
            try
            {
                bool valido = true;
                Cliente cliente;
                cliente = await _clienteRepository.GetByCpf(cpf);
                if (cliente != null)
                {
                    valido = false;
                }
                else
                {
                    cliente = await _clienteRepository.GetByEmail(email);
                    if (cliente != null)
                    {

                    }
                        valido = false;
                }
                return valido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ClienteViewModel> CadastrarCliente(Cliente dadosCliente)
        {
            try
            {
                var novoCliente = new Cliente
                {
                    CPF = dadosCliente.CPF,
                    Nome = dadosCliente.Nome,
                    Email = dadosCliente.Email,
                    DataNascimento = dadosCliente.DataNascimento,
                    Endereco = dadosCliente.Endereco,
                    Telefone = dadosCliente.Telefone,
                    Profissao = dadosCliente.Profissao,
                    Comentarios = dadosCliente.Comentarios,
                    DataCadastro = DateTime.Now,
                };
                var cliente = await _clienteRepository.Add(novoCliente);
                await _logAcessoService.Save("CadastrarCliente", "Cliente cadastrado", cliente.Id);
                return new ClienteViewModel(cliente);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
