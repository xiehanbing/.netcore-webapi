using System.Collections.Generic;
using System.Threading.Tasks;

namespace General.Api.Core.User
{
    public interface IUserDao
    {
        Task<List<General.EntityFrameworkCore.User.User>> GetList();
    }
}