namespace back_3lb.Api.Orders.Contracts
{
    public class OrderResponseContract
    {
        public int Id { get; set; }
        public required int StudentId { get; set; }
        public required int ProductId { get; set; }

    }


}