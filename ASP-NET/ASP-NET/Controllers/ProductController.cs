using ASP_NET.Dto;
using ASP_NET.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductService _service;

        public ProductController(IWebHostEnvironment webHostEnvironment, IProductService service)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._service = service;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ResultResponseDto>> CreateProduct([FromForm] CreateProductDto body)
        {

            ResultResponseDto result = await this._service.CreateProductAsync(body);

            return Ok(result);
        }
    }
}
