using System.Collections.Generic;
using System.Threading.Tasks;

namespace General.Api.Application.User
{
    public interface IUserService
    {
        Task<List<Dto.UserDto>> GetList();


        //Task<List<>>
    }
}