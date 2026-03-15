using System.ComponentModel.DataAnnotations;

namespace back_3lb.Api.Orders.Contracts
{
    public class OrderUpdateInsertStudentContract
    {
        [Required]
        public required int StudentId { get; set; }
        [Required]
        public required int ProductId { get; set; }
    }
}

