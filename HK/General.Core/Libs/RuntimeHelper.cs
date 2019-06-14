using System.Reflection;
using System.Runtime.Loader;

namespace General.Core.Libs
{
    /// <summary>
    /// runtime 帮助类
    /// </summary>
    public class RuntimeHelper
    {
        /// <summary>
        /// 通过程序集名称获取程序集
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns></returns>
        public static Assembly GetAssemblyByName(string assemblyName)
        {
            return AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
        }
    }
}