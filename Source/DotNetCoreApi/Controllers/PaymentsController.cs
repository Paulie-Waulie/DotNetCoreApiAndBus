namespace DotNetCoreApi.Controllers
{
    using System.Threading.Tasks;
    using global::Contracts.Rest;
    using Microsoft.AspNetCore.Mvc;
    using Service;

    [Route("api/[controller]")]
    public class PaymentsController : Controller
    {
        private readonly IPaymentService paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [HttpGet("{id}")]
        public Payment Get(string id)
        {
            return new Payment();
        }

        [HttpPut("{paymentReference}")]
        public async Task<CreatedAtActionResult> Put(string paymentReference, [FromBody]Payment value)
        {
            var redirect = await this.paymentService.RegisterAttempt(paymentReference, value);
            return this.CreatedAtAction("Get", new { id = paymentReference } ,redirect);
        }
    }
}
