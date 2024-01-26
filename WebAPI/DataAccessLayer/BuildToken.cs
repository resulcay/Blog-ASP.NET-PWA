using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebAPI.DataAccessLayer
{
    public class BuildToken
    {
        public string CreateToken() 
        {
            var bytes = Encoding.UTF8.GetBytes("mySuperSecret--withluck8andlongtextarea");
            SymmetricSecurityKey key = new(bytes);
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

            DateTime now = DateTime.Now;
            DateTime duration = now.AddSeconds(10);

            JwtSecurityToken token = new(
                issuer: "https://localhost",
                audience: "https://localhost",
                notBefore: now,
                expires: duration,
                signingCredentials: creds);

            JwtSecurityTokenHandler handler = new();
            string result = handler.WriteToken(token);

            return result;
        }
    }
}
