namespace EmployeeLeaveManagement.DTOs
{
    public class EmployeeLeaveSummaryDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public int TotalLeavesTaken { get; set; }
        public int ApprovedLeaves { get; set; }
        public int PendingLeaves { get; set; }
    }
}
