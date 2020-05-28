using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using HttpUtil;

namespace General.Api.Application.Organize
{
    /// <summary>
    /// 组织
    /// </summary>
    public class OrganizeService : IOrganizeService
    {
        private readonly IHikHttpUtillib _hikHttp;

        public OrganizeService(IHikHttpUtillib hikHttp)
        {
            _hikHttp = hikHttp;
        }
        /// <summary>
        /// <see cref="IOrganizeService.GetAll"/>
        /// </summary>
        public async Task<ListBaseResponse<OrganizeInfoResponse>> GetAll()
        {
            ListBaseResponse<OrganizeInfoResponse> list = new ListBaseResponse<OrganizeInfoResponse>();
            list.List = new List<OrganizeInfoResponse>();
            int current = 1, size = 500;
            bool success = true;
            while (success)
            {
                var data = await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<OrganizeInfoResponse>>>(
                    "/api/resource/v1/org/orgList", new
                    {
                        pageNo = current,
                        pageSize = size
                    });
                if (data?.Data == null)
                {
                    return list;
                }
                if (data.Data?.List?.Count <= 0)
                {
                    success = false;
                }
                else
                {
                    list.List.AddRange(data.Data.List);
                    current++;
                }
            }

            return list;
        }
        /// <summary>
        /// <see cref="IOrganizeService.GetList(OrganizeRequest)"/>
        /// </summary>
        public async Task<ListBaseResponse<OrganizeInfoResponse>> GetList(OrganizeRequest request)
        {
            return (await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<OrganizeInfoResponse>>>(
                "/api/resource/v1/org/advance/orgList",
                new
                {
                    orgName = request.OrgName,
                    orgIndexCodes = request.OrgIndexCodes.FirstOrDefault(),
                    pageNo = request.PageNo,
                    pageSize = request.PageSize
                }))?.Data;
        }
    }
}