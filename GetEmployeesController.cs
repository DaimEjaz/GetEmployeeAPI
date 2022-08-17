using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GetEmployeesAPI.Controllers
{
    public class GetEmployeesController : ApiController
    {
        //Defining route & method for both API's

        [Route("GetEmployees")]
        [HttpGet]
        public List<Employee> GetEmployees()
        {
            //Returning fetched results back to client
            var data = new Employee().GetEmployees();
            return data;
        }

        [Route("GetEmployee/{id}")]
        [HttpGet]

        public Employee GetEmployee(int id)
        {
            var data = new Employee().GetEmployee(id);
            return data;
        }

        [Route("UpdateEmployee/{id}")]
        [HttpPut]

        public void UpdateEmployee(int id, [FromBody] Employee employee)
        {
            int Id = id;
            string StudentName = employee.name;
            string Address = employee.address;
            int AdmissionYear = employee.joiningYear;
            new Employee().UpdateEmployee( Id,  StudentName,  Address, AdmissionYear);
        }


        [Route("AddEmployee")]
        [HttpPost]

        public void AddEmployee([FromBody] Employee employee)
        {
            string StudentName = employee.name;
            string Address = employee.address;
            int AdmissionYear = employee.joiningYear;
            new Employee().AddEmployee(StudentName, Address, AdmissionYear);
        }


        [Route("DeleteEmployee/{id}")]
        [HttpDelete]

        public void DeleteEmployee(int id)
        {
            new Employee().DeleteEmployee(id);
        }


    }
}
