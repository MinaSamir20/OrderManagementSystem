﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Base;
using System.Net;

namespace OrderManagementSystem.Api.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppControllerBase : ControllerBase
    {
        private IMediator? _mediatorInst;
        protected IMediator Mediator => _mediatorInst ??= HttpContext.RequestServices.GetService<IMediator>()!;

        public ObjectResult NewResult<T>(Response<T> response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => new OkObjectResult(response),
                HttpStatusCode.Created => new CreatedResult(string.Empty, response),
                HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(response),
                HttpStatusCode.BadRequest => new BadRequestObjectResult(response),
                HttpStatusCode.NotFound => new NotFoundObjectResult(response),
                HttpStatusCode.Accepted => new AcceptedResult(string.Empty, response),
                HttpStatusCode.UnprocessableEntity => new UnprocessableEntityObjectResult(response),
                _ => new BadRequestObjectResult(response),
            };
        }
    }
}
