namespace General.Api.Application.Door.Dto
{
    public class DoorDeviceChannelType
    {
        /// <summary>
        /// 监控点通道数
        /// </summary>
        public const string HANNEL_CAMERA_NUM = "1";
        /// <summary>
        /// 对讲通道数
        /// </summary>
        public const string CHANNEL_TALK_NUM = "1";
        /// <summary>
        /// 门禁通道
        /// </summary>
        public const string CHANNEL_ACCESS = "door";
        /// <summary>
        /// 读卡器通道
        /// </summary>
        public const string HANNEL_READER = "reader";
        /// <summary>
        /// 监控点通道
        /// </summary>
        public const string CHANNEL_CAMERA = "camera";
        /// <summary>
        /// 对讲通道
        /// </summary>
        public const string CHANNEL_TALK = "talk";
        /// <summary>
        /// 子通道-数字通道
        /// </summary>
        public const string CHANNEL_CHILDREN_DIG_TYPE = "digital";
        /// <summary>
        /// 子通道-模拟通道
        /// </summary>
        public const string CHANNEL_CHILDREN_ANL_TYPE = "analog";

    }
}