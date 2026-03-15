namespace back_3lb.Api.Students.Contracts
{
    public class StudentResponseContract
    {

        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Specialization { get; set; }
        public DateOnly? BirthDate { get; set; }

    }
}
