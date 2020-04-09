using System;
using System.ComponentModel.DataAnnotations;

namespace General.Api.Application.Face
{
    /// <summary>
    /// 以图搜图
    /// </summary>
    public class CaptureSearchRequest : PageRequest
    {
        /// <summary>
        /// 用于搜图的图片的二进制数据经过Base64编码后的字符串，该参数与facePicUrl至少有一个存在，都存在时优先使用facePicBinaryData
        /// </summary>
        public string FacePicBinaryData { get; set; }
        /// <summary>
        /// 	用于搜图的图片的URL，要求URL可以通过GET方式直接下载，该参数与facePicBinaryData至少有一个存在，都存在时优先使用facePicBinaryData
        /// </summary>
        public string FacePicUrl { get; set; }
        /// <summary>
        /// 抓拍到人脸的抓拍机的唯一标识，若为空，则搜索全部的抓拍机
        /// </summary>
        public string[] CameraIndexCodes { get; set; }
        /// <summary>
        /// 指定每个监控点搜索张数的最大值，最大值为100，为空时，等价于100
        /// </summary>
        public int? SearchNum { get; set; }
        /// <summary>
        /// 指定搜图的开始时间，要求遵守ISO8601标准
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 指定搜图的结束时间，要求遵守ISO8601标准，且必须在startTime之后
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 指定搜图的最小相似度，最小为1，最大为100
        /// </summary>
        [Range(1, 100,ErrorMessage = "最小相似度必须在1-100之间")]
        public int MinSimilarity { get; set; }
        /// <summary>
        /// 指定搜图的最大相似度，最小为1，最大为100
        /// </summary>
        public int? MaxSimilarity { get; set; }
        /// <summary>
        /// 年龄，不小于 0
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// 指定查询的年龄段
        /// UNKNOWN	未知	
        ///CHILD 少年
        ///YOUNG 青年
        ///MIDDLE 中年
        /// OLD 老年
        ///INFANT 婴幼儿
        ///KID 儿童
        ///TEENAGER 青少年
        ///PRIME 壮年
        ///MIDDLEAGED 中老年
        /// </summary>
        public string AgeGroup { get; set; }
        /// <summary>
        /// 性别，male-男性，female-女性，UNKNOWN-未知
        /// </summary>
        public string  Sex { get; set; }
        /// <summary>
        /// 是否戴眼镜，YES-是，
        ///NO-否，UNKNOWN-未知
        /// </summary>
        public string WithGlass { get; set; }
        /// <summary>
        /// 是否微笑，YES-是，
        /// NO-否，UNKNOWN-未知
        /// </summary>
        public string  Smile { get; set; }
        /// <summary>
        ///是否是少数民族，YES-是，
        ///NO-否，UNKNOWN-未知
        /// </summary>
        public string  IsEthnic { get; set; }
    }
}