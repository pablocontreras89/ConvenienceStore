using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConvenienceStore.API.Models
{
    [Index(nameof(UPC), IsUnique = true)]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [StringLength(15)]
        public required string UPC { get; set; }
        public required string Description { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
