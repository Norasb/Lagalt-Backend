﻿using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Services.UserServices
{
    public interface IUserService : ICrudService<User, string>
    {
        public Task<bool> UserExists(string id);
    }
}
