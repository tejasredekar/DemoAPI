using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Model
{
    public class ProductCateCTO
    {
        [Key]
        public int CID { get; set; }
        [Required]
        public string? CName { get; set; }
        [Required]
        [Range(20000, 1000000)]
        public double Price { get; set; }

        public string CategoryName { get; set; }
        public int CateId { get; set; }
    }
}
