using back_3lb.Models;

public class Order
{
    public int Id { get; set; }

    public required int StudentId { get; set; }
    public Student? Student { get; set; }

    public required int ProductId { get; set; }
    public Product? Product { get; set; }
}