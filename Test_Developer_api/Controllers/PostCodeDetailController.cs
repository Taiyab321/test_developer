using AutoMapper;
using Developer_Test_Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Test_Developer_api.Services.Interfaces;

namespace Test_Developer_api.Controllers
{
    [Route("api/[controller]")]
    public class PostCodeDetailController : Controller
    {
        private readonly ILogger<PostCodeDetailController> _logger;
        private readonly IPostCodeService _postCodeService;
        private readonly IMapper _mapper;
        public PostCodeDetailController(ILogger<PostCodeDetailController> logger,
           IPostCodeService postCodeService, IMapper mapper)
        {
            _logger = logger;
            this._postCodeService = postCodeService;
            this._mapper = mapper;
        }
        [HttpGet("{postCode}")]
        public async Task<ReturnObjectDto> GetAutocompletePostcode(string postCode)
        {
            var result = await this._postCodeService.GetAutocompletePostcode(postCode);

            if (result.Status == 200)
                return this._mapper.Map<ReturnObjectDto>(result);
            else
                return new ReturnObjectDto() { Status = result.Status, Result = "failed to fetch data" };

        }
        [HttpGet("LookupPostcode/{postCode}")]
        public async Task<ReturnObjectDto> GetLookupPostcode(string postCode)
        {
            var result = await this._postCodeService.GetLookupPostcode(postCode);

            
            if (result.Status == 200)
                return this._mapper.Map<ReturnObjectDto>(result);
            else
                return new ReturnObjectDto() { Status = result.Status, Result = "failed to fetch data" };
        }
    }
}
