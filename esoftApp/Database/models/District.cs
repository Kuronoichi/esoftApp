using System.ComponentModel.DataAnnotations;

namespace esoftApp.Database.Models;

public class District
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
    public string Area { get; set; }

    public ICollection<RealEstate> RealEstates { get; set; }
    public ICollection<Demand> Demands { get; set; }
}