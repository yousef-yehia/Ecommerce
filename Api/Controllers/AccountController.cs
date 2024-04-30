using Api.ApiResponse;
using Api.Dto;
using Api.Extensions;
using AutoMapper;
using Core.Interfaces;
using Core.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly APIResponse _apiResponse;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService, IMapper mapper, APIResponse apiResponse)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _apiResponse = apiResponse;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

            var userToReturn = new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };

            var response = _apiResponse.OkResponse(userToReturn);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<APIResponse>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(_apiResponse.UnauthorizedResponse());

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(_apiResponse.UnauthorizedResponse());

            var userToReturn = new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };

            var response = _apiResponse.OkResponse(userToReturn);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<APIResponse>> Register(RegisterDto registerDto)
        {
            try
            {
                if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
                {
                    
                    return BadRequest(_apiResponse.BadRequestResponse("Email address is in use"));
                }

                var user = new AppUser
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    UserName = registerDto.Email
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (!result.Succeeded) return BadRequest(_apiResponse.BadRequestResponse(""));

                var userToReturn = new UserDto
                {
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                    DisplayName = user.DisplayName
                };

                var response = _apiResponse.OkResponse(userToReturn);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return (_apiResponse.BadRequestResponse(ex.Message));
            }

        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpGet("address")]
        public async Task<ActionResult<APIResponse>> GetUserAddress()
        {
            try
            {
                var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);

                var result = _mapper.Map<Address, AddressDto>(user.Address);
                return Ok(_apiResponse.OkResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(_apiResponse.BadRequestResponse(ex.Message));
            }

        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<APIResponse>> UpdateUserAddress(AddressDto address)
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);

            user.Address = _mapper.Map<AddressDto, Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var resultToReturn = _mapper.Map<AddressDto>(user.Address);
                return Ok(_apiResponse.OkResponse(resultToReturn));

            }

            return BadRequest(_apiResponse.BadRequestResponse("Problem updating the user"));
        }
    }
}
