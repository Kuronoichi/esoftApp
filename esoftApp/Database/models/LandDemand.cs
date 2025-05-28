using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esoftApp.Database.Models;

public class LandDemand
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Demand")]
    public int DemandId { get; set; }
    public Demand Demand { get; set; }

    [Range(0.1, double.MaxValue)]
    public double? MinArea { get; set; }

    [Range(0.1, double.MaxValue)]
    public double? MaxArea { get; set; }
}