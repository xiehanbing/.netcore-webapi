using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.Api.Controllers
{
    /// <summary>
    /// 事件接收服务
    /// </summary>
    [Route("api/[controller]"), Authorize("General")]
    [ApiController]
    public class EventReciveController : ControllerBase
    {
    }
}