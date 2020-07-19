using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Domain.Books;
using BookShop.Domain.Books.Repositories;
using BookShop.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookShop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookReadRepository _bookReadRepository;
        private readonly IMediator _mediator;

        public BooksController(
            ILogger<BooksController> logger,
            IBookReadRepository bookReadRepository,
            IMediator mediator)
        {
            _logger = logger;
            _bookReadRepository = bookReadRepository;
            _mediator = mediator;
        }

        [HttpGet("ListAll")]
        public async Task<ActionResult<Result<IEnumerable<Book>>>> ListAll()
        {
            try
            {
                var list = await _bookReadRepository.ListAll();
                return Ok(new Result<IEnumerable<Book>>(list));
            }
            catch (Exception ex)
            {
                return BadRequest(ResultFactory.Error(ex.Message));
            }
        }

        [HttpPost("AvaliableStatus")]
        public async Task<ActionResult<Result<bool>>> AvaliableStatus(Domain.Books.Queries.AvaliableStatus.Request request)
        {
            try
            {
                var result = await _mediator.Send(request, CancellationToken.None);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ResultFactory.Error(ex.Message));
            }
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<Result<bool>>> Insert(Domain.Books.Commands.Insert.Request request)
        {
            try
            {
                var result = await _mediator.Send(request, CancellationToken.None);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ResultFactory.Error(ex.Message));
            }
        }
    }
}
