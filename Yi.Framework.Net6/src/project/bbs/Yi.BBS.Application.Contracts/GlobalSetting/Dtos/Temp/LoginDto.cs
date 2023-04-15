using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.BBS.Application.Contracts.GlobalSetting.Dtos.Temp
{
    public class LoginDto
    {
        public LoginDto(string token)
        {
            Token = token;
        }
        public string Token { get; set; }
        public LoginUserInfoDto  User{get;set;}
    }

    public class LoginUserInfoDto
    {
        
        public long Id { get; set; }

        public string UserName { get; set; }

        public int Level { get; set; }

        public string Icon { get; set; }
    }
}
