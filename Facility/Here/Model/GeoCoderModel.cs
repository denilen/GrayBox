namespace Facility.Here.Model
{
    public class GeoCoderModel
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public string FullAddress { get; set; }
        public string Address { get; set; }
        public string ResultType { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}