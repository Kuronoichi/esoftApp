using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esoftApp.Database.Models;

public class Offer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Price { get; set; }

    [ForeignKey("Client")]
    public int ClientId { get; set; }
    public Client Client { get; set; }

    [ForeignKey("Agent")]
    public int AgentId { get; set; }
    public Agent Agent { get; set; }

    [ForeignKey("RealEstate")]
    public int RealEstateId { get; set; }
    public RealEstate RealEstate { get; set; }
    
    public ICollection<Deal> Deals { get; set; }
}
