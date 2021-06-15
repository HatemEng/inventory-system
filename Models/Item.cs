using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string TradeName { get; set; }
        public string ScientificName { get; set; }
        public string Category { get; set; }
        public string Manufactures { get; set; }
        public string DangerousLevel { get; set; }
        public string Packing { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        
    }
}