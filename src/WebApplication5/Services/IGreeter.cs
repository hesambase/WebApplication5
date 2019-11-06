using Microsoft.Extensions.Configuration;
namespace WebApplication2.Services
{
    public interface IGreeter
    {
        string getTimeofday();

    }
    public class Greeter:IGreeter
    {
        private IConfiguration _configuration;
        public Greeter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string getTimeofday()
        {
            return _configuration["Greeting"];
        }
    }
}