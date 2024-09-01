using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entidades;
public class Veiculo
{
    // primary key
    [Key]
    // auto increment
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } = default!;
    [Required]
    [StringLength(50)]

    public string Nome { get; set; } = default!;
    [StringLength(50)]
    [Required]
    public string Marca { get; set; } = default!;
    [StringLength(10)]
    [Required]
    public int Ano { get; set; } = default!;
}