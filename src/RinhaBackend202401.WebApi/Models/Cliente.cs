using System;
using System.Collections.Generic;

namespace RinhaBackend202401.WebApi.Models
{
    public class Cliente
    {
        public int Id { get; init; }
        public int Limite { get; init; }
        public int Saldo { get; private set; }

        public List<Transacao> Transacoes { get; init; } = new List<Transacao>();

        private Cliente() { }

        public Cliente(int id, int limite, int saldo)
        {
            Id = id;
            Limite = limite;
            Saldo = saldo;
        }

        public bool RealizarTransacao(NovaTransacaoDto novaTransacao)
        {
            var transacao = new Transacao(
                (TipoTransacao)novaTransacao.Tipo,
                novaTransacao.Valor,
                novaTransacao.Descricao!,
                DateTime.UtcNow,
                this);

            if (transacao.Tipo == TipoTransacao.Credito)
                AdicionarCredito(transacao.Valor);
            else if (!RealizarDebito(transacao.Valor))
                return false;

            Transacoes.Add(transacao);

            return true;
        }

        private void AdicionarCredito(int valor)
        {
            Saldo += valor;
        }

        private bool RealizarDebito(int valor)
        {
            var novoSaldo = Saldo - valor;

            if (novoSaldo < (Limite * -1))
                return false;

            Saldo = novoSaldo;

            return true;
        }
    }
}