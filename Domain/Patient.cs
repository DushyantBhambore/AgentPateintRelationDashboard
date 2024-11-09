using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Patient
    {
        public int Id { get; set; }
        public string PatinetId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public string AgentId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string AddressLine1  { get; set; }
        public string AddressLine2 { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public string ZipCode { get; set; }
        public DateTime AppoinmentDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
