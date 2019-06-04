using System;
using System.Collections.Generic;
using General.Core.Libs;

namespace General.Core.Extension
{
    public static class EnumExtendsion
    {
        /// <summary>
        /// 获取枚举项的描述
        /// </summary>
        /// <param name="typeOfEnum">枚举类型</param>
        /// <param name="value">枚举项/枚举值</param>
        /// <returns>描述</returns>
        public static string GetEnumDescription(this Type typeOfEnum, object value)
        {
            if (value is int)
            {
                return EnumHelper.GetEnumDescriptionByValue(typeOfEnum, Convert.ToInt16(value));
            }
            else if (value is string)
            {
                return EnumHelper.GetDescriptionByName(typeOfEnum, value.ToString());
            }
            return "";
        }

        public static string GetEnumDescriptionByValue(this object value, Type typeOfEnum)
        {
            if (value is int)
            {
                return EnumHelper.GetEnumDescriptionByValue(typeOfEnum, Convert.ToInt16(value));
            }
            else if (value is string)
            {
                return EnumHelper.GetDescriptionByName(typeOfEnum, value.ToString());
            }
            return "";

        }

        public static Dictionary<string, string> GetEnumDestAndValue(this Type typeOfEnum)
        {
            return EnumHelper.GetEnumDescriptionAndValue(typeOfEnum);
        }
    }
}