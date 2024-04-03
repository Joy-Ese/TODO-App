using Microsoft.Extensions.Logging;
using TODO.Services.Interfaces;

namespace TODO.Services.Services
{
    public class AuthServices : IAuth
    {
        private readonly ILogger<AuthServices> _logger;

        public AuthServices(ILogger<AuthServices> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "Nlog injected into AuthServices");
        }

        public string AccountNumberGenerator()
        {
            var accountNumber = DateTime.Now.ToString("HHffMMffdd");
            return accountNumber;
        }

    }
}
