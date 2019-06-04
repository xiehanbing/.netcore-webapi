using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.User;
using General.Api.Application.User.Dto;
using General.Api.Framework;
using General.Log;
using General.Log.Entity;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace General.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogManager _log;
        private readonly IUserService _userService;
        public ValuesController(ILogManager log, IUserService userService)
        {
            _log = log;
            _userService = userService;
        }
        // GET api/values
        /// <summary>
        /// 这是一个注释
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ApiResult<List<UserDto>>> Get(int id)
        {
            var data = await _userService.GetList();
            //throw new MyException(100030,"这是一个测试");
            return new ApiResult<List<UserDto>>(data);
            //return new ApiResult<string>("value");
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _log.Debug("这是一个测试信息Debug");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new Exception("这是一个测试");
        }

        // DELETE api/values/5
        [HttpDelete("{id}"),Authorize(policy: "ageRequire")]
        public void Delete(int id)
        {
            _log.Error("这是一个测试信息Error");
        }
    }
}
