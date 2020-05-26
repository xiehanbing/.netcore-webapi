using System;

namespace General.Api.Application.Face
{
    /// <summary>
    /// 人脸抓拍查询
    /// </summary>
    public class FaceSearchRequest:PageRequest
    {
        /// <summary>
        /// 开始时间
        /// </summary>
       
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 监控点唯一标志集合，若不指定，则查询所有的监控点
        /// </summary>
        public string[]  CameraIndexCodes { get; set; }
        /// <summary>
        /// 指定查询的年龄段
        /// UNKNOWN	未知	
        ///CHILD 少年
        ///YOUNG 青年
        ///MIDDLE 中年
        ///OLD 老年
        ///INFANT 婴幼儿
        ///KID 儿童
        ///TEENAGER 青少年
        ///PRIME 壮年
        ///MIDDLEAGED 中老年
        /// </summary>
        public string AgeGroup { get; set; }
        /// <summary>
        /// 性别 male-男，female-女
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 指定查询是否戴眼镜， yes是， no否
        /// </summary>
        public string WithGlass { get; set; }
    }
}