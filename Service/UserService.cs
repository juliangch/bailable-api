﻿using bailable_api.Database;
using bailable_api.Dtos;
using bailable_api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace bailable_api.Service
{
    public interface IUserService
    {
        User GetUserById(Guid id);
        int CreateUser(RegisterUserRequestDto userDto);
    }

    public class UserService : IUserService
    {
        private readonly UserDao userDao;

        public UserService(ContextDb contextDb)
        {
            userDao = new UserDao(contextDb);
        }

        public User GetUserById(Guid id)
        {
            var user = userDao.GetUserById(id);
            return user;
        }

        public int CreateUser(RegisterUserRequestDto userDto) 
        { 
            return userDao.CreateUser(userDto);
        }
    }
}