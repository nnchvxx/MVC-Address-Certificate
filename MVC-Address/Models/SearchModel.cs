using System;
namespace MVC_Address.Models
{
    public class SearchModel
    {
        public int Size { get; set; }
        public int From { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string Local_authority { get; set; }
        public string Constituency { get; set; }
        public List<string> Floor_area { get; set; }
        public List<string> Energy_band { get; set; }
        public int FromMonth { get; set; } = 1;
        public int FromYear { get; set; } = 2008;
        public int ToMonth { get; set; } = 12;
        public int ToYear { get; set; } = 2023;
    }
}

