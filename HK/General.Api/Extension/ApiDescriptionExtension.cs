using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace General.Api.Extension
{
    /// <summary>
    /// ApiDescriptionExtension 域接口描述 扩展类
    /// </summary>
    public static class ApiDescriptionExtension
    {
        /// <summary>
        /// 获取区域名称
        /// </summary>
        /// <param name="description">域接口描述</param>
        /// <returns></returns>
        public static List<string> GetAreaName(this ApiDescription description)
        {
            string areaName = description.ActionDescriptor.RouteValues["area"];
            string controlName = description.ActionDescriptor.RouteValues["controller"];
            List<string> areaList = new List<string> {controlName};
            if (!string.IsNullOrEmpty(areaName))
            {
                description.RelativePath = $"{areaName}/{controlName}/{description.RelativePath}";
            }
            //description.RelativePath = $"{controlName}/{description.RelativePath}";
            return areaList;
        }
    }
}