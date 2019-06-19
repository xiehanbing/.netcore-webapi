using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.User;
using General.Api.Application.User.Dto;
using General.Api.Application.User.Request;
using General.Api.Framework;
using General.Api.Framework.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.Api.Controllers
{
    /// <summary>
    /// UserController
    /// </summary>
    [Route("api/[controller]"), Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        /// <summary>
        /// construct
        /// </summary>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="query">参数</param>
        /// <returns></returns>
        [HttpPost, Route("list")]
        public async Task<ListBaseResponse<UserResponse>> GetList(UserQuery query)
        {
            return await _userService.GetUserList(query);
        }
    }
}