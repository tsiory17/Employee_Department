using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidtermTsiory
{
    public partial class Form1 : Form
    {
        private bool OKToChange = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Dock = DockStyle.Fill;
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

            BusinessLayer.Departments.UpdateDepartments();
        }

        private void bindingSource2_CurrentChanged(object sender, EventArgs e)
        {
            BusinessLayer.Employees.UpdateEmployees();
        }

        private void departmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OKToChange)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource1.DataSource = Data.Departments.GetDepartments();
                bindingSource1.Sort = "DeptId";
                dataGridView1.DataSource = bindingSource1;

                dataGridView1.Columns["DeptId"].HeaderText = "Department ID";
                dataGridView1.Columns["Name"].DisplayIndex = 0;
               //dataGridView1.Columns["MrgId"].DisplayIndex = 1;

            }
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OKToChange)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource2.DataSource = Data.Employees.GetEmployees();
                bindingSource2.Sort = "EmpId";
                dataGridView1.DataSource = bindingSource2;

                dataGridView1.Columns["EmpId"].HeaderText = "Employee ID";
                dataGridView1.Columns["EmpId"].DisplayIndex = 0;
                dataGridView1.Columns["Name"].DisplayIndex = 1;
                dataGridView1.Columns["Age"].DisplayIndex = 2;
                dataGridView1.Columns["Salary"].DisplayIndex = 3;
                dataGridView1.Columns["DeptId"].DisplayIndex = 4;
            }
        }
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            MessageBox.Show("Impossible to insert / update / delete");
            e.Cancel = false;
            OKToChange = false;
        }
        private void menuStrip1_Click(object sender, EventArgs e)
        {

            OKToChange = true;

            BindingSource temp = (BindingSource)dataGridView1.DataSource;

            Validate();


            if (temp == bindingSource1)
            {
                if (BusinessLayer.Departments.UpdateDepartments() == -1)
                {
                    OKToChange = false;
                }
            }
            else if (temp == bindingSource2)
            {
                if (BusinessLayer.Employees.UpdateEmployees() == -1)
                {
                    OKToChange = false;
                }
            }

        }

        internal static void msgAge()
        {
            MessageBox.Show("Age should be between 18 and 120 years old ");
        }

        internal static void msgSalary()
        {
            MessageBox.Show("Salary is invalid,Must be higher than 15000$ ");
        }
    }
}
