using System.Collections.Generic;
using System.Threading.Tasks;
using General.Core.Dapper;
using General.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace General.Api.Core.User
{
    /// <summary>
    /// 用户接口实现
    /// </summary>
    public class UserDao : IUserDao
    {
        private readonly IRepository<General.EntityFrameworkCore.User.User> _userRepository;
        private readonly IDapperClient _dapperClient;
        public UserDao(IRepository<General.EntityFrameworkCore.User.User> userRepository, IDapperClient dapperClient)
        {
            _userRepository = userRepository;
            _dapperClient = dapperClient;
        }
        /// <summary>
        /// <see cref="IUserDao.GetList"/>
        /// </summary>
        public async Task<List<General.EntityFrameworkCore.User.User>> GetList()
        {
            //var data = _dapperClient.Query<EntityFrameworkCore.User.User>("SELECT * FROM  dbo.[User]");
            //return data;
            return await _userRepository.Table.ToListAsync();
        }
    }
}