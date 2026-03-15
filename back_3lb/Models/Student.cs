namespace back_3lb.Models
{
    public class Student
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Specialization { get; set; }
        public DateOnly? BirthDate { get; set; }

    }
}