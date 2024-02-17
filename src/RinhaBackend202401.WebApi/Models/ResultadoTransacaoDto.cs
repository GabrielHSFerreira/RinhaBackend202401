namespace RinhaBackend202401.WebApi.Models
{
    public record ResultadoTransacaoDto
    {
        public int Limite { get; init; }
        public int Saldo { get; init; }

        public ResultadoTransacaoDto(int limite, int saldo)
        {
            Limite = limite;
            Saldo = saldo;
        }
    }
}