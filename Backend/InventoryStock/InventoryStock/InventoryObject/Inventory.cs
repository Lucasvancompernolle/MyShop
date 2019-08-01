using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryStock.InventoryObject
{
    public class Inventory
    {
        [Key]
        public int productId { get; set; }
        [Required]
        [MaxLength(50)]
        public string productName { get; set; }
        [Required]
        public string productCode { get; set; }

        public DateTime releaseDate { get; set; }
        [Required]
        [MaxLength(500)]
        public string description { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public decimal starRating { get; set; }

        public string imageUrl { get; set; }
       
    }
}
