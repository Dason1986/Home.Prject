using Home.DomainModel.Repositories;
using HomeApplication.Cores;
using HomeApplication.Dtos;
using HomeApplication.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace HomeApplication.Owin.API
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class UserProfile
    {

    }
    public class PassportController : WebAPI
    {

        public PassportController()
        {

        }
        [Route("api/authenticate")]
        [Route("api/authenticate/login")]
        [AllowAnonymousAttribute]
        [HttpPost]
        public HttpResponseMessage Login([FromBody]UserLogin user)
        {
            // 为了简化不做用户名和密码验证
            var expire = DateTime.UtcNow.AddHours(2);
            var res = new TokenResult()
            {
                Expires = expire
            };
            var payloadObj = new JWTPayload()
            {
                Name = user.Username,
                Role = "manage,user",
                Exp = expire
            };
            res.Token = new JWT().Encode<JWTPayload>(payloadObj, PassportConfig.SecretKey);
            var message = new HttpResponseMessage();
            message.Content = new System.Net.Http.ObjectContent<TokenResult>(res, this.Configuration.Formatters.JsonFormatter);

            message.Headers.Add("Authorization", "Bearer " + res.Token);
            return message;
        }
        [Route("api/account")]
        public HttpResponseMessage GetUserProfile()
        {
            var message = new HttpResponseMessage();
            message.Content = new System.Net.Http.ByteArrayContent(System.IO.File.ReadAllBytes(@"api\mock\loginaccount.json"));
            return message;
        }


        [Route("api/account/{id}")]
        public HttpResponseMessage GetUserProfile(string id)
        {
            var message = new HttpResponseMessage();
            message.Content = new System.Net.Http.ByteArrayContent(System.IO.File.ReadAllBytes(@"api\mock\loginaccount.json"));
            return message;
        }
    }
}