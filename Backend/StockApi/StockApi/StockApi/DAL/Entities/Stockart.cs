using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockApi.DAL
{
    public class Stockart
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string MakeName { get; set; }
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public decimal StockPrice { get; set; }
        public decimal ScoreRating { get; set; }
        public string ImageUrl { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}
