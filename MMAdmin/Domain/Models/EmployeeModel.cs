﻿namespace MMAdmin.Domain.Models;

public class EmployeeModel
{
    public Guid UserId { get; set; }

    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public EmployeeModel()
    {
        FullName = string.Empty;
        Email = string.Empty;
        PhoneNumber = string.Empty;
        Password = string.Empty;
    }
}

