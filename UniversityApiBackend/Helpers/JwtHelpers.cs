using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversityApiBackend.Models.DataModels;
using Microsoft.IdentityModel.Tokens;

namespace UniversityApiBackend.Helpers
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens usersAccounts, Guid Id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", usersAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, usersAccounts.UserName),
                new Claim(ClaimTypes.Email, usersAccounts.EmailId ),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MM ddd dd yyyy HH:mm:ss tt")),
            };

            if(usersAccounts.UserName == "Admin" ) 
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrador"));
            }
            else if (usersAccounts.UserName == "User 1")
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("UserOnly", "User 1"));
            }
            return claims;

        }

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id) 
        {
            Id= Guid.NewGuid();
            return GetClaims(userAccounts, Id);
        }


        public static UserTokens GenTokenKey(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var userToken = new UserTokens();
                if(model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

                Guid Id;

                DateTime expireTime = DateTime.UtcNow.AddDays(1);

                userToken.Validity = expireTime.TimeOfDay;

                var jwToken = new JwtSecurityToken(
                    
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256)
                    
                    );
                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                userToken.UserName= model.UserName;
                userToken.Id= model.Id;
                userToken.GuidId = Id;

                return userToken;
            }
            catch (Exception exception)
            {
                throw new Exception("Error generando el JWT");
            }

        }
    }
}
