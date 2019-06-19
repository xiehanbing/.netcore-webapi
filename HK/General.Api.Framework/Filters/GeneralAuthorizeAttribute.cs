using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace General.Api.Framework.Filters
{
    /// <summary>
    /// GeneralAuthorizeAttribute 自定义
    /// </summary>
    public class GeneralAuthorizeAttribute:Attribute, IFilterMetadata
    {
        
    }
}