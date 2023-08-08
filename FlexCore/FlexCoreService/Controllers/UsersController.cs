using EFModels.Models;
using FlexCoreService.UserCtrl.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Xml.Linq;

namespace FlexCoreService.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsersController(AppDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;

        }

        /// <summary>
        /// 取得會員資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{account}")]
        //[Authorize]//登入之後才能使用
        public async Task<ProfileDto> GetUserProfil(string account)
        {
            if (_db.Members == null)
            {
                return null;
            }
            ProfileDto proDto = _db.Members.Include(m => m.fk_Level).Where(m => m.Account == account).Select(m => new ProfileDto
            {
                MemberId = m.MemberId,
                fk_Level = m.fk_LevelId,
                LevelName = m.fk_Level.LevelName,
                Name = m.Name,
                Email = m.Email,
                Mobile = m.Mobile,
                Gender = m.Gender,
                Birthday = m.Birthday,
                CommonAddress = m.CommonAddress,
                AlternateAddress1 = m.AlternateAddress.AlternateAddress1,
                IsSubscribeNews = m.IsSubscribeNews
            }).Single();
            return proDto;
        }

        /// <summary>
        /// 登入中
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public string Login([FromBody]LoginDto value)
        {
            var user = (from m in _db.Members
                        where m.Account == value.Account                      
                        select m).SingleOrDefault();

            if (user == null)
            {
                //欄位或帳號驗證失敗
                return "帳號錯誤";
            }
            else
            {
                //帳號驗證成功
                //驗證密碼
                var userPassword = (from m in _db.Members where m.EncryptedPassword == value.EncryptedPassword select m).SingleOrDefault();

                //
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Account),
                    new Claim("FullName", user.Name),
                    new Claim("MemberId",user.MemberId.ToString())//MemberId為自訂宣告的Type名稱
                   // new Claim(ClaimTypes.Role, "Administrator")//管理角色
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //控制登入狀態
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return user.Account;
            }
        }


        //public void ss (LoginDto value)
        //{
        //    var Claim = _contextAccessor.HttpContext.User.Claims.First();
        //    //var memId=Claim.Where(m=>m.Type=="MemberId").FirstOrDefault();
        //    //12-3
        //}


        /// <summary>
        /// 登出
        /// </summary>
        [HttpDelete]
        [AllowAnonymous]//不需要身分驗證
        public void Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// 未登入
        /// </summary>
        /// <returns></returns>
        [HttpGet("NoLogin")]
        public string NoLogin()
        {
            return "未登入";
        }

    }
}
