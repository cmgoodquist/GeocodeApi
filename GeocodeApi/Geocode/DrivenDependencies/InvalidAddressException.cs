using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeocodeApi.Geocode.DrivenDependencies
{
    public class InvalidAddressException: Exception
    {
        public InvalidAddressException()
            : base("Address must include street, city, and two-character state code.")
        { }
    }
}
