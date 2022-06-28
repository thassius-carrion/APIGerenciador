using AutoMapper;
using Manager.API.Utilities;
using Manager.API.ViewModel;
using Manager.Core.Exceptions;
using Manager.Service.DTO;
using Manager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/api/v1/users/create")]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel userViewModel)
        {
            try
            {
                var userDTO = _mapper.Map<UserDTO>(userViewModel);

                var userCreated = await _userService.Create(userDTO);

                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "User created successfully",
                    Data = userCreated
                });

            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }

            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }

        }

        [HttpPut]
        [Route("/api/v1/users/update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserViewModel updateUserViewModel)
        {
            try
            {
                var userDTO = _mapper.Map<UserDTO>(updateUserViewModel);

                var userUpdated = await _userService.Update(userDTO);

                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "User updated successfully!",
                    Data = userUpdated
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpDelete]
        [Route("/api/v1/users/delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var userDeleted = await _userService.Get(id);
                await _userService.Remove(id);

                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "User deleted successfully!",
                    Data = userDeleted
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }


    }
}
