using System.ComponentModel.DataAnnotations;

namespace Tutorial6_17c.dto;

public class AddAnimal
{
    //[Required]
    [MinLength(3)]
    [MaxLength(200)]
    public string Name { get; set; }
    public string? Description { get; set; }

}