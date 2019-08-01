using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockApi.DAL.TranslationTables
{
    public class StockartForCreationDto
    {

        public string MakeName { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal StockPrice { get; set; }
        public decimal ScoreRating { get; set; }
        public string ImageUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
