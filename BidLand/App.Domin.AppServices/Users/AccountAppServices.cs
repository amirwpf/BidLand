using App.Domin.Core._02_Users.Contracts.AppServices;
using App.Domin.Core._02_Users.Dtos;
using App.Domin.Core._02_Users.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace App.Domin.AppServices.Users
{
    public class AccountAppServices : IAccountAppServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountAppServices(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AssignUserToRole(string userEmail, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task AssignUserToRoleByUserId(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<string> CreateRoleIfNotExists(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                role = new Role(roleName);
                await _roleManager.CreateAsync(role);
                return "نقش جدید ایجاد شد";
            }
            return "نقش موجود است";
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterDto user)
        {
            return await _userManager.CreateAsync(
                new User() { 
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                UserName = user.Email,
                PhoneNumber = user.PhoneNumber

                }, user.Password);

        }

        public async Task<string> DeleteUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {

                    return "کاربر مورد نظر حذف گردید!";
                }

            return "خطا در حذف کاربر مورد نظر!";
            }
            return "کاربر مورد نظر یافت نشد!";
        }

        public async Task<UserDto> FindUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                FullName = user.Firstname + " " + user.Lastname,
                UserRoles = user.Roles

            };
        }

        public async Task<UserDto> FindUserByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            return new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                FullName = user.Firstname + " " + user.Lastname,
                UserRoles = user.Roles

            };

        }

        public async Task<UserDto> FindUserIdByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                FullName = user.Firstname + " " + user.Lastname,
                UserRoles = user.Roles

            };
        }

        public async Task<List<UserDto>> FindUsersByRole(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            return users.Select(x => new UserDto()
            {
                Id = x.Id,
                UserName = x.UserName,
                Firstname = x.Firstname,
                Lastname = x.Lastname,
                FullName = x.Firstname + " " + x.Lastname,
                Email = x.Email,
                UserRoles = x.Roles
            }).ToList();
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<string> GetLoggedInUserId()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var userId = await _userManager.GetUserIdAsync(user);
            return userId;
        }

        public async Task<SignInResult> SignInUserAsync(User user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var result = await _signInManager.PasswordSignInAsync(user,
               password, isPersistent, lockoutOnFailure);
            return result;
        }

        public async Task SignOutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string> UpdatePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return "کاربر یافت نشد";
            }

            var passwordCorrect = await _userManager.CheckPasswordAsync(user, currentPassword);
            if (!passwordCorrect)
                return "رمز عبور فعلی نادرست است.";


            // تغییر رمز عبور کاربر
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded)
                return "گذرواژه تغییر کرد";
            return "تغییر گذرواژه با خطا مواجه شد.";
        }

        public async Task<string> UpdateUserAsync(UserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id.ToString());
            if (user == null)
            {
                return "کاربر یافت نشد";
            }

            user.Id = userDto.Id;
            user.Firstname = userDto.Firstname;
            user.Lastname = userDto.Lastname;
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            var security = await _userManager.UpdateSecurityStampAsync(user);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return "ویرایش موفقیت آمیز بود";
            }
            else
            {
                return "ویرایش با شکست مواجه شد";
            }
        }
    }
}
