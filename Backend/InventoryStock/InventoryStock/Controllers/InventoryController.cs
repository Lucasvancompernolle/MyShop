using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryStock.InventoryObject;
using InventoryStock.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryStock.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;

        public ValuesController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        // GET api/values
        [HttpGet("{code}")]
        public ActionResult<IEnumerable<Inventory>> Get(string code)
        {
            IEnumerable<Inventory> authorsFromRepo = _inventoryRepository.GetInventory(code);

            return Ok(authorsFromRepo);

        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

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
    }
}
