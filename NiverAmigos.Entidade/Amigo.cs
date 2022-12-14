using System.ComponentModel.DataAnnotations;

namespace NiverAmigos.Entidade
{
    public class Amigo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo 'Nome' Obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo 'Sobrenome' Obrigatório")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Campo 'Anversário' Obrigatório")]
        public DateTime Aniversario { get; set; }

    }
}