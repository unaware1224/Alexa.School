using System;
using System.Collections.Generic;
using System.Text;

namespace Alexa.School.Data.Listings
{
    internal class HenricoCountyProvider : ISchoolListingProvider
    {
        public string Name { get; } = "Henrico County Public Schools";
        public List<School> GetSchools()
        {
            throw new NotImplementedException();
        }
    }
}
