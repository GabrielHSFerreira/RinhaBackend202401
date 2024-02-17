using System.ComponentModel.DataAnnotations;

namespace RinhaBackend202401.WebApi.Models
{
    public record NovaTransacaoDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Valor { get; init; }

        [Required]
        [AllowedValues('c', 'd')]
        public char Tipo { get; init; }

        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string? Descricao { get; init; }
    }
}