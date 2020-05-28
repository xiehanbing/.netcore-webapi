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
using HttpUtil;
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
        private readonly IHikHttpUtillib _hikHttp;
        /// <summary>
        /// construct
        /// </summary>
        public UserServce(General.Api.Core.User.IUserDao userDao, IMapper mapper, IHikHttpUtillib hikHttp)
        {
            _userDao = userDao;
            _mapper = mapper;
            _hikHttp = hikHttp;
            _doorControlApi = HikVisionContext.HikVisionBaseUrl;
            if (string.IsNullOrEmpty(_doorControlApi))
            {
                throw new MyException("doorControlApiUrl is null");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Dto.UserDto>> GetList()
        {
            //var url = "http://localhost:5000";

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
                .SetHiKSecreity()
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

        /// <summary>
        /// 获取所有人员列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserResponse>> GetAllUser()
        {
            List<UserResponse> list = new List<UserResponse>();
            int current = 1;
            int size = 500;
            bool success = true;
            while (success)
            {
                var data = await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<UserResponse>>>(
                    "/api/resource/v2/person/personList",
                    new
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
                    list.AddRange(data.Data.List);
                    current++;
                }
            }

            return list;
        }
        /// <summary>
        /// <see cref="IUserService.GetDetail(string)"/>
        /// </summary>
        public async Task<UserResponse> GetDetail(string personId)
        {
            var data = await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<UserResponse>>>(
                "/api/resource/v1/person/condition/personInfo", new
                {
                    paramName = "personId",
                    paramValue = new string[] { personId }
                });
            return data?.Data?.List?.FirstOrDefault();
        }

        /// <summary>
        /// <see cref="IUserService.GetDetailV2(List{string})"/>
        /// </summary>
        public async Task<List<UserResponse>> GetDetailV2(List<string> personId)
        {
            List<UserResponse> list = new List<UserResponse>();
            bool success = true;
            int current = 1;
            int size = 1000;
            while (success)
            {
                List<string> subList;
                if (personId.Count > 1000)
                {
                    subList = personId.Skip((current - 1) * size).Take(size).ToList();
                }
                else
                {
                    success = false;
                    subList = personId;
                }
                if (subList.Count > 0)
                {
                    var data = await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<UserResponse>>>(
                        "/api/resource/v2/person/advance/personList", new
                        {
                            personIds = string.Join(",", subList),
                            pageNo = current,
                            pageSize = size
                        });
                    if (data?.Data?.List?.Count > 0)
                    {
                        list.AddRange(data.Data.List);
                    }
                }
            }

            return list;
        }


        /// <summary>
        /// <see cref="IUserService.GetDetail(List{string})"/>
        /// </summary>
        public async Task<List<UserResponse>> GetDetail(List<string> personId)
        {
            List<UserResponse> list = new List<UserResponse>();
            bool success = true;
            int current = 1;
            int size = 1000;
            while (success)
            {
                List<string> subList;
                if (personId.Count > 1000)
                {
                    subList = personId.Skip((current - 1) * size).Take(size).ToList();
                }
                else
                {
                    success = false;
                    subList = personId;
                }
                if (subList.Count > 0)
                {
                    var data = await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<UserResponse>>>(
                        "/api/resource/v1/person/condition/personInfo", new
                        {
                            paramName = "personId",
                            paramValue = subList
                        });
                    if (data?.Data?.List?.Count > 0)
                    {
                        list.AddRange(data.Data.List);
                    }
                }
            }

            return list;
        }
    }
}