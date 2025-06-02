using Database.Context;
using esoftApp.Database.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace esoftApp.Services;

public class MainService
{
    private readonly esoftContext _context;
    private readonly ValidationService _validationService;

    public MainService()
    {
        _context = new esoftContextFactory().CreateDbContext(null);
        _validationService = new ValidationService();
    }

    public string AddClient(string? FirstName, string? MiddleName, string? LastName, string? Email, string? Phone)
    {
        var newClient = new Client
        {
            FirstName = FirstName,
            MiddleName = MiddleName,
            LastName = LastName,
            Email = Email,
            Phone = Phone
        };

        var validationResult = _validationService.ValidateClient(newClient);
        if (validationResult != ValidationResult.Success)
        {
            return validationResult.ErrorMessage;
        }

        try
        {
            _context.Clients.Add(newClient);
            _context.SaveChanges();
            return string.Empty;
        }
        catch (Exception ex)
        {
            return $"Ошибка при сохранении клиента: {ex.Message}";
        }
    }
    
    public string UpdateClient(int clientId, string? FirstName, string? MiddleName, string? LastName, string? Email, string? Phone)
    {
        var existingClient = GetClientById(clientId);
        if (existingClient == null)
        {
            return "Клиент не найден";
        }

        existingClient.FirstName = FirstName;
        existingClient.MiddleName = MiddleName;
        existingClient.LastName = LastName;
        existingClient.Email = Email;
        existingClient.Phone = Phone;
        
        var validationResult = _validationService.ValidateClient(existingClient);
        if (validationResult != ValidationResult.Success)
        {
            return validationResult.ErrorMessage;
        }

        try
        {
            _context.Entry(existingClient).State = EntityState.Modified;
            _context.SaveChanges();
            return string.Empty;
        }
        catch (Exception ex)
        {
            return $"Ошибка при обновлении клиента: {ex.Message}";
        }
    }
    
    public string DeleteClient(int clientId)
    {
        var client = _context.Clients
            .Include(c => c.Offers)
            .Include(c => c.Demands)
            .FirstOrDefault(c => c.Id == clientId);

        if (client == null)
        {
            return "Клиент не найден";
        }
        
        var validationResult = _validationService.CanDeleteClient(client);
        if (validationResult != ValidationResult.Success)
        {
            return validationResult.ErrorMessage;
        }

        try
        {
            _context.Clients.Remove(client);
            _context.SaveChanges();
            return string.Empty;
        }
        catch (Exception ex)
        {
            return $"Ошибка при удалении клиента: {ex.Message}";
        }
    }
    
    public string CreateAgent(string lastName, string firstName, string middleName, int? commission)
        {
            var newAgent = new Agent
            {
                LastName = lastName,
                FirstName = firstName,
                MiddleName = middleName,
                Commission = commission
            };

            var validationResult = _validationService.ValidateAgent(newAgent);
            if (validationResult != ValidationResult.Success)
            {
                return validationResult.ErrorMessage;
            }

            try
            {
                _context.Agents.Add(newAgent);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return $"Ошибка при создании риэлтора: {ex.Message}";
            }
        }

        public string UpdateAgent(int agentId, string lastName, string firstName, string middleName, int? commission)
        {
            var existingAgent = _context.Agents.Find(agentId);
            if (existingAgent == null)
            {
                return "Риэлтор не найден";
            }

            existingAgent.LastName = lastName;
            existingAgent.FirstName = firstName;
            existingAgent.MiddleName = middleName;
            existingAgent.Commission = commission;

            var validationResult = _validationService.ValidateAgent(existingAgent);
            if (validationResult != ValidationResult.Success)
            {
                return validationResult.ErrorMessage;
            }

            try
            {
                _context.Entry(existingAgent).State = EntityState.Modified;
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return $"Ошибка при обновлении риэлтора: {ex.Message}";
            }
        }

        public string DeleteAgent(int agentId)
        {
            var agent = _context.Agents
                .Include(a => a.Offers)
                .Include(a => a.Demands)
                .FirstOrDefault(a => a.Id == agentId);

            if (agent == null)
            {
                return "Риэлтор не найден";
            }

            var validationResult = _validationService.CanDeleteAgent(agent);
            if (validationResult != ValidationResult.Success)
            {
                return validationResult.ErrorMessage;
            }

            try
            {
                _context.Agents.Remove(agent);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return $"Ошибка при удалении риэлтора: {ex.Message}";
            }
        }
        
        public string CreateRealEstate(RealEstate realEstate)
        {
            var validationResult = _validationService.ValidateRealEstate(realEstate);
            if (validationResult != ValidationResult.Success)
                return validationResult.ErrorMessage;

            try
            {
                _context.RealEstates.Add(realEstate);
                _context.SaveChanges();
                return "Объект недвижимости успешно создан";
            }
            catch (Exception ex)
            {
                return $"Ошибка при создании объекта недвижимости: {ex.Message}";
            }
        }

        public string UpdateRealEstate(RealEstate realEstate)
        {
            var validationResult = _validationService.ValidateRealEstate(realEstate);
            if (validationResult != ValidationResult.Success)
                return validationResult.ErrorMessage;

            try
            {
                _context.RealEstates.Update(realEstate);
                _context.SaveChanges();
                return "Объект недвижимости успешно обновлен";
            }
            catch (Exception ex)
            {
                return $"Ошибка при обновлении объекта недвижимости: {ex.Message}";
            }
        }

        public string DeleteRealEstate(int id)
        {
            try
            {
                var realEstate = _context.RealEstates
                    .Include(re => re.Offers)
                    .FirstOrDefault(re => re.Id == id);

                if (realEstate == null)
                    return "Объект недвижимости не найден";

                var canDeleteResult = _validationService.CanDeleteRealEstate(realEstate);
                if (canDeleteResult != ValidationResult.Success)
                    return canDeleteResult.ErrorMessage;

                _context.RealEstates.Remove(realEstate);
                _context.SaveChanges();
                return "Объект недвижимости успешно удален";
            }
            catch (Exception ex)
            {
                return $"Ошибка при удалении объекта недвижимости: {ex.Message}";
            }
        }

        public Agent? GetAgentById(int agentId)
        {
            return _context.Agents.Find(agentId);
        }

        public List<Agent> GetAllAgents()
        {
            return _context.Agents.ToList();
        }
        
        
    
    public Client? GetClientById(int clientId)
    {
        return _context.Clients.Find(clientId);
    }

    public Offer[] GetAllOffers()
    {
        return _context.Offers.ToArray();
    }

    public RealEstate[] GetAllRealEstates()
    {
        return _context.RealEstates.ToArray();
    }

    public Demand[] GetAllDemands()
    {
        return _context.Demands.ToArray();
    }
}