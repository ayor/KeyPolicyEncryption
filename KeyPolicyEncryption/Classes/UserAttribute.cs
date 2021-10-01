using KeyPolicyEncryption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyPolicyEncryption.Classes
{
    public static class UserAttribute
    {
      public static string GetEmployeeType(EmployeeType employeeType)
        {

            foreach (var item in typeof(EmployeeType).GetProperties())
            {
                var property = item.Name.Split("Is");
                Console.WriteLine(item);
            }

            if (employeeType.IsContractor)
            {
                return "Contractor";
            }
            if (employeeType.IsEmployee)
            {
                return "Employee";
            }
            return "Labor";
        }

       public static string GetDepartment(Department department)
        {
            if (department.IsHR)
            {
                return "HR";
            }
            if (department.IsIT)
            {
                return "IT";
            }
            return "SCM";
        }

      public  static string GetUserRole(UserRole roles)
        {
            if (roles.IsAdmin)
            {
                return "Admin";
            }
            if (roles.IsDataOwner)
            {
                return "Dataowner";
            }
            return "DataUser";
        }
    }
}
