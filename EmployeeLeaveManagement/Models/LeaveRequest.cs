namespace EmployeeLeaveManagement.Models
{
    public class LeaveRequest
    {
        public int LeaveRequestId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string LeaveType { get; set; } = null!;
        public string Status { get; set; } = null!;

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;
    }
}
