using System.Linq;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace General.Api.Framework.Filters
{
    /// <summary>
    /// SwaggerIgnoreFilter
    /// </summary>
    public class SwaggerIgnoreFilter : IDocumentFilter
    {
        /// <summary>
        /// Apply
        /// </summary>
        /// <param name="swaggerDoc">swaggerDoc</param>
        /// <param name="context">context</param>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            var ignoreApis =
            context.ApiDescriptions.Where(o =>
            {
                if (o.TryGetMethodInfo(out MethodInfo methodInfo))
                {
                    if (methodInfo == null) return false;
                    if (methodInfo.CustomAttributes.Any(any => any.AttributeType == typeof(SwaggerIgnoreAttribute)))
                    {
                        var attribute = methodInfo.GetCustomAttribute<SwaggerIgnoreAttribute>();
                        return attribute.IgnoreApi;
                    }
                    else if (methodInfo.DeclaringType.CustomAttributes.Any(any => any.AttributeType == typeof(SwaggerIgnoreAttribute)))
                    {
                        var attribute = methodInfo.DeclaringType.GetCustomAttribute<SwaggerIgnoreAttribute>();
                        return attribute.IgnoreApi;
                    }
                    return false;
                }
                return false;
            }).ToList();
            if (ignoreApis.Count > 0)
                foreach (var ignoreApi in ignoreApis)
                {
                    swaggerDoc.Paths.Remove("/" + ignoreApi.RelativePath);
                }
        }
    }
}