using System;
namespace mP13
{
    public class Employee
    {
        public uint EmployeeID { get; }
        public string Name { get; set; }
        public double Salary { get; set; }

        public Employee(uint employeeID, string employeeName, double salary)
        {
            EmployeeID = employeeID;
            Name = employeeName;
            Salary = salary;
        }
    }
}
