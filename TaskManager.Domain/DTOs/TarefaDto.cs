using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.DTOs
{
    public class TarefaDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "Está completa")]
        public bool IsCompleted { get; set; }

        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        [Display(Name = "Data de Criação")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Data de Conclusão")]
        public DateTime? CompletedAt { get; set; }
    }
}
