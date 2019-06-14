using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace General.Core.Libs
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 获取枚举项的描述
        /// </summary>
        /// <param name="typeOfEnum">枚举类型</param>
        /// <param name="value">枚举项/枚举值</param>
        /// <returns>描述</returns>
        public static string GetEnumDescription(Type typeOfEnum, object value)
        {
            if (value is int)
            {
                return GetEnumDescriptionByValue(typeOfEnum, Convert.ToInt16(value));
            }

            if (value is string)
            {
                return GetDescriptionByName(typeOfEnum, value.ToString());
            }
            return "";
        }
        /// <summary>
        /// 获取枚举值的描述
        /// </summary>
        /// <param name="typeOfEnum">枚举类型</param>
        /// <param name="value">枚举值</param>
        /// <returns>描述</returns>
        public static string GetEnumDescriptionByValue(Type typeOfEnum, int value)
        {
            string result = string.Empty;
            string errorMsg = "";
            try
            {
                FieldInfo f = typeOfEnum.GetField(System.Enum.GetName(typeOfEnum, value));
                if (f != null)
                {
                    DescriptionAttribute attributeOfVal = (DescriptionAttribute)f.GetCustomAttributes(typeof(DescriptionAttribute), false).First();
                    if (attributeOfVal != null)
                    {
                        result = attributeOfVal.Description;
                    }
                }
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
            }
            return result;
        }
        /// <summary>
        /// 根据名称获取描述
        /// </summary>
        /// <param name="typeOfEnum">枚举类型</param>
        /// <param name="value">名称</param>
        /// <returns></returns>
        public static string GetDescriptionByName(Type typeOfEnum, string value)
        {
            string result = string.Empty;
            string errorMsg = "";
            try
            {
                if (value != null)
                {
                    FieldInfo f = typeOfEnum.GetField(value);
                    if (f != null)
                    {
                        DescriptionAttribute attributeOfVal = (DescriptionAttribute)f.GetCustomAttributes(typeof(DescriptionAttribute), false).First();
                        if (attributeOfVal != null)
                        {
                            result = attributeOfVal.Description;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 获取描述和key
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetEnumDescriptionAndKey(Type enumType)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            FieldInfo[] members = enumType.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo member in members)
            {
                list.Add(((DescriptionAttribute)member.GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description, member.Name);
            }
            return list;
        }
        /// <summary>
        /// 获取描述和value
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetEnumDescriptionAndValue(Type enumType)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            FieldInfo[] members = enumType.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo member in members)
            {
                list.Add(((DescriptionAttribute)member.GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description, System.Enum.Parse(enumType, member.Name).GetHashCode().ToString());
            }
            return list;
        }
    }
}