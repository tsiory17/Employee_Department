using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing.Printing;
using System.Data.SqlClient;

namespace Data
{
    internal class Connect
    {
        private static String departmentsEmployeesConnectionsString = GetConnectString();
        internal static String ConnectionString { get => departmentsEmployeesConnectionsString; }

        private static String GetConnectString()
        {
            SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder();
            cs.DataSource = "(local)";
            cs.InitialCatalog = "departmentsEmployees";
            cs.UserID = "sa";
            cs.Password = "sysadm";
            return cs.ConnectionString;
        }
    }
    internal class DataTables
    {
        private static SqlDataAdapter adapterDepartments = InitAdapterDepartments();
        private static SqlDataAdapter adapterEmployees = InitAdapterEmployees();
        private static DataSet ds = InitDataSet();
        private static SqlDataAdapter InitAdapterDepartments()
        {
            SqlDataAdapter d = new SqlDataAdapter(
                "SELECT * FROM Departments ORDER BY DeptId ",
                Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(d);
            d.UpdateCommand = builder.GetUpdateCommand();

            return d;
        }
        private static SqlDataAdapter InitAdapterEmployees()
        {
            SqlDataAdapter e = new SqlDataAdapter(
                "SELECT * FROM Employees ORDER BY EmpId ",
                Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(e);

            builder.ConflictOption = ConflictOption.OverwriteChanges;

            e.UpdateCommand = builder.GetUpdateCommand();

            return e;
        }
        private static DataSet InitDataSet()
        {
            DataSet ds = new DataSet();
            loadDepartments(ds);
            loadEmployees(ds);
            return ds;
        }
        private static void loadDepartments(DataSet ds)
        {

            adapterDepartments.MissingSchemaAction = MissingSchemaAction.AddWithKey;


            adapterDepartments.Fill(ds, "Departments");

            //ForeignKeyConstraint myFK = new ForeignKeyConstraint("MyFK",
            //new DataColumn[] { ds.Tables["Employees"].Columns["MgrId"] },
            // new DataColumn[] { ds.Tables["Departments"].Columns["MgrId"] });

            //myFK.DeleteRule = Rule.None;
            //myFK.UpdateRule = Rule.Cascade;
            //ds.Tables["Employees"].Constraints.Add(myFK);

        }
        private static void loadEmployees(DataSet ds)
        {

            adapterEmployees.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            //be careful here
            adapterEmployees.Fill(ds, "Employees");



            ForeignKeyConstraint myFK = new ForeignKeyConstraint("MyFK",
                new DataColumn[]{
                    ds.Tables["Departments"].Columns["DeptId"]
                },
                new DataColumn[] {
                    ds.Tables["Employees"].Columns["DeptId"]
                }
            );
            myFK.DeleteRule = Rule.None;
            myFK.UpdateRule = Rule.Cascade;
            ds.Tables["Employees"].Constraints.Add(myFK);

           //  =========================================================================
            ForeignKeyConstraint myFK1 = new ForeignKeyConstraint("MyFK1",
                new DataColumn[]{
                    ds.Tables["Employees"].Columns["EmpId"]
                },
                new DataColumn[] {
                    ds.Tables["Departments"].Columns["MgrId"]
                }
            );
            myFK.DeleteRule = Rule.None;
            myFK.UpdateRule = Rule.Cascade;
            ds.Tables["Departments"].Constraints.Add(myFK1);
        }

        internal static SqlDataAdapter getAdapterDepartments()
        {
            return adapterDepartments;
        }

        internal static SqlDataAdapter getAdapterEmployees()
        {
            return adapterEmployees;
        }

        internal static DataSet getDataSet()
        {
            return ds;
        }
    }


    internal class Departments
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterDepartments();
        private static DataSet ds = DataTables.getDataSet();


        internal static DataTable GetDepartments()
        {
            return ds.Tables["Departments"];
        }

        internal static int UpdateDepartments()
        {
            if (!ds.Tables["Departments"].HasErrors)
            {
                return adapter.Update(ds.Tables["Departments"]);
            }
            else
            {
                return -1;
            }
        }
    }

    internal class Employees
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterEmployees();
        private static DataSet ds = DataTables.getDataSet();

        internal static DataTable GetEmployees()
        {
            return ds.Tables["Employees"];
        }

        internal static int UpdateEmployees()
        {
            if (!ds.Tables["Employees"].HasErrors)
            {
                return adapter.Update(ds.Tables["Employees"]);
            }
            else
            {
                return -1;
            }
        }

    }
}

