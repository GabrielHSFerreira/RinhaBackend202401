using Medallion.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RinhaBackend202401.WebApi.Contexts;
using RinhaBackend202401.WebApi.Models;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RinhaBackend202401.WebApi.Controllers
{
    [Route("clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly RinhaContext _context;
        private readonly IDistributedLockProvider _lockProvider;

        public ClientesController(RinhaContext context, IDistributedLockProvider lockProvider)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _lockProvider = lockProvider ?? throw new ArgumentNullException(nameof(lockProvider));
        }

        [HttpPost("{id:int}/transacoes")]
        public async Task<IActionResult> InserirNovaTransacao(
            [FromRoute] int id,
            NovaTransacaoDto novaTransacao,
            CancellationToken cancellationToken)
        {
            using (await _lockProvider.AcquireLockAsync($"UserTransaction{id}", cancellationToken: cancellationToken))
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (cliente == null)
                    return NotFound("Cliente não encontrado.");

                if (!cliente.RealizarTransacao(novaTransacao))
                    return UnprocessableEntity("Transação excede limite do cliente.");

                await _context.SaveChangesAsync(cancellationToken);

                return Ok(new ResultadoTransacaoDto(cliente.Limite, cliente.Saldo));
            }
        }

        [HttpGet("{id:int}/extrato")]
        public async Task<IActionResult> ObterExtrato([FromRoute] int id, CancellationToken cancellationToken)
        {
            var saldo = await _context.Clientes
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new SaldoDto(x.Saldo, DateTime.UtcNow, x.Limite))
                .FirstOrDefaultAsync(cancellationToken);

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