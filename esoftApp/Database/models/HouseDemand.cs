using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esoftApp.Database.Models;

public class HouseDemand
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

    [Range(1, int.MaxValue)]
    public int? MinRoomsCount { get; set; }

    [Range(1, int.MaxValue)]
    public int? MaxRoomsCount { get; set; }

    [Range(2, int.MaxValue)]
    public int? MinFloorCount { get; set; }

    [Range(2, int.MaxValue)]
    public int? MaxFloorCount { get; set; }
}