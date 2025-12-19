using EmployeeLeaveManagement.Data;
using EmployeeLeaveManagement.DTOs;
using EmployeeLeaveManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagement.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly AppDbContext _context;

        public LeaveService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<LeaveRequestDto> GetAllLeaveRequests()
        {
            return _context.LeaveRequests
                .Include(l => l.Employee)
                .Select(l => new LeaveRequestDto
                {
                    LeaveRequestId = l.LeaveRequestId,
                    EmployeeName = l.Employee.FullName,
                    LeaveType = l.LeaveType,
                    Status = l.Status,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate
                })
                .ToList();
        }

        public void AddLeaveRequest(
            int employeeId,
            DateTime startDate,
            DateTime endDate,
            string leaveType
        )
        {
            var leave = new LeaveRequest
            {
                EmployeeId = employeeId,
                StartDate = startDate,
                EndDate = endDate,
                LeaveType = leaveType,
                Status = "Pending"
            };

            _context.LeaveRequests.Add(leave);
            _context.SaveChanges();
        }
        public void UpdateLeaveStatus(int leaveRequestId, string status)
        {
            var leave = _context.LeaveRequests.First(l => l.LeaveRequestId == leaveRequestId);
            leave.Status = status;
            _context.SaveChanges();
        }

    }
}
