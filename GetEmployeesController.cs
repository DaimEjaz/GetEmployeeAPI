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
        [Route("GetEmployees")]
        [HttpGet]
        public string GetEmployees()
        {
            var data = new Employee().GetEmployees();
            return data;
        }

        [Route("GetEmployee/{id}")]
        [HttpGet]

        public string GetEmployee(int id)
        {
            var data = new Employee().GetEmployee(id);
            return data;
        }
    }
}
