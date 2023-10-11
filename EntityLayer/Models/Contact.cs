﻿using CoreLayer.Entities.Concrete;

namespace EntityLayer.Models;

public class Contact :Entity
{
    public DateTime CreatedDate { get; set; }
    public string Email { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CompanyName { get; set; }
    public string? CompanySite { get; set; }
    public string Message { get; set; }
}
