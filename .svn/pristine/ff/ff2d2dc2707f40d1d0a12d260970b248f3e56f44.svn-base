using System;

namespace General.Api.Application.Parking.Dto.Device
{
    /// <summary>
    /// 道闸反控响应实体
    /// </summary>
    public class DeviceControlResponse
    {
        /// <summary>
        /// 权限组ID
        /// </summary>
        public string  PrivilegeGroupId { get; set; }
        /// <summary>
        /// 权限组名称
        /// </summary>
        public string PrivilegeGroupName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string  Remark { get; set; }
        /// <summary>
        /// 是否是默认权限组 0-是，1-否 	
        /// </summary>
        public int IsDefault { get; set; }
        /// <summary>
        /// 是否是默认权限组 描述
        /// </summary>
        public string IsDefaultDesc => IsDefault == 0 ? "是" : "否";
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}