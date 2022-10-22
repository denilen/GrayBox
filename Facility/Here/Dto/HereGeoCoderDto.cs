using Newtonsoft.Json;

namespace ConsoleApp.Here.Dto
{
    public class HereGeoCoderDto
    {
        [JsonProperty("title")] 
        public string Title { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("resultType")]
        public string ResultType { get; set; }
        
        [JsonProperty("localityType")]
        public string LocalityType { get; set; }
        
        [JsonProperty("address")]
        public AddressDto Address { get; set; }

        [JsonProperty("position")] 
        public PositionDto Position { get; set; }
    }

    public class AddressDto
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("countryName")]
        public string CountryName { get; set; }

        [JsonProperty("stateCode")]
        public string StateCode { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
        
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }
        
        [JsonProperty("houseNumber")]
        public string HouseNumber { get; set; }
    }

    public class PositionDto
    {
        [JsonProperty("lat")] 
        public double Lat { get; set; }

        [JsonProperty("lng")] 
        public double Lng { get; set; }
    }
}