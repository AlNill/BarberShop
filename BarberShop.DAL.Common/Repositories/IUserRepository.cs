using System.Threading.Tasks;
using BarberShop.DAL.Common.Models;

namespace BarberShop.DAL.Common.Repositories
{
    public interface IUserRepository: IGenericRepository<User>
    {
        public Task<User> GetByNickName(string nickName);
    }
}
