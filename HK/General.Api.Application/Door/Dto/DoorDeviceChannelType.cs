namespace General.Api.Application.Door.Dto
{
    /// <summary>
    /// DoorDeviceChannelType
    /// </summary>
    public class DoorDeviceChannelType
    {
        /// <summary>
        /// 监控点通道数
        /// </summary>
        public const string HannelCameraNum = "1";
        /// <summary>
        /// 对讲通道数
        /// </summary>
        public const string ChannelTalkNum = "1";
        /// <summary>
        /// 门禁通道
        /// </summary>
        public const string ChannelAccess = "door";
        /// <summary>
        /// 读卡器通道
        /// </summary>
        public const string HannelReader = "reader";
        /// <summary>
        /// 监控点通道
        /// </summary>
        public const string ChannelCamera = "camera";
        /// <summary>
        /// 对讲通道
        /// </summary>
        public const string ChannelTalk = "talk";
        /// <summary>
        /// 子通道-数字通道
        /// </summary>
        public const string ChannelChildrenDigType = "digital";
        /// <summary>
        /// 子通道-模拟通道
        /// </summary>
        public const string ChannelChildrenAnlType = "analog";

    }
}