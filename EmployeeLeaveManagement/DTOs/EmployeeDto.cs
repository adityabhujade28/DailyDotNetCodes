using System;
using System.Collections.Generic;

namespace EmployeeLeaveManagement.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime JoinedOn { get; set; }
        public string DepartmentName { get; set; } = null!;
    }
}
