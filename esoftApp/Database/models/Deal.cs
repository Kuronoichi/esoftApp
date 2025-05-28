using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esoftApp.Database.Models;

public class Deal
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Demand")]
    public int DemandId { get; set; }
    public Demand Demand { get; set; }

    [ForeignKey("Offer")]
    public int OfferId { get; set; }
    public Offer Offer { get; set; }

    [Range(0.1, 99.9)]
    public double CompanyDeductions { get; set; }

    [Range(0.1, 99.9)]
    public double BuyerAgentDeductions { get; set; }

    [Range(0.1, 99.9)]
    public double SellerAgentDeductions { get; set; }
}