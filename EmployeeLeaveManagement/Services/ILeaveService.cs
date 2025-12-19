using EmployeeLeaveManagement.DTOs;

public interface ILeaveService
{
    IEnumerable<LeaveRequestDto> GetAllLeaveRequests();

    void AddLeaveRequest(int employeeId, DateTime startDate, DateTime endDate, string leaveType);

    void UpdateLeaveStatus(int leaveRequestId, string status);
}
