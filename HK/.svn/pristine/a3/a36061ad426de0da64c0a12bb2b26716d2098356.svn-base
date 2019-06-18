using AutoMapper;

namespace General.Api.Application
{
    /// <summary>
    /// automapper 映射配置
    /// </summary>
    public class AutoMapperProfileConfiguration : Profile
    {
        //添加你的实体映射关系.
        /// <summary>
        /// 添加你的实体映射关系
        /// </summary>
        public AutoMapperProfileConfiguration()
        {

            CreateMap<User.Dto.UserDto, General.EntityFrameworkCore.User.User>()
                .ForMember(dest => dest.No, opts => opts.MapFrom(src => src.No))
                .ReverseMap();
            //CreateMap<User.Dto.UserDto,General.EntityFrameworkCore.User.User>(cfg=>{})
            ////GoodsEntity转GoodsDto.
            //CreateMap<GoodsEntity, GoodsDto>() //映射发生之前
            //             .BeforeMap((source, dto) =>
            //             {
            //                 //可以较为精确的控制输出数据格式
            //                 dto.CreateTime = Convert.ToDateTime(source.CreateTime).ToString("yyyy-MM-dd");
            //             }) //映射发生之后
            //             .AfterMap((source, dto) =>
            //              {
            //                  //code ...
            //              });
            ////GoodsDto转GoodsEntity
            //CreateMap<GoodsDto, GoodsEntity>();

        }

    }
}