using Microsoft.AspNetCore.Mvc;
using RelationShip.Dto;
using RelationShip.Interfaces;

namespace RelationShip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthReposirory _authReposirory;

        public AuthController(IAuthReposirory authReposirory)
        {
            _authReposirory = authReposirory;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(AuthDto valueAuth)
        {
            var token = _authReposirory.Login(valueAuth);
            return Ok(token);
        }

        [HttpPost]
        public IActionResult Register(AuthDto valueAuth)
        {
            var isSuccess = _authReposirory.Register(valueAuth);
            if(isSuccess)
            {
                return Ok("Register Success");
            }
            return BadRequest("Register Fail");
        }

        [HttpGet]
        [Route("decodeByCookie")]
        public IActionResult Decode()
        {
            var code = _authReposirory.DecodeJwt();
            if(code != null)
            {
                return Ok(code);    
            }
            return BadRequest("Get Jwt Fail");
        }

        [HttpGet]
        [Route("decodeBySession")]
        public IActionResult DecodeBySession()
        {
            // Lấy giá trị của cookie session
            var sessionValue = HttpContext.Session.GetString("jwtSession");
          
            return Ok(sessionValue);
        }

        [HttpGet]
        [Route("refreshToken")]
        public IActionResult GetNewAccsessToken()
        {
            var token = _authReposirory.RefreshAccessToken();
            return Ok(token);
        }

        [HttpPost]
        [Route("GenerateOTP")]
        public IActionResult GenerateOTP(OtpDto requestOtp)
        {
             _authReposirory.GenerateOTP(requestOtp);
            return Ok();
        }

        [HttpPost]
        [Route("CheckOtp")]
        public IActionResult CheckOtp(OtpDto requestOtp)
        {
            bool checkOtpIsSuccess = _authReposirory.CheckOtp(requestOtp);
            if (checkOtpIsSuccess)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout()
        {
            var logoutIsSuccess = _authReposirory.Logout();
            if(logoutIsSuccess)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
