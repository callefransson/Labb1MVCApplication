namespace Labb1MVCApplication.Models
{
    public class FilterLeaveByDateModel
    {
       public enum Months
        {
            January = 0,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }
        public List<Leave> Leaves {  get; set; }
        public string FilterByDate { get; set; }

    }
}
