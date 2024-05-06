using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoAPI.CustomAttributes;
namespace DemoAPI.Model
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PID { get; set; }
        [Required]
        public string? PName { get; set; }

        [Required]
        [Range(20000, 1000000)]
        [Multipleof500]
        public double price { get; set; }
        public int? CategoryCID { get; set; }

        [ForeignKey("CategoryCID")]
        public virtual Category?Category { get; }
    }
}
