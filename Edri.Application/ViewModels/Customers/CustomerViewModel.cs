using System;
using Edri.Domain.Entities;

namespace Edri.Application.ViewModels.Customers;

public sealed class CustomerViewModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid DistrictId { get; set; }
    public string DocumentType { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public string CustomerType { get; set; } = string.Empty;
    public string? BusinessName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static CustomerViewModel FromCustomer(Customer customer)
    {
        return new CustomerViewModel
        {
            Id = customer.Id,
            UserId = customer.UserId,
            DistrictId = customer.DistrictId,
            DocumentType = customer.DocumentType,
            DocumentNumber = customer.DocumentNumber,
            CustomerType = customer.CustomerType,
            BusinessName = customer.BusinessName,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Address = customer.Address,
            Phone = customer.Phone,
            Email = customer.Email,
            Status = customer.Status,
            RegistrationDate = customer.RegistrationDate,
            UpdatedAt = customer.UpdatedAt
        };
    }
}