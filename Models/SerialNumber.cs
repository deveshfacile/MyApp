using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models
{
    public class SerialNumber
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }


       

        public Item? Item { get; set; }
    }
}
