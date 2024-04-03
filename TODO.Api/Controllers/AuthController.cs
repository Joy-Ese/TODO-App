using Microsoft.AspNetCore.Mvc;
using TODO.Services.Interfaces;

namespace TODO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuth _authService;

        public AuthController(IAuth authService)
        {
            _authService = authService;
        }

    }
}
