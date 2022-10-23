using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo o máximo de caracater é 60")]
        [MinLength(3, ErrorMessage = "Este campo o mímino de caracter é 3")]
        public string Title { get; set; }

        [MaxLength(1024, ErrorMessage = "Este vampo deve conter no máximo 1024 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage="Categoria inválida")]
        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
    }
}