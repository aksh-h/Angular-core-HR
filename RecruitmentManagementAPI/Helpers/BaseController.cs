using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace RecruitmentManagementAPI.Controllers
{
    public class BaseController : ControllerBase
    {
      public BaseController()
        {

        }
        public HttpResponseMessage CreateResponse<T>(HttpStatusCode statusCode, T content) 
        { 
            return new HttpResponseMessage() { StatusCode = statusCode, Content = new StringContent(JsonConvert.SerializeObject(content)) }; 
        }

        public string GenerateToken(string UserName , string Role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(GetConfigValue("AppSecretKey")));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, UserName),
                    new Claim(ClaimTypes.Role, Role)
                }),
                Expires = DateTime.UtcNow.AddDays(70),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            IdentityModelEventSource.ShowPII = true;
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringtoken = tokenHandler.WriteToken(token);
            return stringtoken;
        }

        public static string GetConfigValue(string Key)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            return root.GetSection(Key).Value;

        }

    }
}