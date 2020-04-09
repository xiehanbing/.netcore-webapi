using System.ComponentModel;

namespace General.Api.Application.Video.Request
{
    /// <summary>
    /// 控制模板
    /// </summary>
    public class ControlModel
    {
        /// <summary>
        /// 监控点编号
        /// </summary>
        public string  CameraIndexCode { get; set; }
        /// <summary>
        /// 0-开始
        ///1-停止
        /// </summary>
        public int Action { get; set; }
        /// <summary>
        /// 控制命令
        /// </summary>
        public CommandEnum Command { get; set; }
        /// <summary>
        /// 云台速度，取值范围为1-100，默认50
        /// </summary>
        public int? Speed { get; set; }
        /// <summary>
        /// 预置点编号
        /// </summary>
        public int?  PresetIndex{ get; set; }
    }
    /// <summary>
    /// 控制命令枚举
    /// </summary>
    public enum CommandEnum
    {
        [Description("左转")]
        LEFT,
        [Description("右转")]
        RIGHT,
        [Description("上转")]
        UP,
        [Description("下转")]
        DOWN,
        [Description("焦距变大")]
        ZOOM_IN,
        [Description("焦距变小")]
        ZOOM_OUT,
        [Description("左上")]
        LEFT_UP,
        [Description("左下")]
        LEFT_DOWN,
        [Description("右上")]
        RIGHT_UP,
        [Description("右下")]
        RIGHT_DOWN,
        [Description("焦点前移")]
        FOCUS_NEAR,
        [Description("焦点后移")]
        FOCUS_FAR,
        [Description("光圈扩大")]
        IRIS_ENLARGE,
        [Description("光圈缩小")]
        IRIS_REDUCE,
        [Description("到预置点")]
        GOTO_PRESET
    }
}