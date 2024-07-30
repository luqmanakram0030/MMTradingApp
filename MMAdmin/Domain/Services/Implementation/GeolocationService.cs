using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Location = Microsoft.Maui.Devices.Sensors.Location;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace MMAdmin.Domain.Services.Implementation;

public class GeolocationService : IGeolocationService
{
    public async Task<string> GetCountryName()
    {
        try
        {
            string countryName = "";
            var position = await GetCurrentPositionAsync();
            if (position != null)
            {
                var placemarks = await Geocoding.Default.GetPlacemarksAsync(position.Latitude, position.Longitude);
                if (placemarks != null && placemarks.Any())
                {
                    var placemark = placemarks.FirstOrDefault();
                    countryName = placemark.CountryName;
                }
            }

            return countryName;
        }
        catch (Exception ex)
        {
            ShowErrorAsync($"Error getting location: {ex.Message}");
            return null;
        }
    }

    public async Task<Location> GetCurrentPositionAsync()
    {
        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(request);

            if (location != null)
            {
                return new Location(location.Latitude, location.Longitude);
            }
            else
            {
                ShowErrorAsync("Location not found");
                return default(Location);
            }
        }
        catch (Exception ex)
        {
            ShowErrorAsync($"Error getting location: {ex.Message}");
            return default(Location);
        }
    }
    public void MoveMapToPosition(Map map, Location position, double kilometers)
    {
        map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Microsoft.Maui.Maps.Distance.FromMeters(kilometers)));
    }

    public void ShowErrorAsync(string message)
    {
        ToastService.ShowToastAsync(message);
    }

    public async Task DrawRouteAsync(Map map, Location start, Location end)
    {
        try
        {
            var apiKey = "AIzaSyBiCLFWeI8W8gDzHKs5uQEBgnIlmih1DTs";
            var url = $"https://maps.googleapis.com/maps/api/directions/json?origin={start.Latitude},{start.Longitude}&destination={end.Latitude},{end.Longitude}&key={apiKey}";

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var directions = JsonConvert.DeserializeObject<Domain.Models.DirectionsResponse>(response);

                if (directions?.routes?.Count > 0)
                {
                    var route = directions.routes[0];
                    var steps = route.legs[0].steps;

                    // Clear existing map elements
                    map.MapElements.Clear();

                    // Draw polyline for the route
                    var points = Decode(route.overview_polyline.points);
                    var polyline = new Polyline
                    {
                        StrokeColor = Microsoft.Maui.Graphics.Colors.Black,
                        StrokeWidth = 5
                    };
                    foreach (var point in points)
                    {
                        polyline.Geopath.Add(point);
                    }
                    map.MapElements.Add(polyline);

                    // Display turn-by-turn directions (example)
                    foreach (var step in steps)
                    {
                        var instruction = step.html_instructions; 
                        var distance = step.distance.text;
                        var duration = step.duration.text; 
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error drawing route: {ex.Message}");
        }
    }

    public List<Location> Decode(string encodedPoints)
    {
        if (string.IsNullOrEmpty(encodedPoints))
            return null;

        var polylineChars = encodedPoints.ToCharArray();
        var polyline = new List<Location>();
        int index = 0;
        int currentLat = 0;
        int currentLng = 0;
        int next5Bits;
        int sum;
        int shifter;

        while (index < polylineChars.Length)
        {
            // Calculate latitude
            sum = 0;
            shifter = 0;
            do
            {
                next5Bits = polylineChars[index++] - 63;
                sum |= (next5Bits & 31) << shifter;
                shifter += 5;
            } while (next5Bits >= 32 && index < polylineChars.Length);

            if (index >= polylineChars.Length)
                break;

            currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

            // Calculate longitude
            sum = 0;
            shifter = 0;
            do
            {
                next5Bits = polylineChars[index++] - 63;
                sum |= (next5Bits & 31) << shifter;
                shifter += 5;
            } while (next5Bits >= 32 && index < polylineChars.Length);

            if (index >= polylineChars.Length && next5Bits >= 32)
                break;

            currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

            double latitude = Convert.ToDouble(currentLat) / 1E5;
            double longitude = Convert.ToDouble(currentLng) / 1E5;

            polyline.Add(new Location(latitude, longitude));
        }

        return polyline;
    }
}
