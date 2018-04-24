namespace DotNetCoreApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using DotNetCore.Contracts.Rest;
    using Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Service;

    [Route("api/[controller]")]
    public class PaymentsController : Controller
    {
        private readonly IPaymentService paymentService;
        private readonly IGetPaymentQuery getPaymentQuery;

        public PaymentsController(IPaymentService paymentService, IGetPaymentQuery getPaymentQuery)
        {
            this.paymentService = paymentService;
            this.getPaymentQuery = getPaymentQuery;
        }

        [HttpGet("{id}")]
        public async Task<Payment> Get(string id)
        {
            var model = await this.getPaymentQuery.Get(id);
            return model.ToContract();
        }

        [HttpPut("{paymentReference}")]
        public async Task<ActionResult> Put(string paymentReference, [FromBody]Payment value)
        {
            var redirect = await this.paymentService.RegisterPayment(value.ToModel(paymentReference));
            return this.CreatedAtAction("Get", new { id = paymentReference }, redirect.ToContract());
        }
    }
}
