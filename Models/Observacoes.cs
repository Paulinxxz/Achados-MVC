using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AchadosApi.Models
{
    [Table("Observacoes")]
    public class Observacoes
    {
        [Column("ObservacoesId")]
        [Display(Name = "Cód. observacao")]
        public int Id { get; set; }

        [Column("ObservacaoDescricao")]
        [Display(Name = "Descrição")]
        public string ObservacaoDescricao { get; set; } = string.Empty;

        [Column("ObservacaoLocal")]
        [Display(Name = "Local")]
        public string ObservacaoLocal { get; set; } = string.Empty;

        [Column("ObservacaoData")]
        [Display(Name = "Data de Observação")]
        public DateTime ObservacaoData { get; set; }

        [ForeignKey("AnimalId")]
        public int AnimalId { get; set; }
        public Animais? Animais { get; set; }

        [ForeignKey("UsuarioId")]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

    }
}
