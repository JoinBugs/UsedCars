using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace UsedCars.Models
{
    public class Car
    {
        [JsonProperty()]
        [Required()]
        public int Id { get; set; }
        
        [JsonProperty()]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [Required()]
        public string Model { get; set; }

        [JsonProperty()]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string Description { get; set; }

        [JsonProperty()]
        [Range(0, 9000)]
        [Required()]
        public int Year { get; set; }

        [JsonProperty()]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [Required()]
        public string Brand { get; set; }

        [JsonProperty()]
        [Range(0, 1000000)]
        [Required()]
        public int Kilometers { get; set; }

        [JsonProperty()]
        [Range(0, 10000000)]
        [Required()]
        public decimal Price { get; set; }
    }
}