using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RinhaBackend202401.WebApi.Contexts;
using RinhaBackend202401.WebApi.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RinhaBackend202401.WebApi.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly RinhaContext _context;

        public ClientesController(RinhaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpPost("{id:int}/transacoes")]
        public async Task<IActionResult> InserirNovaTransacao(
            [FromRoute] int id,
            NovaTransacaoDto novaTransacao,
            CancellationToken cancellationToken)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (cliente == null)
                return NotFound("Cliente não encontrado.");

            cliente.RealizarTransacao(novaTransacao);

            await _context.SaveChangesAsync(cancellationToken);

            return Ok(new ResultadoTransacaoDto(cliente.Limite, cliente.Saldo));
        }

        [HttpGet("{id:int}/extrato")]
        public async Task<IActionResult> ObterExtrato([FromRoute] int id, CancellationToken cancellationToken)
        {
            var saldo = await _context.Clientes
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new SaldoDto(x.Saldo, DateTime.UtcNow, x.Limite))
                .FirstAsync(cancellationToken);

            if (saldo == null)
                return NotFound("Cliente não encontrado.");

            var transacoes = await _context.Transacoes
                .AsNoTracking()
                .Where(x => x.IdCliente == id)
                .OrderByDescending(x => x.Id)
                .Take(10)
                .Select(x => new TransacaoDto(x.Valor, x.Tipo, x.Descricao, x.RealizadaEm))
                .ToListAsync(cancellationToken);

            return Ok(new ExtratoDto(saldo, transacoes));
        }
    }
}