using System.ComponentModel.DataAnnotations;

namespace Tutorial6_17c.dto;

public class AddAnimal
{
    public int IdAnimal { get; set; }
    //[MinLength(3)]
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    [Required]
    [MaxLength(200)]
    public string? Description { get; set; }
    [Required]
    [MaxLength(200)]
    public string Category { get; set; }
    [Required]
    [MaxLength(200)]
    public string Area { get; set;}
}