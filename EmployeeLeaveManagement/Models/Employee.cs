namespace EmployeeLeaveManagement.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime JoinedOn { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
    }
}
