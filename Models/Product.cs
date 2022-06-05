using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LWAApi.Models
{
    [Table("TblProducts")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int  Price { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
    }
}
