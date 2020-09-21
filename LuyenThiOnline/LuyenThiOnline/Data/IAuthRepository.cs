using System.Collections.Generic;
using System.Threading.Tasks;
using LuyenThiOnline.DTOs;
using LuyenThiOnline.Models;

namespace LuyenThiOnline.Data
{
    public interface IAuthRepository
    {
        Task<Account> Register(Account account,string password); 
        Task<Account> Login(string username,string password);
        Task<Account> GetUser(int id);
        Task<Account> GetUserTracking(int id);
        Task<Account> GetUser(string username);
        Task<IEnumerable<Account>> GetAll();
        Task<Account> UpdateUser(Account account);
        Task<Account> ChangePassword(AccountForChangePWDTO account);
        Task<bool> Exists(string username);
        Task<bool> SaveAll();
    }
}