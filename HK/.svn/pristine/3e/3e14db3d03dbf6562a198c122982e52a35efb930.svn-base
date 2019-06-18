using System.Threading.Tasks;
using General.Api.Application.EventRevice.Dto;
using General.Api.Application.EventRevice.Dto.VerifyPerson;

namespace General.Api.Application.EventRevice
{
    /// <summary>
    /// 事件订阅接收接口
    /// </summary>
    public interface IDoorApplicationService
    {
        /// <summary>
        /// 门禁认证对比成功 接收
        /// </summary>
        /// <param name="model">参数</param>
        /// <returns></returns>
        Task<bool> VerifyPersonSuccess(EventReciveDto<VerifyPersonDto> model);
        /// <summary>
        ///  门禁认证对比失败 接收
        /// </summary>
        /// <param name="model">参数</param>
        /// <returns></returns>
        Task<bool> VerifyPersonFailed(EventReciveDto<VerifyPersonDto> model);
    }
}