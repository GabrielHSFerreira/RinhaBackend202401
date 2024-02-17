namespace RinhaBackend202401.WebApi.Models
{
    public record NovaTransacaoDto
    {
        public int Valor { get; init; }
        public char Tipo { get; init; }
        public string? Descricao { get; init; }
    }
}