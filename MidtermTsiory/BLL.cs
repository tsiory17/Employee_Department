using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BusinessLayer
{
    class Departments
    {
        internal static int UpdateDepartments()
        {


            return Data.Departments.UpdateDepartments();
        }
    }

    class Employees
    {
        internal static int UpdateEmployees()
        {

            DataTable dt2 = Data.Employees.GetEmployees().GetChanges(DataRowState.Added | DataRowState.Modified);
            if ((dt2 != null) && (dt2.Select(" Salary <= 15000 ").Length > 0))
            {
                MidtermTsiory.Form1.msgSalary();
                Data.Employees.GetEmployees().RejectChanges();
                return -1;
            }

            if ((dt2 != null) && (dt2.Select(" Age < 18 or Age >120 ").Length > 0))
            {
                MidtermTsiory.Form1.msgAge();
                Data.Employees.GetEmployees().RejectChanges();
                return -1;
            }

            else
            {
                return Data.Employees.UpdateEmployees();

            }





        }

     }
 }

