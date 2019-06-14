using System.Threading.Tasks;

namespace General.Api.Application.EventRevice
{
    public interface IDoorApplicationService
    {
        Task<bool> VerifyPerson();
    }
}