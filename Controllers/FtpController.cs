using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Fit_Pro_Tracker_Recruiting_exercise.Model;
using Fit_Pro_Tracker_Recruiting_exercise.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data.Common;
using System.Data.SqlClient;

namespace Fit_Pro_Tracker_Recruiting_exercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FtpController : ControllerBase
    {
        private readonly ICustomer _customer;
       

        public FtpController(ICustomer customer)
        {
            _customer = customer;
        }


        [HttpPost(nameof(AddCustomer))]

        public async Task<int> AddCustomer(Model.Customer data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Id",
                         data.Id,
                         DbType.Int32);
            var result = await Task.FromResult(_customer.Insert<int>("[dbo].[SP_Add_Customer]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }

        [HttpGet(nameof(GetById))]
        public async Task<Parameters> GetById(int Id)
        {
            var result = await Task.FromResult(_customer.Get<Parameters>($"Select * from [Customer] where Id = {Id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpDelete(nameof(DeleteCustomer))]
        public async Task<int> DeleteCustomer(int Id)
        {
            var result = await Task.FromResult(_customer.Execute($"Delete [Customer] Where Id = {Id}", null, commandType: CommandType.Text));
            return result;
        }




    }
}
