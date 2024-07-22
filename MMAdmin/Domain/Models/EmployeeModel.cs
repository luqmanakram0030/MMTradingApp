namespace MMAdmin.Domain.Models;

public class EmployeeModel
{
    public Guid UserId { get; set; }

    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }

    // Use the Location class
    public Location Location { get; set; }
    public bool IsPresent { get; set; }

    public EmployeeModel()
    {
        FullName = string.Empty;
        Email = string.Empty;
        PhoneNumber = string.Empty;
        Password = string.Empty;
        Location = new Location();
        IsPresent = false;
    }
}

public class Location
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public Location()
    {
        Latitude = 0.0;
        Longitude = 0.0;
    }

    public Location(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public override string ToString()
    {
        return $"Latitude: {Latitude}, Longitude: {Longitude}";
    }
}

