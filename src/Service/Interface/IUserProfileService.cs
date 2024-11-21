using Service.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserProfileService
    {
        public DtoUserprofile GetAllUser(string emailid, string pwd);
        public List<DtoUserprofile> GetAllUsers();
        public DtoUserprofile GetUsers(int userid);
        void AddUserdetails(DtoUserprofile userdetails);
        void Updatepwd(DtoUserprofile userdetails);
    }
}
