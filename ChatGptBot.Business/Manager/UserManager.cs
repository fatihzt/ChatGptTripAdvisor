using ChatGptBot.Business.Service;
using ChatGptBot.DataAccess.Abstract;
using ChatGptBot.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptBot.Business.Manager
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IConfiguration _configuration;
        public UserManager(IUserDal userDal,IConfiguration configuration)
        {
            _userDal= userDal;
            _configuration= configuration;
        }
        public int Add(User entity)
        {
            return _userDal.Add(entity);
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null || password.Length < 9) { throw new ArgumentException("Password must be at least 8 characters long"); }
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public string CreateToken(User user)
        {
            var secureKey = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value);
            var claims = new[]
                    {
                            new Claim("Name",user.Name),
                            new Claim("Id",user.Id.ToString()),
                            new Claim("PasswordHash",user.PasswordHash.ToString()),
                            new Claim("PasswordSalt",user.PasswordSalt.ToString()),
                            new Claim("Surname",user.Surname),
                        };

            var securityKey = new SymmetricSecurityKey(secureKey);
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);


            var token = new JwtSecurityToken(
                _configuration.GetSection("Jwt:Issuer").Value,
                _configuration.GetSection("Jwt:Audience").Value,
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.Now.AddMinutes(10)

                );

            var tokens = new JwtSecurityTokenHandler().WriteToken(token);

            return tokens;
        }

        public bool Delete(User entity)
        {
            return _userDal.Delete(entity);
        }

        public User Get(Expression<Func<User, bool>> filter = null)
        {
            return _userDal.Get(filter);
        }

        public List<User> GetAll(Expression<Func<User, bool>> filter = null)
        {
            return _userDal.GetAll(filter);
        }

        public bool Update(User entity)
        {
            return _userDal.Update(entity);
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
