using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Core.Entities;
using Core.Interfaces;
using API.Dtos;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Core.Entities.AccountModels;
using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountController(                               
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                ITokenService tokenService,
                                IMapper mapper
                                )
        {
            
            _userManager = userManager;
            _signInManager = signInManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<bool> LogOff()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            {
                Email = user.Email,
                Token = tokenService.CreateToken(user),
                UserName = user.DisplayName
            };
        }
        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("[action]")]
        
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);
            return mapper.Map<Address, AddressDto>(user.Address);
        }
        [Authorize]
        [HttpGet("[action]")]
        
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);
            user.Address = mapper.Map<AddressDto, Address>(address);
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Ok(mapper.Map<Address, AddressDto>(user.Address));
            else
                return BadRequest("problem updaing user");
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginViewModel loginViewModel)
        {
 
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe = false, lockoutOnFailure: false);
            var user = await _userManager.FindByNameAsync(loginViewModel.Email);
            
            if( user == null)
            {
                return Unauthorized();
            }

            if (result.Succeeded )
            {
                return new UserDto
                {
                    Email = user.Email,
                    Token = tokenService.CreateToken(user),
                    UserName = user.DisplayName
                };
            }
            return Unauthorized();

        }
        [HttpGet("[action]")]
        public bool EmailExits(string email)
        {
            return CheckEmailExistsAsync(email).Result.Value;
     
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterModel model, string returnUrl = null)
        {
            if (CheckEmailExistsAsync(model.Email).Result.Value)
            {
                return  BadRequest("already exits the user");
            }
            var user = new ApplicationUser
            {
                UserName = model.userName,
                Email = model.Email,
                DisplayName = "TM Hridoy",
                Address = new Address
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Tareq Mahmud",
                    LastName = "Hridoy",
                    Street = "F",
                    City = "Khulna",
                    State = "Khalispur",
                    ZipCode = "9000",
                    ApplicationUserId = "user101"
                }
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //  await _signInManager.SignInAsync(user, isPersistent: false);
                return new UserDto
                {
                    Email = user.Email,
                    Token = tokenService.CreateToken(user),
                    UserName = user.UserName
                };
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
