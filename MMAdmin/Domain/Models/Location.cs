using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAdmin.Domain.Models
{
    public class Location
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }
        [JsonProperty("lng")]
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

    public class Place
    {
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class PlaceSearchResponse
    {
        [JsonProperty("predictions")]
        public List<Place> Predictions { get; set; }
    }
    public class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }
        public Viewport viewport { get; set; }
    }

    public class PlaceDetails
    {
        [JsonProperty("formatted_address")]
        public string Address { get; set; }
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
    }

    public class PlaceDetailsResponse
    {
        [JsonProperty("result")]
        public PlaceDetails Result { get; set; }
    }
    public class LocationModel
    {
        public string PlaceName { get; set; }
        public string Formatted_Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public List<Route> routes { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class OpeningHours
    {
        public bool open_now { get; set; }
    }

    public class Photo
    {
        public int height { get; set; }
        public List<string> html_attributions { get; set; }
        public string photo_reference { get; set; }
        public int width { get; set; }
    }

    public class PlusCode
    {
        public string compound_code { get; set; }
        public string global_code { get; set; }
    }

    public class Result
    {
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string icon { get; set; }
        public string icon_background_color { get; set; }
        public string icon_mask_base_uri { get; set; }
        public string name { get; set; }
        public string place_id { get; set; }
        public string reference { get; set; }
        public List<string> types { get; set; }
        public List<Photo> photos { get; set; }
        public string business_status { get; set; }
        public OpeningHours opening_hours { get; set; }
        public PlusCode plus_code { get; set; }
        public double? rating { get; set; }
        public int? user_ratings_total { get; set; }
    }

    public class Root
    {
        public List<object> html_attributions { get; set; }
        public string next_page_token { get; set; }
        public List<Result> results { get; set; }
        public string status { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }


    public class Route
    {
        public List<Leg> legs { get; set; }
        public OverviewPolyline overview_polyline { get; set; }
    }

    public class OverviewPolyline
    {
        public string points { get; set; }
    }
    public class DirectionsResponse
    {
        public string status { get; set; }
        public List<Route> routes { get; set; }
    }

    public class Leg
    {
        public List<Step> steps { get; set; }
    }

    public class Step
    {
        public string html_instructions { get; set; }
        public Distance duration { get; set; }
        public Distance distance { get; set; }
    }


    public class Distance
    {
        public string text { get; set; }
    }

}
