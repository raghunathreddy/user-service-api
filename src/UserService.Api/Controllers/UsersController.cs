using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Service.DtoModels;
using Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserProfileService _userService;

        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public UsersController(IUserProfileService userService)
        {
            _userService = userService;
            //_secretKey = configuration["JwtSettings:SecretKey"];
            //_issuer = configuration["JwtSettings:Issuer"];
            //_audience = configuration["JwtSettings:Audience"];
        }
        // GET: api/<UsersController>
        [HttpGet]
        public List<DtoUserprofile> GetAllusers()
        {
            return _userService.GetAllUsers();
            //return new string[] { "value1", "value2" };
        }
        [HttpGet("{id}")]
        public DtoUserprofile GetUser(int id)
        {
            return _userService.GetUsers(id);
            //return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpPost]
        [AllowAnonymous]
        public IActionResult UserLogin(string emailid,string pwd)
        {
            DtoJwtToken accessToken = _userService.GetAllUser(emailid, pwd);
            return StatusCode(StatusCodes.Status200OK, accessToken);
            //return _userService.GetAllUser(emailid, pwd);
        }

        // POST api/<UsersController>
        [HttpPost]
        public void RegisterUser([FromBody] DtoUserprofile userdetails)
        {
            _userService.AddUserdetails(userdetails);
        }

        [HttpPut("{id}")]
        public void ResetPassword(int id,string password)
        {
            var userdto = new DtoUserprofile() {user_id=id, pwd= password };
            _userService.Updatepwd(userdto);
        }


    }
}
