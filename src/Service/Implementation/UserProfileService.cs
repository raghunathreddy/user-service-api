using AutoMapper;
using Service.DtoModels;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Model;
using UserService.Repository.Implementation;
using UserService.Repository.Interface;

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

        public DtoUserprofile GetAllUser(string emailid, string pwd)
        {
            var result = _userReposiroty.GetAllUser(emailid, Encodepwd(pwd));
            return _mapper.Map<DtoUserprofile>(result);
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
