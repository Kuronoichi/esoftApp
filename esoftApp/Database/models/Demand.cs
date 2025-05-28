using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esoftApp.Database.Models;

public class Demand
{
    [Key]
    public int Id { get; set; }

    [Range(1, int.MaxValue)]
    public int? MinPrice { get; set; }

    [Range(1, int.MaxValue)]
    public int? MaxPrice { get; set; }

    [MaxLength(255)]
    public string? City { get; set; }

    [MaxLength(255)]
    public string? Street { get; set; }

    [MaxLength(255)]
    public string? House { get; set; }
    
    [MaxLength(255)]
    public string? Flat { get; set; }

    [ForeignKey("District")]
    public int DistrictId { get; set; }
    public District District { get; set; }

    [ForeignKey("Client")]
    public int ClientId { get; set; }
    public Client Client { get; set; }

    [ForeignKey("Agent")]
    public int AgentId { get; set; }
    public Agent Agent { get; set; }

    [ForeignKey("RealEstateType")]
    public int RealEstateTypeId { get; set; }
    public RealEstateType RealEstateType { get; set; }
    
    public HouseDemand? HouseDemand { get; set; }
    public FlatDemand? FlatDemand { get; set; }
    public LandDemand? LandDemand { get; set; }
    public ICollection<Deal> Deals { get; set; }
}

    