using System.ComponentModel.DataAnnotations;
using PagedList;

namespace ThinkBridgeTest.Models
{
    public class Inventory
    {
        public int intId { get; set; }

        [Required(ErrorMessage = "Item Name is required.")]
        [StringLength(50, ErrorMessage = "You have reached your maximum limit of characters allowed")]
        public string strItemName { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(50, ErrorMessage = "You have reached your maximum limit of characters allowed")]
        public string strItemDescription { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public int intPrice { get; set; }

        public string CreatedDate { get; set; }
    }

    public class InventorySearch
    {
        //public int? Index { get; set; }
        public IPagedList<Inventory> List { get; set; }
    }

    public class SearchItem
    {
        public string Search { get; set; }
    }
}