using CleanArchitecture.Application;
using CleanArchitecture.Function;
using CleanArchitecture.Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace CleanArchitecture.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;
            builder.Services.AddInfrastructure(configuration);
            builder.Services.AddApplication();
        }
    }
}