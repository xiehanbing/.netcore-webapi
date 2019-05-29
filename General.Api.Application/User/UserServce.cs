using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace General.Api.Application.User
{
    public class UserServce : IUserService
    {
        private readonly General.Api.Core.User.IUserDao _userDao;
        private readonly IMapper _mapper;
        public UserServce(General.Api.Core.User.IUserDao userDao, IMapper mapper)
        {
            _userDao = userDao;
            _mapper = mapper;
        }

        public async Task<List<Dto.UserDto>> GetList()
        {
            return _mapper.Map<List<Dto.UserDto>>(await _userDao.GetList());
        }
    }
}