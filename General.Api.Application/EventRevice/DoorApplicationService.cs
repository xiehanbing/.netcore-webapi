using System.Threading.Tasks;
using General.Api.Application.EventRevice.Dto;
using General.Api.Application.EventRevice.Dto.VerifyPerson;

namespace General.Api.Application.EventRevice
{
    /// <summary>
    /// 事件订阅接收接口
    /// </summary>
    public class DoorApplicationService : IDoorApplicationService
    {
        public  Task<bool> VerifyPersonFailed(EventReciveDto<VerifyPersonDto> model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> VerifyPersonSuccess(EventReciveDto<VerifyPersonDto> model)
        {
            throw new System.NotImplementedException();
        }
    }
}