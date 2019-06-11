using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.User;
using General.Api.Application.User.Dto;
using General.Api.Framework;
using General.Core;
using General.Log;
using General.Log.Entity;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace General.Api.Controllers
{
    /// <summary>
    /// ValuesController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogManager _log;
        private readonly IUserService _userService;
        /// <summary>
        /// ValuesController
        /// </summary>
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
        /// <summary>
        /// Get(int)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ApiResult<List<UserDto>>> Get(int id)
        {
            throw new MyException(100030, "这是一个测试");
            var data = await _userService.GetList();
            //throw new MyException(100030,"这是一个测试");
            return new ApiResult<List<UserDto>>(data);
            //return new ApiResult<string>("value");
        }
        /// <summary>
        /// Post(string)
        /// </summary>
        /// <param name="value"></param>
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _log.Debug("这是一个测试信息Debug");
        }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new Exception("这是一个测试");
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/values/5
        [HttpDelete("{id}"),Authorize(policy: "General")]
        public void Delete(int id)
        {
            _log.Error("这是一个测试信息Error");
        }
    }
}
