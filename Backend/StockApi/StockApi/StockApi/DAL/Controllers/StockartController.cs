using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StockApi.DAL;
using StockApi.DAL.TranslationTables;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Drawing;
using System.IO;

namespace StockApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class StockArtController : ControllerBase
    {
        public ILogger<StockArtController> _logger { get; private set; }

        private readonly IStockRepository stockRepository;

        public StockArtController(IStockRepository stockRepository, ILogger<StockArtController> logger)
        {

            _logger = logger;
            this.stockRepository = stockRepository;

            //stockRepository.AddStockArt(new Stockart() { Code = "123", Description = "blabla", ImageUrl = "http...", MakeName = "test",
            //                                             ScoreRating = 3.5M, StockPrice = 5.50M });
            //stockRepository.Save();
        }

        //GET api/stockarts
        [HttpGet("stockarts")]
        public ActionResult<IEnumerable<StockArtDto>> GetStockarts()
        {
            var stockartsFromDB = stockRepository.GetStockArts();
            var returnValue = Mapper.Map<IEnumerable<StockArtDto>>(stockartsFromDB);
            return Ok(returnValue);
        }

        [HttpPost("stockarts/excel")]
        public ActionResult GetStockartCollectionExcel([FromBody] IEnumerable<int> StockCollectionByID)
        {
            byte[] fileContents;
            List<Stockart> stockartsFromDB = new List<Stockart>();

            foreach (var id in StockCollectionByID)
            {
                stockartsFromDB.Add(stockRepository.GetStockById(id));
            }

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Communication";
            ws.Cells["B1"].Value = "Com1";

            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Report1";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A5"].Value = "Id";
            ws.Cells["B5"].Value = "MakeName";
            ws.Cells["C5"].Value = "Code";
            ws.Cells["D5"].Value = "Description";
            ws.Cells["E5"].Value = "ReleaseDate";
            ws.Cells["F5"].Value = "StockPrice";
            ws.Cells["G5"].Value = "ImageUrl";
            ws.Cells["H5"].Value = "ScoreRating";

            ws.Cells["A5:H5"].AutoFitColumns();
            ws.Cells["A5:H5"].Style.Font.Bold = true;

            var sortedListStockart = from item in stockartsFromDB
                         orderby item.Id ascending
                         select item;

            int rowStart = 6;

            foreach (var item in sortedListStockart)
            {
                if (item.ScoreRating >= 4)
                {
                    ws.Cells[string.Format("A{0}:H{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[string.Format("A{0}:H{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("red")));

                }

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Id;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.MakeName;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Code;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Description;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.ReleaseDate;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.StockPrice;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.ScoreRating;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.ImageUrl;
                rowStart++;
            }

           fileContents = pck.GetAsByteArray();


            if (fileContents == null || fileContents.Length == 0)
            {
                return NotFound();
            }

            //string path = Path.GetDirectoryName("C:\\Users\\lucas\\Desktop\\testangular\\Backend\\StockApi\\StockApi\\StockApi\\");
            //FileInfo fi1 = new FileInfo(path + $"stockart_{string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now)}.xlsx");

            //pck.SaveAs(fi1);

            //return Ok(fi1.FullName);

            return File(
                fileContents: fileContents,
                contentType: "application/ms-excel",
                fileDownloadName: $"stockart_{string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now)}.xlsx"
            );

        }

        //// GET api/stockarts?code={code}
        //[HttpGet("stockarts/code={code}")]
        //public ActionResult<IEnumerable<Stockart>> GetStockartsByCode(string code)
        //{
        //    var stockartsFromDB = stockRepository.GetStockByCode(code);
        //    var returnValue = Mapper.Map<IEnumerable<StockArtDto>>(stockartsFromDB);
        //    return Ok(returnValue);
        //}

        //GET api/stockarts/{id}
        [HttpGet("stockarts/{id}")]
        public ActionResult<IEnumerable<Stockart>> GetStockArt(int id)
        {
            Stockart stockart = stockRepository.GetStockById(id);


            var returnValue = Mapper.Map(stockart, typeof(Stockart), typeof(StockArtDto));
            return Ok(returnValue);

        }

        //[HttpPost(Name = "CreateStockart")]
        //public IActionResult CreateStockart([FromBody] StockArtDto stockart)
        //{
        //    if (stockart == null)
        //    {
        //        return BadRequest();
        //    }

        //    var StockArtEntity = Mapper.Map<Stockart>(stockart);
        //    stockRepository.AddStockArt(StockArtEntity);
        //    if (!stockRepository.Save())
        //    {
        //        //global exception handler will catch the error
        //        throw new ApplicationException("Creating an author failed on save");
        //        //return StatusCode(500, "A problem happened with handling your request");
        //    }

        //    var authorToReturn = Mapper.Map<StockArtDto>(StockArtEntity);


        //    return Ok(authorToReturn);

        //    //GetAuthor is name given to method GetAuthor
        //    //Id is the Id for Author
        //    //authorToReturn will be serialised in the body
        //    //return CreatedAtRoute("GetAuthor", new { id = authorToReturn.Id }, authorToReturn);
        //}

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost]
        public IActionResult CreateStockCollection([FromBody] IEnumerable<StockartForCreationDto> StockCollection)
        {
            if (StockCollection == null)
            {
                return BadRequest();
            }

            var stockEntities = Mapper.Map<IEnumerable<Stockart>>(StockCollection);

            foreach (var stock in stockEntities)
            {
                stockRepository.AddStockArt(stock);
            }

            if (!stockRepository.Save())
            {
                throw new Exception("Creating an author collection failed on save.");
            }

            var stockCollectionToReturn = Mapper.Map<IEnumerable<StockArtDto>>(stockEntities);

            return Ok(stockCollectionToReturn);

        }
    }
}
