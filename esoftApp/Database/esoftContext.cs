using esoftApp.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Context;

public class esoftContext(DbContextOptions<esoftContext> options) : DbContext(options)
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Agent> Agents { get; set; }
    public DbSet<RealEstateType> RealEstateTypes { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<RealEstate> RealEstates { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Demand> Demands { get; set; }
    public DbSet<HouseDemand> HouseDemands { get; set; }
    public DbSet<FlatDemand> FlatDemands { get; set; }
    public DbSet<LandDemand> LandDemands { get; set; }
    public DbSet<Deal> Deals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable(t => t.HasCheckConstraint(
                "CK_clients_phone_or_email_not_null",
                "Phone IS NOT NULL OR Email IS NOT NULL"));
        });
        
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.ToTable(t => t.HasCheckConstraint(
                "CK_agents_commision_allowed_value",
                "Commission > 0 AND Commission < 100"));
        });
        
        modelBuilder.Entity<RealEstate>(entity =>
        {
            entity.ToTable(t => 
            {
                t.HasCheckConstraint(
                    "CK_realestate_latitude_allowed_value",
                    "Latitude >= -90 AND Latitude <= 90");
                
                t.HasCheckConstraint(
                    "CK_realestate_longitude_allowed_value",
                    "Longitude >= -180 AND Longitude <= 180");
            });

            entity.HasOne(r => r.RealEstateType)
                .WithMany(t => t.RealEstates)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(r => r.District)
                .WithMany(d => d.RealEstates)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Offer>(entity =>
        {
            entity.ToTable(t => t.HasCheckConstraint(
                "CK_price_over_zero",
                "Price > 0"));

            entity.HasOne(o => o.Client)
                .WithMany(c => c.Offers)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(o => o.Agent)
                .WithMany(a => a.Offers)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(o => o.RealEstate)
                .WithMany(r => r.Offers)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Demand>(entity =>
        {
            entity.ToTable(t => 
            {
                t.HasCheckConstraint(
                    "CK_demands_min_price_over_zero",
                    "MinPrice > 0");
                
                t.HasCheckConstraint(
                    "CK_demands_max_price_over_zero",
                    "MaxPrice > 0");
            });

            entity.HasOne(d => d.District)
                .WithMany(d => d.Demands)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Client)
                .WithMany(c => c.Demands)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Agent)
                .WithMany(a => a.Demands)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.RealEstateType)
                .WithMany(t => t.Demands)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<HouseDemand>(entity =>
        {
            entity.ToTable(t => 
            {
                t.HasCheckConstraint(
                    "CK_house_demands_min_area_over_zero",
                    "MinArea > 0");
                
                t.HasCheckConstraint(
                    "CK_house_demands_max_area_over_zero",
                    "MaxArea > 0");
                
                t.HasCheckConstraint(
                    "CK_house_demands_min_rooms_count_over_zero",
                    "MinRoomsCount > 0");
                
                t.HasCheckConstraint(
                    "CK_house_demands_max_rooms_count_over_zero",
                    "MaxRoomsCount > 0");
                
                t.HasCheckConstraint(
                    "CK_house_demands_min_floor_count_over_one",
                    "MinFloorCount > 1");
                
                t.HasCheckConstraint(
                    "CK_house_demands_max_floor_count_over_one",
                    "MaxFloorCount > 1");
            });

            entity.HasOne(h => h.Demand)
                .WithOne(d => d.HouseDemand)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфигурация FlatDemand
        modelBuilder.Entity<FlatDemand>(entity =>
        {
            entity.ToTable(t => 
            {
                t.HasCheckConstraint(
                    "CK_flat_demands_min_area_over_zero",
                    "MinArea > 0");
                
                t.HasCheckConstraint(
                    "CK_flat_demands_max_area_over_zero",
                    "MaxArea > 0");
                
                t.HasCheckConstraint(
                    "CK_flat_demands_min_rooms_count_over_zero",
                    "MinRoomsCount > 0");
                
                t.HasCheckConstraint(
                    "CK_flat_demands_max_rooms_count_over_zero",
                    "MaxRoomsCount > 0");
                
                t.HasCheckConstraint(
                    "CK_flat_demands_min_floor_over_one",
                    "MinFloor > 1");
                
                t.HasCheckConstraint(
                    "CK_flat_demands_max_floor_over_one",
                    "MaxFloor > 1");
            });

            entity.HasOne(f => f.Demand)
                .WithOne(d => d.FlatDemand)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфигурация LandDemand
        modelBuilder.Entity<LandDemand>(entity =>
        {
            entity.ToTable(t => 
            {
                t.HasCheckConstraint(
                    "CK_land_demands_min_area_over_zero",
                    "MinArea > 0");
                
                t.HasCheckConstraint(
                    "CK_land_demands_max_area_over_zero",
                    "MaxArea > 0");
            });

            entity.HasOne(l => l.Demand)
                .WithOne(d => d.LandDemand)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфигурация Deal
        modelBuilder.Entity<Deal>(entity =>
        {
            entity.ToTable(t => 
            {
                t.HasCheckConstraint(
                    "CK_deal_company_deductions_allowed_value",
                    "CompanyDeductions > 0 AND CompanyDeductions < 100");
                
                t.HasCheckConstraint(
                    "CK_deal_buyer_agent_deductions_allowed_value",
                    "BuyerAgentDeductions > 0 AND BuyerAgentDeductions < 100");
                
                t.HasCheckConstraint(
                    "CK_deal_seller_agent_deductions_allowed_value",
                    "SellerAgentDeductions > 0 AND SellerAgentDeductions < 100");
                
                t.HasCheckConstraint(
                    "CK_deal_deductions_lower_hundred",
                    "CompanyDeductions + BuyerAgentDeductions + SellerAgentDeductions < 100");
            });

            entity.HasOne(d => d.Demand)
                .WithMany(d => d.Deals)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Offer)
                .WithMany(o => o.Deals)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}