using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Database.Context;
using esoftApp.Database.Models;

namespace esoftApp.Services;

public class ValidationService
{
    private readonly MainService _mainService = new MainService();
    public ValidationResult ValidateClient(Client client)
    {
        if (string.IsNullOrWhiteSpace(client.Phone) && string.IsNullOrWhiteSpace(client.Email))
        {
            return new ValidationResult("Хотя бы одно из полей 'Телефон' или 'Email' должно быть указано");
        }
        
        if (!string.IsNullOrWhiteSpace(client.Email) && !IsValidEmail(client.Email))
        {
            return new ValidationResult("Указан некорректный email");
        }
        
        if (!string.IsNullOrWhiteSpace(client.Phone) && !IsValidPhone(client.Phone))
        {
            return new ValidationResult("Указан некорректный номер телефона");
        }

        return ValidationResult.Success;
    }

    public ValidationResult CanDeleteClient(Client client)
    {
        if ((client.Offers != null && client.Offers.Any()) || 
            (client.Demands != null && client.Demands.Any()) ||
            !(_mainService.GetAllOffers().Any(c => c.ClientId == client.Id)) ||
            !(_mainService.GetAllDemands().Any(c => c.ClientId == client.Id)))
        {
            return new ValidationResult("Нельзя удалить клиента, связанного с предложением или потребностью");
        }

        return ValidationResult.Success;
    }
    
    public ValidationResult ValidateAgent(Agent agent)
    {
        if (string.IsNullOrWhiteSpace(agent.LastName) || 
            string.IsNullOrWhiteSpace(agent.FirstName) || 
            string.IsNullOrWhiteSpace(agent.MiddleName))
        {
            return new ValidationResult("Фамилия, имя и отчество обязательны для заполнения");
        }
        
        if (agent.Commission.HasValue && (agent.Commission < 0 || agent.Commission > 100))
        {
            return new ValidationResult("Доля комиссии должна быть в диапазоне от 0 до 100");
        }

        return ValidationResult.Success;
    }

    public ValidationResult CanDeleteAgent(Agent agent)
    {
        if ((agent.Offers != null && agent.Offers.Any()) || 
            (agent.Demands != null && agent.Demands.Any()) ||
            !(_mainService.GetAllOffers().Any(c => c.AgentId == agent.Id)) ||
            !(_mainService.GetAllDemands().Any(c => c.AgentId == agent.Id)))
        {
            return new ValidationResult("Нельзя удалить клиента, связанного с предложением или потребностью");
        }

        return ValidationResult.Success;
    }
    
    public ValidationResult ValidateRealEstate(RealEstate realEstate)
{
    // Проверка обязательных полей
    if (string.IsNullOrWhiteSpace(realEstate.City))
    {
        return new ValidationResult("Город обязателен для заполнения");
    }
    
    if (string.IsNullOrWhiteSpace(realEstate.Street))
    {
        return new ValidationResult("Улица обязательна для заполнения");
    }
    
    if (realEstate.House == null || realEstate.House <= 0)
    {
        return new ValidationResult("Номер дома должен быть положительным числом");
    }
    
    // Проверка типа недвижимости
    if (realEstate.RealEstateTypeId <= 0)
    {
        return new ValidationResult("Тип недвижимости обязателен для выбора");
    }
    
    // Проверка района
    if (realEstate.DistrictId <= 0)
    {
        return new ValidationResult("Район обязателен для выбора");
    }
    
    // Проверка координат, если они указаны
    if (realEstate.Latitude.HasValue && (realEstate.Latitude < -90 || realEstate.Latitude > 90))
    {
        return new ValidationResult("Широта должна быть в диапазоне от -90 до 90");
    }
    
    if (realEstate.Longitude.HasValue && (realEstate.Longitude < -180 || realEstate.Longitude > 180))
    {
        return new ValidationResult("Долгота должна быть в диапазоне от -180 до 180");
    }
    
    // Проверка дополнительных параметров
    if (realEstate.Flat.HasValue && realEstate.Flat <= 0)
    {
        return new ValidationResult("Номер квартиры должен быть положительным числом");
    }
    
    if (realEstate.Floor.HasValue && realEstate.Floor <= 0)
    {
        return new ValidationResult("Этаж должен быть положительным числом");
    }
    
    if (realEstate.Rooms.HasValue && realEstate.Rooms <= 0)
    {
        return new ValidationResult("Количество комнат должно быть положительным числом");
    }
    
    if (realEstate.Area.HasValue && realEstate.Area <= 0)
    {
        return new ValidationResult("Площадь должна быть положительным числом");
    }

    return ValidationResult.Success;
}

    public ValidationResult CanDeleteRealEstate(RealEstate realEstate)
    {
        if ((realEstate.Offers != null && realEstate.Offers.Any()) ||
            !(_mainService.GetAllOffers().Any(o => o.RealEstateId == realEstate.Id)))
        {
            return new ValidationResult("Нельзя удалить объект недвижимости, связанный с предложением");
        }

        return ValidationResult.Success;
    }

    
    

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private bool IsValidPhone(string phone)
    {
        return Regex.IsMatch(phone, @"^\+?[0-9\s\-\(\)]{5,20}$");
    }
}