using System;
using System.Collections.Generic;

namespace mP13
{
    class Program
    {
        static LinkedList<Employee> employeeLinkedList = new LinkedList<Employee>((employee1, employee2) => employee1.EmployeeID > employee2.EmployeeID);

        static void Main(string[] args)
        {
            bool exit = false;
            char userInput;
            List<Employee> employees = new List<Employee>();

            while(!exit)
            {
                Console.Clear();
                Console.WriteLine("EMPLOYEE MANAGER\n------------\nWhat would you like to do?" +
                    "\n1. Add a new employee " +
                    "\n2. Delete an employee" +
                    "\n3. View all employeees " +
                    "\n4. Display the number of employees " +
                    "\n5. Search for a specific employee" +
                    "\n6. Press anything else to exit");

                userInput = Console.ReadKey().KeyChar;

                switch(userInput)
                {
                    case '1':
                        AddEmployee();
                        break;
                    case '2':
                        DeleteEmployeeAtID();
                        break;
                    case '3':
                        ViewAllEmployees();
                        break;
                    case '4':
                        DisplayNumEmployees();
                        break;
                    case '5':
                        SearchByID();
                        break;
                    default:
                        exit = true;
                        break;
                }
            }
        }

        public static void AddEmployee()
        {
            string name;
            uint employeeID;
            double salary;
            Node<Employee> lastEmployee;

            Console.Clear();
            Console.WriteLine("Enter the employee's name:");
            name = Console.ReadLine();
            Console.WriteLine("Enter the employees salary:");
            salary = GetInputDouble();

            lastEmployee = employeeLinkedList.GetTail();

            if(lastEmployee == null)
            {
                employeeID = 0;
            }
            else
            {
                employeeID = lastEmployee.Data.EmployeeID + 1;
            }

            employeeLinkedList.AddNode(new Employee(employeeID, name, salary));
            Console.WriteLine("The new employee has been added! (Press ENTER to continue).");
            Console.ReadLine();
        }

        public static void DeleteEmployeeAtID()
        {
            uint employeeID;
            bool userDeleted;

            Console.Clear();
            Console.WriteLine("Enter the ID of the employee you'd like to delete: ");
            employeeID = GetInput("ID must be a positive integer");
            userDeleted = employeeLinkedList.Delete(node => node.Data.EmployeeID == employeeID);

            if (userDeleted)
            {
                Console.WriteLine("This employee has been successfully deleted. (Press ENTER to continue).");
            }
            else
            {
                Console.WriteLine("No employee with that ID exists. (Press ENTER to continue).");
            }

            Console.ReadLine();
        }

        public static void SearchByID()
        {
            uint employeeID;
            Node<Employee> employeeNode;

            Console.Clear();
            Console.WriteLine("Enter the ID of the employee: ");
            employeeID = GetInput();

            employeeNode = employeeLinkedList.QueryList((employeeNode) => employeeNode.Data.EmployeeID == employeeID);


            if(employeeNode == null)
            {
                Console.WriteLine("There is no employee with that ID. (Press ENTER to continue)");
            }
            else
            {
                Console.WriteLine($"The employee with ID {employeeID} is: \nEmployee: {employeeNode.Data.Name}\nID: {employeeNode.Data.EmployeeID}\nSalary: {employeeNode.Data.Salary}\n");
            }

            Console.WriteLine("Press ENTER to continue.");
            Console.ReadLine();
        }

        public static void ViewAllEmployees()
        {
            Console.Clear();
            employeeLinkedList.PrintList((node) => Console.WriteLine($"Employee: {node.Data.Name}\nID: {node.Data.EmployeeID}\nSalary: {node.Data.Salary}\n"));
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }

        public static void DisplayNumEmployees()
        {
            Console.Clear();
            Console.WriteLine($"There are currently {employeeLinkedList.GetLength()} employees in the list. (Press ENTER to continue).");
            Console.ReadKey();
        }

        public static uint GetInput(string errorMessage = "")
        {
            uint input;

            while(true)
            {
                try
                {
                    input = Convert.ToUInt32(Console.ReadLine());
                }
                catch(Exception e)
                {
                    if(errorMessage == "")
                    {
                        Console.WriteLine(e.Message + " (Press ENTER to continue).");
                    }
                    else
                    {
                        Console.WriteLine(errorMessage + " (Press ENTER to continue).");
                    }
                    
                    Console.ReadLine();
                    ClearLines(3);
                    continue;
                }

                ClearLines(2);
                return input;
            }
        }

        public static double GetInputDouble(string errorMessage = "")
        {
            double input;

            while (true)
            {
                try
                {
                    input = Convert.ToDouble(Console.ReadLine());
                }
                catch (Exception e)
                {
                    if (errorMessage == "")
                    {
                        Console.WriteLine(e.Message + " (Press ENTER to continue).");
                    }
                    else
                    {
                        Console.WriteLine(errorMessage + " (Press ENTER to continue).");
                    }

                    Console.ReadLine();
                    ClearLines(3);
                    continue;
                }

                ClearLines(2);
                return input;
            }
        }

        public static void ClearLines(int numLines)
        {
            for (int i = 0; i < numLines; i++)
            {
                //Move the cursor up
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                //Replace whatever was on that line with spaces
                Console.Write(new string(' ', Console.BufferWidth));
                //Move the cursor up
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
        }
    }
}
