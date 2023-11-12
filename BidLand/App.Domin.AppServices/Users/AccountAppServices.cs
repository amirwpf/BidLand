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
using App.Domin.Core._02_Users.ViewModels;
using App.Domin.Core._02_Users.Contracts.Services;
using App.Domin.Core._02_Users.Enums;
using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;

namespace App.Domin.AppServices.Users
{
    public class AccountAppServices : IAccountAppServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IBuyerService _buyerService;
        private readonly ISellerService _sellerService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountAppServices(UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            SignInManager<User> signInManager,
            IHttpContextAccessor httpContextAccessor,
            ISellerService sellerService,
            IBuyerService buyerService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _sellerService = sellerService;
            _buyerService = buyerService;
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
                role = new IdentityRole<int>(roleName);
                await _roleManager.CreateAsync(role);
                return "نقش جدید ایجاد شد";
            }
            return "نقش موجود است";
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterViewModel user, CancellationToken token)
        {

            var registerd = await _userManager.CreateAsync(
                new User()
                {
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Email = user.Username,
                    UserName = user.Username,
                    PhoneNumber = user.PhoneNumber

                }, user.Password);

            if (registerd.Succeeded)
            {
                var _user = await _userManager.FindByEmailAsync(user.Username);
                if (user.BuyerOrSeller == BuyerSellerTypes.Buyer)
                {

                    await _buyerService.CreateAsync(new BuyerRepoDto()
                    {
                        UserId = _user.Id,
                        InsertionDate = DateTime.Now

                    }, token);
                }
                else if (user.BuyerOrSeller == BuyerSellerTypes.Seller)
                {
                    await _sellerService.CreateAsync(new SellerRepoDto()
                    {
                        UserId = _user.Id,
                        InsertionDate = DateTime.Now

                    }, token);
                }
            }
            return registerd;
        }

        public async Task<bool> DeleteBuyerUserAsync(int id, CancellationToken cancellationToken)
        {
            BuyerRepoDto buyerRepoDto = await _buyerService.GetByIdAsync(id, cancellationToken);
            if (buyerRepoDto != null)
            {
                await _buyerService.DeleteAsync(buyerRepoDto, cancellationToken);
                return true;
            }
            return false;

        }

        public async Task<bool> DeleteSellerUserAsync(int id, CancellationToken cancellationToken)
        {
            SellerRepoDto seller = await _sellerService.GetByIdAsync(id, cancellationToken);
            if (seller != null)
            {
                await _sellerService.DeleteAsync(seller, cancellationToken);
                return true;
            }
            return false;

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

        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);

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
                //UserRoles = user.Roles

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
                //  UserRoles = user.Roles

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
                //UserRoles = x.Roles
            }).ToList();
        }

        public async Task<List<IdentityRole<int>>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<BuyerRepoDto> GetBuyerByIdAsync(int id, CancellationToken token)
        {
            return await _buyerService.GetByIdAsync(id, token);
        }

        public async Task<List<BuyerRepoDto>> GetBuyerDeletedUsersAsync(CancellationToken cancellationToken)
        {
            return await _buyerService.GetAllDeletedAsync(cancellationToken);
        }
        public async Task<List<BuyerRepoDto>> GetBuyerUsersAsync(CancellationToken cancellationToken)
        {
            return await _buyerService.GetAllAsync(cancellationToken);
        }

        public async Task<string> GetLoggedInUserId()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var userId = await _userManager.GetUserIdAsync(user);
            return userId;
        }

        public async Task<SellerRepoDto> GetSellerByIdAsync(int id, CancellationToken token)
        {
            return await _sellerService.GetByIdAsync(id, token);
        }

        public async Task<List<SellerRepoDto>> GetSellerDeletedUsersAsync(CancellationToken cancellationToken)
        {
            return await _sellerService.GetAllDeletedAsync(cancellationToken);
        }
        public async Task<List<SellerRepoDto>> GetSellerUsersAsync(CancellationToken cancellationToken)
        {
            return await _sellerService.GetAllAsync(cancellationToken);
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

        public async Task UpdateBuyerAsync(BuyerRepoDto model, CancellationToken cancellationToken)
        {
            await _buyerService.UpdateAsync(model, cancellationToken);
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

        public async Task UpdateSellerAsync(SellerRepoDto model, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            //if (user != null)
            //{
            //    user.Firstname = model.User.Firstname;
            //    user.Lastname = model.User.Lastname;
            //    await _userManager.UpdateSecurityStampAsync(user);
            //    await _userManager.UpdateAsync(model.User);

            //}
            await _sellerService.UpdateAsync(model, cancellationToken);
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
