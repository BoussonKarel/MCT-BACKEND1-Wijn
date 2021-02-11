using System;

namespace MCT_BACKEND1_Wijn.Models
{
    public class Wine
    {
        public int WineId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public string Color { get; set; } 
        public decimal Price { get; set; }
        public string Grapes { get; set; }
    }
}
