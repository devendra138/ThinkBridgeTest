using System.Collections.Generic;
using System.Web.Http;
using ThinkBridgeTest.Repository;
using ThinkBridgeTest.Models;

namespace ThinkBridgeTest.Controllers
{
    public class InventoryController : ApiController
    {
        readonly InventoryRepository _inventoryRepository;

        public InventoryController()
        {
            _inventoryRepository = new InventoryRepository();
        }

        //Insert Inventory Data
        [HttpPost]
        [Route("api/InsertInventoryData", Name = "InsertInventoryData")]
        public IHttpActionResult InsertInventoryData(Inventory Model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Result = _inventoryRepository.InsertInventoryData(Model);
            return Ok(Result);
        }

        //Modify Inventory Data
        [HttpPost]
        [Route("api/ModifyInventoryData", Name = "ModifyInventoryData")]
        public IHttpActionResult ModifyInventoryData(Inventory Model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Result = _inventoryRepository.ModifyInventoryData(Model);
            return Ok(Result);
        }

        //Delete Inventory Data
        [HttpPost]
        [Route("api/DeleteInventoryData", Name = "DeleteInventoryData")]
        public IHttpActionResult DeleteInventoryData(Inventory Model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Result = _inventoryRepository.DeleteInventoryData(Model);
            return Ok(Result);
        }

        //Display Inventory Data
        [HttpPost]
        [Route("api/DisplayInventoryData", Name = "DisplayInventoryData")]
        public IEnumerable<Inventory> DisplayInventoryData()
        {
            var List = _inventoryRepository.DisplayInventoryData();
            return List;
        }

        //Search Inventory Data 
        [HttpPost]
        [Route("api/SearchInventory", Name = "SearchInventory")]
        public InventorySearch SearchInventory(SearchItem Search)
        {
            // Get All Appointment List
            var List = _inventoryRepository.SearchItem(Search);
            return List;
        }
    }
}
