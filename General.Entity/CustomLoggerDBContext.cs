using Microsoft.EntityFrameworkCore;

namespace General.EntityFrameworkCore
{
    public class CustomLoggerDBContext:DbContext
    {
        public static string ConnectionString="";
    }
}