﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IUserDAL
    {
        Task<UserDto> GetUserDetailsAsyncById(int userId);

        Task<UserDto> LoginAsync(LoginDto loginDto);

        Task<string> CreateUserAsync(LoginDto loginDto);

    }
}
