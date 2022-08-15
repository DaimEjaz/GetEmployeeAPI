using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GetEmployeesAPI
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string role { get; set; }

        public double salary { get; set; }

        public string address { get; set; }

        string[] nameArr = { "Edgar Barber", "Marquis Reilly", "Isabell Frank", "Issac Hammond", "Aydan Mcclain", "Daphne Burke", "Mia Duarte", "Phoebe Mcclain", "Dayana Massey", "Maxwell Zhang", "Keith Pineda", "Dustin Camacho", "Alyvia Lam", "Justice Mata", "Alissa Bautista", "Laura Wade", "Howard Flynn", "Marques Wilcox", "Taryn Wang", "Caden Potts" };
        string[] roleArr = { "IT Coordinator", "Technical writer", "Data Analyst", "Web Developer", "Systems Analyst", "IT Analyst", "SQA Engineer", "System Administrator", "Business intelligence analyst", "Software Architect", ".NET Developer", "UX Designer", "Product Manager", "Database Administrator", "Data Scientist", "Computer Programmer", "Application analyst", "Web Developer", "Java Developer", "Application developer" };
        string[] addressArr = { "753, Schinner Lake", "15504, O'Kon Plaza", "10281, Moore Island", "58552, Fermin Harbors", "786, Schmitt Crescent", "4028, Swaniawski Pass", "12616, Allison Inlet", "19745, Emily Street", "5733, Winifred Motorway", "678, McClure Course" };

        public List<Employee> EmployeeList = new List<Employee>();
        //public List<string[]>

        public Employee()
        {

        }
        public Employee(string employeeName, string employeeRole, double empSalary, string empAddress)
        {
            name = employeeName;
            role = employeeRole;
            salary = empSalary;
            address = empAddress;
        }
        public void EmployeeGenerator(string[] nameArr, string[] roleArr, string[] addressArr)
        {
            Random rnd = new Random();
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=CMDLHRDB01;Initial Catalog=EmployeeTbl;User ID=sa;Password=CureMD2022"))
            {
                for (int i = 0; i < 10; i++)
                {
                    string query = "INSERT INTO dbo.Employees(Name, Role, Salary, Address) VALUES(@Name, @Role, @Salary, @Address)";
                    using (SqlCommand command = new SqlCommand(query, sqlCon))
                    {
                        command.Parameters.AddWithValue("@Name", nameArr[i % nameArr.Length]);
                        command.Parameters.AddWithValue("@Role", roleArr[i % roleArr.Length]);
                        command.Parameters.AddWithValue("@Salary", rnd.Next(50000, 100000));
                        command.Parameters.AddWithValue("@Address", addressArr[i % addressArr.Length]);

                        sqlCon.Open();
                        //int result = command.ExecuteNonQuery();
                        sqlCon.Close();

                    }

                }
            }

        }
            


        public string GetEmployees()
        {

            SqlConnection con = new SqlConnection(@"Data Source=CMDLHRDB01;Initial Catalog=StudentTbl;User ID=sa;Password=CureMD2022");
            con.Open();
            string query = "SELECT TOP 10 * FROM Students;";
            SqlDataAdapter SDA = new SqlDataAdapter(query, con);
            DataTable table = new DataTable();
            SDA.Fill(table);
            if (table.Rows.Count > 0)
            {
                con.Close();
                return JsonConvert.SerializeObject(table);
            }

            //Error handling
            else
            {
                con.Close();
                return "No employees records found in the database";
            }


        }

        public string GetEmployee(int id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=CMDLHRDB01;Initial Catalog=StudentTbl;Persist Security Info=True;User ID=sa;Password=CureMD2022");
            con.Open();
            SqlDataAdapter SDA = new SqlDataAdapter("SELECT * FROM students WHERE ID='" + id + "'", con);
            DataTable table = new DataTable();
            SDA.Fill(table);
            if (table.Rows.Count > 0)
            {
                con.Close();
                return JsonConvert.SerializeObject(table);
            }

            //Error handling
            else
            {
                con.Close();
                return "No employees records found in the database";
            }
        }


    }
}