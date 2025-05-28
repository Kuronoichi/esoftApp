using System.ComponentModel.DataAnnotations;

namespace esoftApp.Database.Models;

public class RealEstateType
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
    
    public ICollection<RealEstate> RealEstates { get; set; }
    public ICollection<Demand> Demands { get; set; }
}