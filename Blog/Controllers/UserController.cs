using System;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok(await _userService.GetAll());
            }
            catch (UserException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _userService.Get(id));
            }
            catch (UserException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            try
            {
                await _userService.Create(userDto);

                return Ok();
            }
            catch (UserException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("UpdateUser/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserDto userDto)
        {
            try
            {
                await _userService.Update(id, userDto);

                return Ok();
            }
            catch (UserException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("DeleteUser/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            try
            {
                await _userService.Delete(id);

                return Ok();
            }
            catch (UserException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}