using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DtoModels
{
    public class DtoUserprofile
    {
        public int? user_id { get; set; }
        public string? email { get; set; }
        public string? pwd { get; set; }
        public string? username { get; set; }
        public string? user_address { get; set; }
        public string? favorite_genres { get; set; }
        public string? reading_preferences { get; set; }
    }
}
