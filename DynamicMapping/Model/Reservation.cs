using System.Xml.Serialization;

namespace DynamicMapping.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        
        public string? Name { get; set; }
        
        public string? BookingType { get; set; }

        public int Amount { get; set; }

    }
}
