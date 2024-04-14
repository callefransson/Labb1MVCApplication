using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Labb1MVCApplication.Models
{
    public class Leave
    {
        [Key]
        public Guid LeaveId { get; set; }
        public string LeaveType { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public DateTime TimeWhenLeaveWasSet { get; set; } = DateTime.Now;
        // Foreign key property
        [ForeignKey("Employee")]
        public Guid FkEmployeeId { get; set; }

        // Navigation property for Employee
        public virtual Employee? Employee { get; set; }

    }
}
