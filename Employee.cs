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
        public int Id { get; set; }
        public string name { get; set; }
        public string address { get; set; }

        public int joiningYear { get; set; }

        public List<Employee> list = new List<Employee>();

        string[] nameArr = { "Edgar Barber", "Marquis Reilly", "Isabell Frank", "Issac Hammond", "Aydan Mcclain", "Daphne Burke", "Mia Duarte", "Phoebe Mcclain", "Dayana Massey", "Maxwell Zhang", "Keith Pineda", "Dustin Camacho", "Alyvia Lam", "Justice Mata", "Alissa Bautista", "Laura Wade", "Howard Flynn", "Marques Wilcox", "Taryn Wang", "Caden Potts" };
        string[] roleArr = { "IT Coordinator", "Technical writer", "Data Analyst", "Web Developer", "Systems Analyst", "IT Analyst", "SQA Engineer", "System Administrator", "Business intelligence analyst", "Software Architect", ".NET Developer", "UX Designer", "Product Manager", "Database Administrator", "Data Scientist", "Computer Programmer", "Application analyst", "Web Developer", "Java Developer", "Application developer" };
        string[] addressArr = { "753, Schinner Lake", "15504, O'Kon Plaza", "10281, Moore Island", "58552, Fermin Harbors", "786, Schmitt Crescent", "4028, Swaniawski Pass", "12616, Allison Inlet", "19745, Emily Street", "5733, Winifred Motorway", "678, McClure Course" };

        public List<Employee> EmployeeList = new List<Employee>();

        public Employee()
        {

        }
        public Employee(int id, string employeeName, string employeeAddress, int year)
        {
            Id = id;
            name = employeeName;
            address = employeeAddress;
            joiningYear = year;
        }

        // ****************************************USED IT TO POPULATE DATABASE****************************************************//

        //public void EmployeeGenerator(string[] nameArr, string[] roleArr, string[] addressArr)
        //{
        //    Random rnd = new Random();
        //    using (SqlConnection sqlCon = new SqlConnection(@"Data Source=CMDLHRDB01;Initial Catalog=EmployeeTbl;User ID=sa;Password=CureMD2022"))
        //    {
        //        for (int i = 0; i < 10; i++)
        //        {
        //            string query = "INSERT INTO dbo.Employees(Name, Role, Salary, Address) VALUES(@Name, @Role, @Salary, @Address)";
        //            using (SqlCommand command = new SqlCommand(query, sqlCon))
        //            {
        //                command.Parameters.AddWithValue("@Name", nameArr[i % nameArr.Length]);
        //                command.Parameters.AddWithValue("@Role", roleArr[i % roleArr.Length]);
        //                command.Parameters.AddWithValue("@Salary", rnd.Next(50000, 100000));
        //                command.Parameters.AddWithValue("@Address", addressArr[i % addressArr.Length]);

        //                sqlCon.Open();
        //                //int result = command.ExecuteNonQuery();
        //                sqlCon.Close();

        //            }

        //        }
        //    }

        //}



        public List<Employee> GetEmployees()
        {
            //Making connection & creating list of Employee objects from the read data
            using (SqlConnection con = new SqlConnection(@"Data Source=CMDLHRDB01;Initial Catalog=StudentTbl;User ID=sa;Password=CureMD2022"))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT TOP 10 * FROM Students;", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee emp = new Employee(Convert.ToInt32(reader.GetValue(0)), reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), Convert.ToInt32(reader.GetValue(3)));
                            list.Add(emp);
                        }
                    }

                }
                con.Close();
                return list;

            }

        }

        public Employee GetEmployee(int id)
        {
            //Making connection & returning Employee object from the read data

            using (SqlConnection con = new SqlConnection(@"Data Source=CMDLHRDB01;Initial Catalog=StudentTbl;User ID=sa;Password=CureMD2022"))
            {
                Employee emp = new Employee();
                using (SqlCommand command = new SqlCommand("SELECT * FROM students WHERE ID='" + id + "'", con))
                {
                    con.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            emp = new Employee(Convert.ToInt32(reader.GetValue(0)), reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), Convert.ToInt32(reader.GetValue(3)));
                        }

                    }


                }
                con.Close();
                return emp;



            }


        }

        public void UpdateEmployee(int Id, string StudentName, string Address, int AdmissionYear)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=CMDLHRDB01;Initial Catalog=StudentTbl;User ID=sa;Password=CureMD2022"))
            {
                
                string updateQuery = " UPDATE Students SET StudentName = @StudentName , Address = @Address, AdmissionYear=@AdmissionYear WHERE Id = @Id; ";
                using (SqlCommand command = new SqlCommand(updateQuery, con))
                {
                    con.Open();

                    command.Parameters.AddWithValue("@StudentName", StudentName);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@AdmissionYear",AdmissionYear);
                    command.Parameters.AddWithValue("@Id", Id);

                    int result = command.ExecuteNonQuery();

                    con.Close();

                }
            }
        }

        public void AddEmployee(string StudentName, string Address, int AdmissionYear)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=CMDLHRDB01;Initial Catalog=StudentTbl;User ID=sa;Password=CureMD2022"))
            {

                string updateQuery = " INSERT INTO Students(StudentName, Address, AdmissionYear ) VALUES(@StudentName , @Address, @AdmissionYear); ";
                using (SqlCommand command = new SqlCommand(updateQuery, con))
                {
                    con.Open();

                    command.Parameters.AddWithValue("@StudentName", StudentName);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@AdmissionYear", AdmissionYear);
                    //command.Parameters.AddWithValue("@Id", Id);

                    int result = command.ExecuteNonQuery();

                    con.Close();

                }
            }
        }

        public void DeleteEmployee(int Id)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=CMDLHRDB01;Initial Catalog=StudentTbl;User ID=sa;Password=CureMD2022"))
            {

                string deleteQuery = " DELETE FROM Students WHERE Id =@id; ";
                string reseedCmd = "DBCC CHECKIDENT ('Students', RESEED, 1);";

                using (SqlCommand command = new SqlCommand(deleteQuery, con))
                {
                    con.Open();
                    command.Parameters.AddWithValue("@Id", Id);

                    int result = command.ExecuteNonQuery();

                    con.Close();

                }

                using (SqlCommand command = new SqlCommand(reseedCmd, con))
                {
                    con.Open();
                    int result = command.ExecuteNonQuery();
                    con.Close();

                }
            }
        }
    }
}