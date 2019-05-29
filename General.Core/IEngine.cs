namespace General.Core
{
    /// <summary>
    /// 系统引擎接口
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// 构建一个实例
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns>接口实例</returns>
        T Resolve<T>() where T : class;
    }
}