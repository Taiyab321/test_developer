using Developer_Test_Api.Dto;

namespace Test_Developer_api.Services.Interfaces
{
    public interface IPostCodeService
    {
        Task<AutocompletePostcodeDto> GetAutocompletePostcode(string postCode);
        Task<LookupPostcodeDto> GetLookupPostcode(string postCode);
    }
}
