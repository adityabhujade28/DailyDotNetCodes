using EmployeeLeaveManagement.Services;

namespace EmployeeLeaveManagement.Views
{
    public class LeaveView
    {
        private readonly ILeaveService _leaveService;

        public LeaveView(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        public void ShowAllLeaveRequests()
        {
            var leaves = _leaveService.GetAllLeaveRequests();

            Console.WriteLine(" Leave Requests ");
            foreach (var leave in leaves)
            {
                Console.WriteLine(
                    $"{leave.LeaveRequestId} | {leave.EmployeeName} | {leave.LeaveType} | {leave.Status} | {leave.StartDate:d} - {leave.EndDate:d}"
                );
            }
        }

        public void AddLeaveRequest()
        {
            Console.Write("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine()!);

            Console.Write("Enter Leave Type (Sick/Casual): ");
            string leaveType = Console.ReadLine()!;

            Console.Write("Enter Start Date (yyyy-mm-dd): ");
            DateTime start = DateTime.Parse(Console.ReadLine()!);

            Console.Write("Enter End Date (yyyy-mm-dd): ");
            DateTime end = DateTime.Parse(Console.ReadLine()!);

            _leaveService.AddLeaveRequest(employeeId, start, end, leaveType);
            Console.WriteLine("Leave request submitted (Pending).");
        }
    }
}
