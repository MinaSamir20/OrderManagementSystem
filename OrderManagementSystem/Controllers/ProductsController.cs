using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Api.Base;
using OrderManagementSystem.Application.Features.Products.Models;

namespace OrderManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : AppControllerBase
    {
        [HttpPost, Route("")]
        public async Task<IActionResult> Create(CreateProduct command) => NewResult(await Mediator.Send(command));

        [HttpPut, Route("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateProduct command) => NewResult(await Mediator.Send(command));

        [HttpGet, Route("")]
        public async Task<IActionResult> GetAll() => NewResult(await Mediator.Send(new GetProductList()));

        [HttpGet, Route("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int productId) => NewResult(await Mediator.Send(new GetProductDetails(productId)));

    }
}
