using AutoMapper;
using Service.DtoModels;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserService.Model;
using UserService.Repository.Implementation;
using UserService.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Service.Implementation
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserRepository _userReposiroty;
        private readonly IMapper _mapper;
        public UserProfileService(IUserRepository userReposiroty, IMapper mapper)
        {
            _userReposiroty = userReposiroty;
            _mapper = mapper;
            // _emailHelper = emailHelper;
        }

        public void AddUserdetails(DtoUserprofile userdetails)
        {
            var users = _mapper.Map<User>(userdetails);
            var userdata = new User()
            {
                 
                  email= users.email,
                  pwd = Encodepwd(users.pwd),
                  username= users.user_address,
                  user_address= users.user_address,
                  favorite_genres= users.favorite_genres,
                  reading_preferences= users.reading_preferences
            };
            _userReposiroty.AddUserdetails(userdata);
        }

        public DtoJwtToken GetAllUser(string emailid, string pwd)
        {
            var result = _userReposiroty.GetAllUser(emailid, Encodepwd(pwd));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes("4F79A9D8B8A7AABDECBF4D0EFAFD6D3F2C1A0A9C8987859499BAC7B6F2D9E8F7");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user_id", result.user_id.ToString()),
                    new Claim("username", result.username),
                    new Claim("email", result.email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new DtoJwtToken { AccessToken = tokenHandler.WriteToken(token) };

           // return StatusCode(StatusCodes.Status200OK, accessToken);

         
        }

        public List<DtoUserprofile> GetAllUsers()
        {
            var result = _userReposiroty.GetAllUsers().Result;
            return _mapper.Map<List<DtoUserprofile>>(result);
        }
        public DtoUserprofile GetUsers(int userid)
        {
            var result = _userReposiroty.GetUser (userid);
            return _mapper.Map<DtoUserprofile>(result);
        }

        public static string Encodepwd(string pwd)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(pwd);

            // Encode the byte array to Base64 string
            return  Convert.ToBase64String(byteArray);
        }
        public static string decodepwd(string pwd)
        {
            byte[] byteArray = Convert.FromBase64String(pwd);
            // Convert the byte array to the original string (using UTF8 encoding)
           return Encoding.UTF8.GetString(byteArray);
        }

        public void Updatepwd(DtoUserprofile userdetails)
        {
            var users = _mapper.Map<User>(userdetails);
            var userdata = new User()
            {
                user_id=users.user_id,
                email = users.email,
                pwd = Encodepwd(users.pwd),
               
              
            };
            _userReposiroty.Updatepwd(userdata);
        }
    }
}
