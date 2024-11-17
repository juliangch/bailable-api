﻿using bailable_api.Dtos;
using bailable_api.Models;
using bailable_api.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace bailable_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public ActionResult GetUserById(Guid id)
        {
            User u = _userService.GetUserById(id);
            if (u == null) return NotFound();
            return Ok(u);
        }

        [HttpPost]
        public ActionResult RegisterUser([FromBody] RegisterUserRequestDto registerUserRequestDto)
        {
            return _userService.CreateUser(registerUserRequestDto) > 0 ? Created() : Problem("No se pudo registrar el usuario");
        }
    }
}