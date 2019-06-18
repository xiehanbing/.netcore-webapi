using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.EventRevice;
using General.Api.Application.EventRevice.Dto;
using General.Api.Application.EventRevice.Dto.VerifyPerson;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.Api.Controllers
{
    /// <summary>
    /// 事件接收服务
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventReciveController : ControllerBase
    {
        private readonly IDoorApplicationService _applicationService;
        /// <summary>
        /// construct
        /// </summary>
        public EventReciveController(IDoorApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        /// <summary>
        /// 门禁认证对比成功
        /// </summary>
        /// <param name="model">参数</param>
        /// <returns></returns>
        [HttpPost,Route("verify/person/success")]
        public async Task<bool> VerifyPersonSuccess(EventReciveDto<VerifyPersonDto> model)
        {
            return await _applicationService.VerifyPersonSuccess(model);
        }
        /// <summary>
        /// 门禁认证对比失败接收
        /// </summary>
        /// <param name="model">参数</param>
        /// <returns></returns>
        [HttpPost, Route("verify/person/fail")]
        public async Task<bool> VerifyPersonFail(EventReciveDto<VerifyPersonDto> model)
        {
            return await _applicationService.VerifyPersonFailed(model);
        }
    }
}