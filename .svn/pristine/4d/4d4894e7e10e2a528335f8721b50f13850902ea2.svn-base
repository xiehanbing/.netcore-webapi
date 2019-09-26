using System.Runtime.CompilerServices;

namespace General.Core
{
    /// <summary>
    /// 引擎上下文
    /// </summary>
    public class EngineContext
    {
        private static IEngine _engine;
        /// <summary>
        /// 引擎单例 (线程安全)
        /// </summary>
        /// <param name="engine"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(IEngine engine)
        {
            return _engine ?? (_engine = engine);
        }
        /// <summary>
        /// 当前引擎单例
        /// </summary>
        public static IEngine CurrentEngin => _engine;
    }
}