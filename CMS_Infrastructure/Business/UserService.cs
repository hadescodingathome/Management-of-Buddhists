using CMS_Core.Business;
using CMS_Core.LogFun;
using CMS_WebDesignCore.LogFun;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Infrastructure.Business
{
    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;

        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<RegisterRespone> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
                return new RegisterRespone
                {
                    Message = "Register Model is null",
                    IsSuccess = false,
                };

            if (model.Password != model.ConfirmPassword) 
                return new RegisterRespone
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);
            if (result.Succeeded)
            {
                // TODO: Send a confirmation Email

                return new RegisterRespone
                {
                    Message = "User created successfully",
                    IsSuccess = true,
                };
            }

            return new RegisterRespone
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<LoginRespone> LoginUserAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return new LoginRespone
                {
                    Message = "There is no user with that Email address",
                    IsSuccess = false,
                };

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new LoginRespone
                {
                    Message = "Invalid password",
                    IsSuccess = false,
                };

            var roles = await _userManager.GetRolesAsync(user); // Get the user's roles

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, model.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
    };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // Add each role as a claim
            }

            var key = _configuration["AuthSettings:Key"];
           
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var securityKey = new SymmetricSecurityKey(keyBytes);

            var token = new JwtSecurityToken(
              //  issuer: _configuration["AuthSettings:Issuer"],
             //   audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginRespone
            {
                Message = "Login successfully!",
                IsSuccess = true,
                Token = tokenAsString,
                ExpireDate = token.ValidTo,
                data = user
            };
        }
    }
}

