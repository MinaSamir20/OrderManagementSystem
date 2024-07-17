using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Api.Base;
using OrderManagementSystem.Application.Features.Invoices.Models;

namespace OrderManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : AppControllerBase
    {
        [HttpGet, Route("{invoiceId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int invoiceId) => NewResult(await Mediator.Send(new GetInvoiceDetails(invoiceId)));

        [HttpGet, Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll() => NewResult(await Mediator.Send(new GetInvoiceList()));
    }
}
