using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Api.Base;
using OrderManagementSystem.Application.Features.Orders.Models;
using OrderManagementSystem.Application.Features.Products.Models;

namespace OrderManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : AppControllerBase
    {
        [HttpPost, Route("")]
        public async Task<IActionResult> Create(CreateOrder command) => NewResult(await Mediator.Send(command));

        [HttpPut, Route("{orderId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateOrder command) => NewResult(await Mediator.Send(command));

        [HttpGet, Route("{orderId}")]
        public async Task<IActionResult> Get(int orderId) => NewResult(await Mediator.Send(new GetProductDetails(orderId)));

        [HttpGet, Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll() => NewResult(await Mediator.Send(new GetOrderList()));

    }
}
