using System;

namespace RinhaBackend202401.WebApi.Models
{
    public class Transacao
    {
        public int Id { get; init; }
        public TipoTransacao Tipo { get; init; }
        public int Valor { get; init; }
        public string Descricao { get; init; } = string.Empty;
        public DateTime RealizadaEm { get; init; }

        public Cliente? Cliente { get; init; }

        public int IdCliente { get; init; }

        private Transacao() { }

        public Transacao(TipoTransacao tipo, int valor, string descricao, DateTime realizadaEm, Cliente cliente)
        {
            Tipo = tipo;
            Valor = valor;
            Descricao = descricao ?? throw new ArgumentNullException(nameof(descricao));
            RealizadaEm = realizadaEm;
            Cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            IdCliente = cliente.Id;
        }
    }
}