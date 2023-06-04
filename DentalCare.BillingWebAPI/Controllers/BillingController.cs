using Entities = DentalCare.Entities;
using DentalCare.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using DentalCare.BLL.Interface;
using DentalCare.DAL.Interface;
using DentalCare.Entities;
using DentalCare.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace DentalCare.BillingWebAPI.Controllers
{
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly ILogger<BillingController> _logger;
        private readonly IBillingBLL _billingBLL;

        public BillingController(ILogger<BillingController> logger, IBillingBLL billingBLL)
        {
            _logger = logger;
            _billingBLL = billingBLL;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [Route("/Bills/{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Request BillId: { id }");
            var bill =  _billingBLL.GetBill(id);
            _logger.LogInformation($"Response Bill: { bill.SerializeToJson() }");

            return Ok(bill);
        }

    }
}