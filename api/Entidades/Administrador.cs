using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entidades;
public class Administrador
{
    // primary key
    [Key]
    // auto increment
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } = default!;
    [Required]
    [StringLength(255)]

    public string Email { get; set; } = default!;
    [StringLength(50)]
    [Required]
    public string Senha { get; set; } = default!;
    [StringLength(10)]
    [Required]
    public string Perfil { get; set; } = default!;
}