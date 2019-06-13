using System.Collections.Generic;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Spcms.Dto;

namespace General.Api.Application.Spcms
{
    /// <summary>
    /// 入侵报警服务
    /// </summary>
    public interface ISpcmsService
    {
        /// <summary>
        /// 获取子系统状态
        /// </summary>
        /// <param name="subSysIndexCodes">子系统编码列表</param>
        /// <returns></returns>
        Task<ListBaseResponse<SubSysStatusResponse>> GetSubsystemStatus(List<string> subSysIndexCodes);
        /// <summary>
        /// 获取防区状态
        /// </summary>
        /// <param name="defenceIndexCodes">防区编码列表</param>
        /// <returns></returns>
        Task<ListBaseResponse<DefenceStatusResponse>> GetDefenceStatus(List<string> defenceIndexCodes);
    }
}