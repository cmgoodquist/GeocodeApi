using GeocodeApi.AppStart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Tests
{
    public class DependencyInjectionTests
    {
        public static IEnumerable<object[]> ControllerTypes()
        {
            var assemblyTypes = Assembly.GetAssembly(typeof(Program)).GetTypes();
            return assemblyTypes
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
                .Select(type => new[] { type });
        }

        [Theory]
        [MemberData(nameof(ControllerTypes))]
        public void ControllerDependencies_ResolveCorrectly_WhenControllerInstantiated(Type controllerType)
        {
            //Arrange
            var host = Program.CreateHostBuilder(new string[] { }).Build();
            var serviceProvider = host.Services;
            var controllerActivator = serviceProvider.GetRequiredService<IControllerActivator>();

            //Act
            //Will throw InvalidOperationException if dependency can't be resolved.
            controllerActivator.Create(
                new ControllerContext(
                    new ActionContext(
                        new DefaultHttpContext()
                        {
                            RequestServices = serviceProvider
                        },
                        new RouteData(),
                        new ControllerActionDescriptor()
                        {
                            ControllerTypeInfo = controllerType.GetTypeInfo()
                        }
                    )
                )
            );

            //Assert
            Assert.True(true);
        }

    }
}
