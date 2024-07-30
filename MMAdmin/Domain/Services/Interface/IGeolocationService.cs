namespace MMAdmin.Domain.Services.Interface;

public interface IGeolocationService
{
    Task<string> GetCountryName();
    Task<Microsoft.Maui.Devices.Sensors.Location> GetCurrentPositionAsync();
    void ShowErrorAsync(string message);
    void MoveMapToPosition(Microsoft.Maui.Controls.Maps.Map map, Microsoft.Maui.Devices.Sensors.Location position, double kilometers);
    Task DrawRouteAsync(Microsoft.Maui.Controls.Maps.Map map, Microsoft.Maui.Devices.Sensors.Location start, Microsoft.Maui.Devices.Sensors.Location end);
}
