using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Api.Base;
using OrderManagementSystem.Application.Features.Customers.Models;

namespace OrderManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : AppControllerBase
    {
        [HttpPost, Route("")]
        public async Task<IActionResult> Create(CreateCustomer command) => NewResult(await Mediator.Send(command));

        [HttpGet, Route("{customerId}/orders")]
        public async Task<IActionResult> Get(int customerId) => NewResult(await Mediator.Send(new GetOrdersListWithCustomerId(customerId)));
    }
}
