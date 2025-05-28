using System.ComponentModel.DataAnnotations;

namespace esoftApp.Database.Models;

public class Agent
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(255)]
    public string MiddleName { get; set; }

    [Required]
    [MaxLength(255)]
    public string LastName { get; set; }

    [Range(1, 99)]
    public int? Commission { get; set; }
    
    public ICollection<Offer> Offers { get; set; }
    public ICollection<Demand> Demands { get; set; }
}