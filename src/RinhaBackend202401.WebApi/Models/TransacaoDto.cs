using System;

namespace RinhaBackend202401.WebApi.Models
{
    public record TransacaoDto
    {
        public int Valor { get; init; }
        public char Tipo { get; init; }
        public string Descricao { get; init; }
        public DateTime RealizadaEm { get; init; }

        public TransacaoDto(int valor, TipoTransacao tipo, string descricao, DateTime realizadaEm)
        {
            Valor = valor;
            Tipo = (char)tipo;
            Descricao = descricao ?? throw new ArgumentNullException(nameof(descricao));
            RealizadaEm = realizadaEm;
        }
    }
}