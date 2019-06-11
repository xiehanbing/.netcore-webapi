using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using General.Api.Application.Hikvision;
using General.Api.Application.User.Dto;
using General.Api.Application.User.Request;
using General.Core;
using General.Core.Extension;
using General.Core.HttpClient.Extension;
using Microsoft.Extensions.Configuration;

namespace General.Api.Application.User
{
    /// <summary>
    /// 人员信息
    /// </summary>
    public class UserServce : IUserService
    {
        private readonly General.Api.Core.User.IUserDao _userDao;
        private readonly IMapper _mapper;
        private readonly string _doorControlApi;
        /// <summary>
        /// construct
        /// </summary>
        public UserServce(General.Api.Core.User.IUserDao userDao, IMapper mapper)
        {
            _userDao = userDao;
            _mapper = mapper;
            _doorControlApi =HikVisionContext.HikVisionBaseUrl;
            if (string.IsNullOrEmpty(_doorControlApi))
            {
                throw new MyException("doorControlApiUrl is null");
            }
        }

        public async Task<List<Dto.UserDto>> GetList()
        {
            //var url = "http://192.168.1.102:2013/general.api";

            //var geturl = url.AppendFormat("/api/Values/1")
            //    .SetParam("key", "123");
            //var data = url.AppendFormat("/api/Values/1").Get().GetJsonResult();
            return _mapper.Map<List<Dto.UserDto>>(await _userDao.GetList());
        }
        /// <summary>
        /// <see cref="IUserService.GetUserList(UserQuery)"/>
        /// </summary>
        public async Task<ListBaseResponse<UserResponse>> GetUserList(UserQuery query)
        {

            var data = await _doorControlApi.AppendFormat("/api/resource/v1/person/advance/personList")
                .PostAsync(new
                {
                    personIds = query.PersonIds.StringJoin(","),
                    query.PersonName,
                    query.Gender,
                    orgIndexCodes = query.OrgIndexCode.StringJoin(","),
                    certificateType = query.CertificateType,
                    query.CertificateNo,
                    query.PageNo,
                    query.PageSize
                }).ReciveJsonResultAsync<HikVisionResponse<ListBaseResponse<UserResponse>>>();
            return data?.Data;
        }
    }
}