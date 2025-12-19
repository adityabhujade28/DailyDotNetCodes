using EmployeeLeaveManagement.Services;

namespace EmployeeLeaveManagement.Views
{
    public class AdminView
    {
        private readonly ILeaveService _leaveService;

        public AdminView(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        public void ShowPendingLeaves()
        {
            var leaves = _leaveService.GetAllLeaveRequests()
                .Where(l => l.Status == "Pending");

            Console.WriteLine(" Pending Leave Requests ");
            foreach (var leave in leaves)
            {
                Console.WriteLine(
                    $"{leave.LeaveRequestId} | {leave.EmployeeName} | {leave.LeaveType} | {leave.StartDate:d} - {leave.EndDate:d}"
                );
            }
        }

        public void ApproveOrRejectLeave()
        {
            Console.Write("Enter Leave Request ID: ");
            int leaveId = int.Parse(Console.ReadLine()!);

            Console.Write("Approve or Reject (A/R): ");
            string input = Console.ReadLine()!.ToUpper();

            if (input == "A")
                _leaveService.UpdateLeaveStatus(leaveId, "Approved");
            else if (input == "R")
                _leaveService.UpdateLeaveStatus(leaveId, "Rejected");
            else
                Console.WriteLine("Invalid option.");

            Console.WriteLine("Leave status updated.");
        }
    }
}
