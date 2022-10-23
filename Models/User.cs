using System.ComponentModel.DataAnnotations;
namespace Shop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo tem um limite de carater de 20")]
        [MinLength(3, ErrorMessage = "Este campo deve ter no minimo 3 caraterer")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo tem um limite de carater de 20")]
        [MinLength(3, ErrorMessage = "Este campo deve ter no minimo 3 caraterer")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}