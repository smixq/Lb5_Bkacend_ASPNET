using System.ComponentModel.DataAnnotations;

namespace back_3lb.Api.Students.Contracts
{
    public class StudentUpdateInsertContract
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Specialization { get; set; }

        public DateOnly? BirthDate { get; set; }

    }
}
