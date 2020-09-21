using System.Threading.Tasks;
using LuyenThiOnline.Models;

namespace LuyenThiOnline.Data
{
    public interface IAuthorRepository
    {
         Task<Role> CreateRole(Role role);
         Task<bool> AddAccountToRole(Account account, string roleName);
         Task<bool> Exists(string roleName);
         Task<bool> IsAccountInRole(Account account, string roleName);
    }
}