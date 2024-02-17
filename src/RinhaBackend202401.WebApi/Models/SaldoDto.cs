using System;

namespace RinhaBackend202401.WebApi.Models
{
    public record SaldoDto
    {
        public int Total { get; init; }
        public DateTime DataExtrato { get; init; }
        public int Limite { get; init; }

        public SaldoDto(int total, DateTime dataExtrato, int limite)
        {
            Total = total;
            DataExtrato = dataExtrato;
            Limite = limite;
        }
    }
}