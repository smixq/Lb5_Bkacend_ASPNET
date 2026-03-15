using System.ComponentModel.DataAnnotations;

namespace back_3lb.Api.Products.Contracts
{
    public class ProductUpdateInsertContract
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
