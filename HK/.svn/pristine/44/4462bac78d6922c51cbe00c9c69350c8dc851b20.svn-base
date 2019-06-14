using System.IO;
using System.Reflection;
using log4net.Core;
using log4net.Layout.Pattern;

namespace General.Log.PatternConverter
{
    /// <summary>
    /// 自定义布局
    /// </summary>
    internal class ObjectPatternConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            if (Option != null)
            {
                WriteObject(writer, loggingEvent.Repository, LookupProperty(Option, loggingEvent));
            }
            else
            {
                WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
            }
        }
        /// <summary>
        /// 通过反射获取传入的日志对象的某个属性的值
        /// </summary>
        /// <param name="property"></param>
        /// <param name="loggingEvent"></param>
        /// <returns></returns>
        private object LookupProperty(string property, LoggingEvent loggingEvent)
        {
            object propertyValue = string.Empty;
            PropertyInfo pePropertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);
            if (pePropertyInfo != null)
            {
                propertyValue = pePropertyInfo.GetValue(loggingEvent.MessageObject, null);

            }

            return propertyValue;
        }
    }
}