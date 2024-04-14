namespace Labb1MVCApplication.Models
{
    public class SearchLeaveForEmployee
    {
        public Employee? Employee { get; set; }
        public List<Leave>? Leaves { get; set; }
        public bool HasActiveLeave { get; set; }

    }
}
