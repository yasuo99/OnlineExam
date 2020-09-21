using System.Linq;
using System.Threading.Tasks;
using LuyenThiOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace LuyenThiOnline.Data
{
    public class AuthoRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _db;
        public AuthoRepository(ApplicationDbContext db)
        {
            _db = db;

        }
        public async Task<bool> AddAccountToRole(Account account, string roleName)
        {
            try
            {
                if (await Exists(roleName))
                {
                    var role = await _db.Roles.FirstOrDefaultAsync(u => u.RoleName == roleName);
                    account.RoleId = role.RoleId;
                    _db.Update(account);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else{
                    Role role = new Role()
                    {
                        RoleName = roleName
                    };
                    var newRole = await CreateRole(role);
                     account.RoleId = role.RoleId;
                    _db.Update(account);
                    await _db.SaveChangesAsync();
                    return true;
                }
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        public Task<Role> CreateRole(Role role)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Exists(string roleName)
        {
            return await _db.Roles.AnyAsync(u => u.RoleName == roleName);
        }

        public Task<bool> IsAccountInRole(Account account, string roleName)
        {
            throw new System.NotImplementedException();
        }
    }
}