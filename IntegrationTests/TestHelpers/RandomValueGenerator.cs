using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTests.TestHelpers
{
    public class RandomValueGenerator
    {
        public string String() => Guid.NewGuid().ToString("N");
        public string String(int length) => String().Substring(0, length);
    }
}
