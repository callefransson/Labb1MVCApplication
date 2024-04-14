using System.ComponentModel.DataAnnotations;

namespace Labb1MVCApplication.Models
{
    public class Employee
    {
        [Key]
        public Guid EmployeeId { get; set; }
        [Required(ErrorMessage = "Employee need a name")]
        [StringLength(50, ErrorMessage = "Name can't be longer then 50 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Employee need a name")]
        [StringLength(50, ErrorMessage = "Name can't be longer then 50 characters")]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public virtual ICollection<Leave>? Leaves { get; set; }
    }
}
