﻿using API.Extentions;
using AutoMapper;
using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Repository.Identity;
using Standard.DTOs;
using Standard.Entities.Identity;
using System.ComponentModel;
using System.Security.Claims;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]

    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
             ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }


        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);

              
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreteToken(user),
                DisplayName = user.DisplayName,                        
            };
        }

        [Authorize]
        [HttpGet("Useraddress")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindByUserByClaimsPrinciplelWithAddressAsync(HttpContext.User);
            return _mapper.Map<Address, AddressDto>(user.Address);
        }


        [HttpPut("Updateaddress")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress([FromBody] AddressDto address)
        {
            var user = await _userManager.FindByUserByClaimsPrinciplelWithAddressAsync(HttpContext.User);

            user.Address = _mapper.Map<AddressDto, Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));

            return BadRequest("Problem updating the user");
        }






        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid credentials");
            }
           
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreteToken(user),
                DisplayName = user.DisplayName,


            };
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(CheckEmailAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult("Email Address is in use");
            }
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) // Corrected to check if the result was not successful
            {
                return BadRequest(result.Errors);
            }
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreteToken(user)
            };
        }
    }
}
