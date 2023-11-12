﻿using App.Domin.Core._02_Users.Dtos;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._02_Users.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Contracts.AppServices
{
    public interface IAccountAppServices
    {
        Task<string> UpdatePasswordAsync(int userId, string currentPassword, string newPassword);
        Task AssignUserToRoleByUserId(string userId, string roleName);
        Task<string> GetLoggedInUserId();
        Task<string> UpdateUserAsync(UserDto userDto);
        Task<UserDto> FindUserByIdAsync(int id);
        Task<string> DeleteUserAsync(string email);
        Task<List<IdentityRole<int>>> GetAllRoles();
        Task<List<UserDto>> FindUsersByRole(string roleName);
        Task<IdentityResult> CreateUserAsync(RegisterViewModel model, CancellationToken token);
        Task<string> CreateRoleIfNotExists(string roleName);
        Task AssignUserToRole(string userEmail, string roleName);
        Task<UserDto> FindUserIdByEmailAsync(string email);
        Task<User> FindUserByEmailAsync(string email);
        Task<SignInResult> SignInUserAsync(User user, string password, bool isPersistent, bool lockoutOnFailure);
        Task SignOutUserAsync();
    }
}