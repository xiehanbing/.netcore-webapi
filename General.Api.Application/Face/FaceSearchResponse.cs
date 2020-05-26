namespace General.Api.Application.Face
{
    /// <summary>
    /// 人脸抓拍查询响应
    /// </summary>
    public class FaceSearchResponse
    {
        /// <summary>
        /// 事件时间,遵守ISO8601标准，yyyy-MM-ddTHH:mm:ss.SSS+当前时区，例如北京时间：2018-07-26T15:00:00.000+08:00
        /// </summary>
        public string EventTime { get; set; }
        /// <summary>
        /// 性别，male-男，female-女
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 年龄段
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
        public string Age { get; set; }
        /// <summary>
        /// 指定查询是否戴眼镜， yes 是， no 否
        /// </summary>
        public string Glass { get; set; }


        /// <summary>
        /// 监控点唯一标识
        /// </summary>
        public string CameraIndexCode { get; set; }
        /// <summary>
        /// 监控点名称
        /// </summary>
        public string CameraName { get; set; }
        /// <summary>
        /// 抓拍背景图
        /// </summary>
        public string BkgUrl { get; set; }
        /// <summary>
        /// 抓拍小图
        /// </summary>
        public string SnapUrl { get; set; }

        #region 额外参数

        /// <summary>
        /// 性别，male-男，female-女
        /// </summary>
        public string Sex => Gender;
        /// <summary>
        /// 抓拍到的人脸年龄段
        /// </summary>
        public string AgeGroup => Age;

        /// <summary>
        /// 抓拍到的人脸是否戴眼镜
        /// </summary>
        public string WithGlass => Glass;
        /// <summary>
        /// 地点区域
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 抓拍到的人脸和上传人脸的相似度
        /// </summary>
        public string Similarity { get; set; }

        /// <summary>
        /// 抓拍到的人脸的人脸图片
        /// </summary>
        public string FacePicUrl => SnapUrl;

        /// <summary>
        /// 抓拍背景图
        /// </summary>
        public string BkgPicUrl => BkgUrl;

        #endregion
    }
}