﻿using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<int> Add(User user);
        Task PostSkill(User user);
        Task<bool> Exists(int id);
        Task<User> GetUserByEmailAndPassworHashAsync(string email, string passwordHash);

    }
}
