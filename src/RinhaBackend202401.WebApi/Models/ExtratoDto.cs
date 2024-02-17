using System;
using System.Collections.Generic;

namespace RinhaBackend202401.WebApi.Models
{
    public record ExtratoDto
    {
        public SaldoDto Saldo { get; init; }
        public List<TransacaoDto> UltimasTransacoes { get; init; }

        public ExtratoDto(SaldoDto saldo, List<TransacaoDto> ultimasTransacoes)
        {
            Saldo = saldo ?? throw new ArgumentNullException(nameof(saldo));
            UltimasTransacoes = ultimasTransacoes ?? throw new ArgumentNullException(nameof(ultimasTransacoes));
        }
    }
}