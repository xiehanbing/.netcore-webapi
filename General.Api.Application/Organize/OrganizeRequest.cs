using System.Collections.Generic;

namespace General.Api.Application.Organize
{
    /// <summary>
    /// 查询组织列表请求参数
    /// </summary>
    public class OrganizeRequest:PageRequest
    {
        /// <summary>
        /// 组织名称，如默认部门
        /// </summary>
        public string  OrgName { get; set; }
        /// <summary>
        /// 组织唯一标识码集合
        /// </summary>
        public List<string> OrgIndexCodes { get; set; }
    }
}