using Common;
using DataAccess;
using Microsoft.IdentityModel.Tokens;
using Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserBL : IUserBL
    {

        private readonly IUserDAL _userDAL;

        public UserBL(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }

        public async Task<string> CreateUserAsync(LoginDto loginDto)
        {
            var user = await _userDAL.CreateUserAsync(loginDto);
            return user;
        }

        public async Task<UserDto> GetUserDetailsAsyncById(int userId)
        {
            var userDetails = await _userDAL.GetUserDetailsAsyncById(userId);
            return userDetails;
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var userDetails = await _userDAL.LoginAsync(loginDto);
            if (userDetails != null) userDetails.Token = generateJwtToken(userDetails, 5);
            return userDetails!;
        }

        private string generateJwtToken(UserDto user, int expirationTime)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();


            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(expirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
