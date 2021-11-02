using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.Common.Repositories;
using BarberShop.DAL.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.DAL.EF.Repositories
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<User> GetByNickName(string nickName)
        {
            return await this.GetFirstOrDefaultAsync(x => x.NickName == nickName);
        }

        public IEnumerable<User> GetRangeWithRole(int skipPos = 0, int count = 10)
            => DbSet.Include(x => x.Role).Skip(skipPos).Take(count);
    }
}
