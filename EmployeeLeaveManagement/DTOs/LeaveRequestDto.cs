namespace EmployeeLeaveManagement.DTOs
{
    public class LeaveRequestDto
    {
        public int LeaveRequestId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string LeaveType { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
