namespace Developer_Test_Api.Dto
{
    public class LookupPostcodeDto
    {
        public int Status { get; set; }
        public LookupPostcode Result { get; set; }
    }
    public class LookupPostcode
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string Admin_district { get; set; }
        public string Parliamentary_constituency { get; set; }
        public double Latitude { get; set; }
        public string Area { get; set; }
    }
}
