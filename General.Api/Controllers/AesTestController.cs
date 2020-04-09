using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Framework.Filters;
using General.Core.Encrypt;
using General.Core.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.Api.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]"), SwaggerIgnore(true)]
    [ApiController]
    public class AesTestController : ControllerBase
    {
        [Route("getAes/encry"),HttpPost]
        public async Task<string> AesEncry(AesModel model)
        {
            var stringStr = model.GetSerializeObject().AesEncry();
            return stringStr;
        }

        [Route("dncry"), HttpPost]
        public async Task<AesModel> AesDncry(AesModel model)
        {
            return model;
            //var stringStr = model.GetSerializeObject();
            //return stringStr;
        }
        [Route("dncry/v2"), HttpPost]
        public async Task<string> AesDncryV2(AesModel model)
        {

            var stringStr = model.GetSerializeObject();
            return stringStr;
        }
    }

    public class AesModel
    {
        public string  Name { get; set; }
        public string  Value { get; set; }
    }
}