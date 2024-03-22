using ServerLibrary.Repositories.Contracts;
using BaseLibrary.DTOs;
using BaseLibrary.Responses;

using Microsoft.Extensions.Options;
using ServerLibrary.Helpers;
using ServerLibrary.Data;
using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ServerLibrary.Repositories.Implementations
{
    internal class UserAccountRepository(IOptions<JwtSection> config, AppDbContext appDbContext) : IUserAccount
    {
        public async Task<GeneralResponse> CreateAsync(Register User)
        {
            if (User == null) return new GeneralResponse(false, "Model is Empty");

            var checkUser = await FindUserByEmail(User.Email!);
            if (checkUser != null) return new GeneralResponse(false, "User registered already");

            //save user
            var applicationUser = await AddToDataBase(new ApplicationUser()
            {
                Fullname = User.Fullname,
                Email = User.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(User.Password),
            });

            //check, create and assign role

        }

        public Task<LoginResponse> SignInAsync(Login user)
        {
            throw new NotImplementedException();
        }
        private async Task<ApplicationUser> FindUserByEmail(string email) => 
            await appDbContext.ApplicationUsers.FirstOrDefaultAsync(_ => _.Email!.ToLower()!.Equals(email!.ToLower()));
        
        private async Task<T> AddToDataBase<T>(T model)
        {
            var result = appDbContext.Add(model!);
            await appDbContext.SaveChangesAsync();

            return (T)result.Entity;
        }
    }
}
