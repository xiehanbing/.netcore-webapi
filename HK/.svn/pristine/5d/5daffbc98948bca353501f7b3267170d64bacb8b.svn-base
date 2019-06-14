using General.Log.PatternConverter;
using log4net.Layout;

namespace General.Log
{
    /// <summary>
    /// 自定义log4net 布局
    /// </summary>
    internal class CustomLayout : PatternLayout
    {
        public CustomLayout()
        {
            #region 内部自定义
            //AddConverter("ClientIp", typeof(ClientIpPatternConverter));
            #endregion

            #region 开发人员自定义

            AddConverter("Object", typeof(ObjectPatternConverter));

            #endregion

        }
    }
}