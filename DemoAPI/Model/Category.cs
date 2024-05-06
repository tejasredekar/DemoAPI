using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Model
{
    /// <summary>
    /// Category Class
    /// </summary>
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CID { get; set; }
        [Required]
        public string? CName { get; set; }
        [Required]
        [Range(1, 10)]
        public int CCapacity { get; set; } = 1;

        public virtual List<Product>? Product { get; set; }
    }
}
                