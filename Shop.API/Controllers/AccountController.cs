using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Errors;
using Shop.API.Extensions;
using Shop.Core.Dto;
using Shop.Core.Entities;
using Shop.Core.Services;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenServices _tokenServices;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, ITokenServices tokenServices, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _tokenServices = tokenServices;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return Unauthorized(new BaseCommonResponse(401));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (result.Succeeded == false || result == null)
            {
                return Unauthorized(new BaseCommonResponse(401));
            }

            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenServices.CreateToken(user)
            });
        }

        [HttpGet("check-email-exist")]
        public async Task<ActionResult<bool>> CheckEmailExist([FromQuery] string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if (result != null)
            {
                return true;
            }
            return false;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (CheckEmailExist(dto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new APIValidationErrorResponse(
                    ["Este correo electrónico se encuentra registrado."]
                    ));
            }

            var user = new AppUser
            {
                DisplayName = dto.DisplayName,
                UserName = dto.Email,
                Email = dto.Email,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded == false)
            {
                return BadRequest(new BaseCommonResponse(400));
            }

            return Ok(new UserDto
            {
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                Token = _tokenServices.CreateToken(user)
            });
        }

        [Authorize]
        [HttpGet("test")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult<string> Test()
        {
            return "Ok";
        }

        [Authorize]
        [HttpGet("get-current-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.FindEmailByClaimPrincipal(HttpContext.User);
            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenServices.CreateToken(user)
            });
        }

        [Authorize]
        [HttpGet("get-user-address")]
        public async Task<IActionResult> GetUserAddress()
        {
            var user = await _userManager.FindUserByClaimPrincipamWithAddress(HttpContext.User);

            var result = _mapper.Map<Address, AddressDto>(user.Address);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("update-user-address")]
        public async Task<IActionResult> UpdateUserAddress(AddressDto dto)
        {
            var user = await _userManager.FindUserByClaimPrincipamWithAddress(HttpContext.User);
            user.Address = _mapper.Map<AddressDto, Address>(dto);

            var result = _userManager.UpdateAsync(user);
            if (result.IsCompletedSuccessfully)
            {
                return Ok(_mapper.Map<Address, AddressDto>(user.Address));
            }
            return BadRequest($"Problema al actualizar este {HttpContext.User}");
        }

    }
}
