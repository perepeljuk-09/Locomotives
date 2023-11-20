using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locomotives.Domain.Entities
{
	public class Employee
	{
		public int EmployeeId { get; set; }
		public string? Department { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Patronomyc { get; set; }
		public DateTime Birthday { get; set; }
		public DateTime DateOfEmployment { get; set; }
		public int Salary { get; set; }
	}
}
