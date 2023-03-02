using System;
using System.Collections.Generic;

namespace Developer_Test_Api.Dto
{
    public class AutocompletePostcodeDto
    {
        public int Status { get; set; }
        public List<string> Result { get; set; }
    }
}
