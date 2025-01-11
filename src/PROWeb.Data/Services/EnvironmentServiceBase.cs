using Microsoft.AspNetCore.Hosting;

namespace PROWeb.Data.Services
{
    public class EnvironmentServiceBase
    {
        protected IWebHostEnvironment Environment { get; }

        protected EnvironmentServiceBase(IWebHostEnvironment environment)
        {
            Environment = environment;
        }
    }
}
