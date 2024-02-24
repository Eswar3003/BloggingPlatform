using Entities;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDAL : DatabaseContext, IUserDAL
    {

        private readonly DatabaseContext _dbContext;

        public UserDAL(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> CreateUserAsync(LoginDto loginDto)
        {

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var isUserExist = await _dbContext.User.FirstOrDefaultAsync(u => u.Username == loginDto.Username
                    && u.Password == loginDto.Password);

                    if (isUserExist == null)
                    {

                        User user = new()
                        {
                            Username = loginDto.Username,
                            Password = loginDto.Password
                        };

                        _dbContext.User.Add(user);
                        await _dbContext.SaveChangesAsync();

                        transaction.Commit();

                        return "New user successfully created";
                    }
                    else
                    {
                        return "User already exist";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    transaction.Rollback();
                }
            }

            return "";

        }

        public async Task<UserDto> GetUserDetailsAsyncById(int userId)
        {
            var userDetails = _dbContext.User.FirstOrDefault(u => u.Id == userId);
            if (userDetails == null) return null!;

            UserDto user = new()
            {
                Id = userDetails.Id,
                Username = userDetails.Username,
                Password = userDetails.Password
            };

            await Task.CompletedTask;

            return user;
        }


        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var userDetails =await _dbContext.User.FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.Password == loginDto.Password);
            if (userDetails == null) return null!;

            UserDto user = new()
            {
                Id = userDetails.Id,
                Username = userDetails.Username,
                Password = userDetails.Password
            };

            return user;
        }
    }
}
