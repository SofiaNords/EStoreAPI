using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EStoreAPI.Entities
{
    public class Product
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string ProductNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Range(1.00, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(100)]
        public string Category { get; set; }

        public bool IsDiscontinued { get; set; } = false;
    }
}
