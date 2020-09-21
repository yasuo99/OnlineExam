using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LuyenThiOnline.DTOs;
using LuyenThiOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace LuyenThiOnline.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _db;
        public AuthRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Exists(string username)
        {
            return await _db.Accounts.AnyAsync(u => u.Username == username);
        }

        public async Task<Account> Login(string username, string password)
        {
            var accountFromDb = await _db.Accounts.FirstOrDefaultAsync(u => u.Username == username);
            if (accountFromDb == null)
            {
                return null;
            }
            if (ComparePassword(password, accountFromDb.PasswordHash, accountFromDb.PasswordSalt))
            {
                return accountFromDb;
            }
            else
            {
                return null;
            }
        }

        private bool ComparePassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var tempPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                if (tempPasswordHash.Length != passwordHash.Length)
                {
                    return false;
                }
                for (int i = 0; i < tempPasswordHash.Length; i++)
                {
                    if (tempPasswordHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public async Task<bool> SaveAll(){
            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<Account> Register(Account account, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreateHashedPassword(password, out passwordHash, out passwordSalt);
            account.PasswordHash = passwordHash;
            account.PasswordSalt = passwordSalt;
            account.LevelId = 1;
            account.DateOfBirth = account.DateOfBirth;
            await _db.AddAsync(account);
            await _db.SaveChangesAsync();
            return account;
        }

        private void CreateHashedPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<Account> GetUser(int id)
        {
            return await _db.Accounts.AsNoTracking().Include(u => u.Badges).ThenInclude(u => u.Badge).Include(u => u.Certificates).ThenInclude(u => u.Certificate).Include(u => u.Level).Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
        }
        
        public async Task<Account> GetUser(string username)
        {
            return await _db.Accounts.AsNoTracking().Include(u => u.Badges).ThenInclude(u => u.Badge).Include(u => u.Certificates).ThenInclude(u => u.Certificate).Include(u => u.Level).Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == username);
        }
        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _db.Accounts.Include(u => u.Badges).ThenInclude(u => u.Badge).Include(u => u.Certificates).ThenInclude(u => u.Certificate).Include(u => u.Level).Include(u => u.Role).ToListAsync();
        }

        public async Task<Account> UpdateUser(Account account)
        {
            if (_db.Entry(account).State == EntityState.Detached)
            {
                _db.Attach(account);
            }
            _db.Entry(account).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return account;
        }

        public async Task<Account> ChangePassword(AccountForChangePWDTO account)
        {
            var accountFromDb = await GetUser(account.Username);
            if(!ComparePassword(account.OldPassword, accountFromDb.PasswordHash, accountFromDb.PasswordSalt))
            {
                return null;
            }
            byte[] hasedPassword, passwordSalt;
            CreateHashedPassword(account.NewPassword,out hasedPassword,out passwordSalt);
            accountFromDb.PasswordHash = hasedPassword;
            accountFromDb.PasswordSalt = passwordSalt;
            await UpdateUser(accountFromDb);
            return accountFromDb;
        }

        public async Task<Account> GetUserTracking(int id)
        {
            return await _db.Accounts.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}