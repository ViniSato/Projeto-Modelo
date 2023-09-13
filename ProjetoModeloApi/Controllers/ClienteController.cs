using Microsoft.AspNetCore.Mvc;
using ProjetoModelo.Domain.Interfaces.Services;
using ProjetoModelo.Domain.Models;
using ProjetoModelo.Domain.Models.ViewModels;
using ProjetoModelo.Services.AppServices;

namespace ProjetoModeloApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, [FromServices] IClienteService clienteService)
            => Ok(await clienteService.GetById(id));

        [HttpGet("valida-cliente")]
        public async Task<IActionResult> ValidaCliente(string dado, string tipo, [FromServices] IClienteService clienteService)
                 => Ok(await clienteService.ValidaCliente(dado, tipo));

        [HttpPost()]
        public async Task<IActionResult> CadastrarCliente(
        [FromBody] Cliente? dadosCliente,
        [FromServices] ILogErroService erroService,
        [FromServices] IClienteService clienteService)
        {
            try
            {
                ClienteViewModel clienteViewModel;
                var valido = await clienteService.Validador(dadosCliente.CPF, dadosCliente.Email);
                if (valido != false)
                {
                    clienteViewModel = await clienteService.CadastrarCliente(dadosCliente);
                }
                else
                {
                    throw new BadHttpRequestException("Cliente já cadastrado");
                }

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = clienteViewModel.Id },
                    clienteViewModel
                );
            }
            catch (Exception ex)
            {
                await erroService.Save(ex, true, dadosCliente.Id);
                return StatusCode(500);
            }
        }
    }
}