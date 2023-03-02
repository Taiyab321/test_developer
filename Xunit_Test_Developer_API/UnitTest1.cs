using AutoMapper;
using Developer_Test_Api.Dto;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using Test_Developer_api.Controllers;
using Test_Developer_api.Mapper;
using Test_Developer_api.Services.Interfaces;

namespace Xunit_Test_Developer_API
{
    public class UnitTest1
    {
        private readonly Mock<IPostCodeService> postCodeService;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<PostCodeDetailController>> _logger;
        public UnitTest1()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new DevAutoMapper());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _logger = new  Mock<ILogger<PostCodeDetailController>>();
            postCodeService = new Mock<IPostCodeService>();
        }

        

        [Fact]
        public async void GetAutocompletePostcode_PostCode()
        {

            var dtoPostCode = GetAutocompletePostcodeDto();

            postCodeService.Setup(x =>  x.GetAutocompletePostcode("sl"))
                .Returns(dtoPostCode);

            var postCodeController = new PostCodeDetailController(_logger.Object, postCodeService.Object, _mapper);
            //act
            var result = await postCodeController.GetAutocompletePostcode("sl");
            //assert
            Assert.NotNull(result);
            Assert.Equal(result.Status.ToString(), dtoPostCode.Result.Status.ToString());
        }
        [Fact]
        public async void GetLookupPostcode_PostCode()
        {
            var testloolupPostCode = GetLookupPostcode();

            postCodeService.Setup(x => x.GetLookupPostcode("SL0 0AA"))
              .Returns(testloolupPostCode);

            var postCodeController = new PostCodeDetailController(_logger.Object, postCodeService.Object, _mapper);
            //act
            var result = await postCodeController.GetLookupPostcode("SL0 0AA");
            //assert
            Assert.NotNull(result);
            Assert.Equal(result.Status.ToString(), testloolupPostCode.Result.Status.ToString());
        }
        public async Task<AutocompletePostcodeDto> GetAutocompletePostcodeDto()
        {
            string[] str = {
                "SL0 0AA",
                "SL0 0AD",
                "SL0 0AE",
                "SL0 0AF",
                "SL0 0AG",
                "SL0 0AH",
                "SL0 0AJ",
                "SL0 0AL",
                "SL0 0AN",
                "SL0 0AP"
            };
            AutocompletePostcodeDto obj = new AutocompletePostcodeDto()
            {
                Status = 200,
                Result = str.ToList(),
            };
            return await Task.Run(() => obj);

        }
        public async Task<LookupPostcodeDto> GetLookupPostcode()
        {
            LookupPostcode lookup = new LookupPostcode()
            {
                Country = "England",
                Region = "South East",
                Admin_district = "Buckinghamshire",
                Parliamentary_constituency = "Beaconsfield",
                Latitude = 51.521717,
                Area = "South"
            };
            LookupPostcodeDto obj = new LookupPostcodeDto()
            {
                Status = 200,
                Result = lookup
            };
            return await Task.Run(() => obj);
        }
    }
}
       
    
