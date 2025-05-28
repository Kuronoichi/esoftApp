using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esoftApp.Database.Models;

public class RealEstate
{
    [Key]
    public int Id { get; set; }

    [MaxLength(255)]
    public string? City { get; set; }

    [MaxLength(255)]
    public string? Street { get; set; }

    public int? House { get; set; }
    public int? Flat { get; set; }

    [Range(-90, 90)]
    public double? Latitude { get; set; }

    [Range(-180, 180)]
    public double? Longitude { get; set; }

    public int? Floor { get; set; }
    public int? Rooms { get; set; }
    public double? Area { get; set; }

    [ForeignKey("RealEstateType")]
    public int RealEstateTypeId { get; set; }
    public RealEstateType RealEstateType { get; set; }

    [ForeignKey("District")]
    public int DistrictId { get; set; }
    public District District { get; set; }
    
    public ICollection<Offer> Offers { get; set; }
}
