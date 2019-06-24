using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace General.Api.Framework.Filters
{
    /// <summary>
    /// SwaggerIgnoreAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SwaggerIgnoreAttribute: Attribute, IFilterMetadata
    {
        /// <summary>
        /// construct
        /// </summary>
        /// <param name="ignore"></param>
        public SwaggerIgnoreAttribute(bool ignore)
        {
            IgnoreApi = ignore;
        }
        /// <summary>
        /// 
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IgnoreApi { get; set; }
    }
}