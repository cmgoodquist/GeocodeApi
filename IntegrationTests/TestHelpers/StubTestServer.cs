using GeocodeApi.AppStart;
using GeocodeApi.Geocode;
using GeocodeApi.Geocode.DrivenDependencies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Net.Http;

namespace IntegrationTests.TestHelpers
{
    public class StubTestServer : TestServer
    {
        public StubTestServer(HttpResponseMessage expectedResponse) : base(new WebHostBuilder()
            .UseConfiguration(
                new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
            )
            .UseStartup<Startup>()
            .ConfigureServices((service) => service
                .AddHttpClient<IGeocodeService, GeocodeService>()
                .ConfigurePrimaryHttpMessageHandler(() => new StubHttpMessageHandler(expectedResponse))
            )
        )
        { }
    }
}
