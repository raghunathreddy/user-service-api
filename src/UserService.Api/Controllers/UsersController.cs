using Microsoft.AspNetCore.Mvc;
using Service.DtoModels;
using Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserProfileService _userService;
        public UsersController(IUserProfileService userService)
        {
            _userService = userService;
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
        public DtoUserprofile ValidateUser(string emailid,string pwd)
        {
            return _userService.GetAllUser(emailid, pwd);
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
