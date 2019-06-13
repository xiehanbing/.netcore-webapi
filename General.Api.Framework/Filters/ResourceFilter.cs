﻿using System;
using System.Threading.Tasks;
using General.Core.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace General.Api.Framework.Filters
{
    public class ResourceFilter: IAsyncResourceFilter //IResourceFilter
    {
        //public void OnResourceExecuting(ResourceExecutingContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void OnResourceExecuted(ResourceExecutedContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            //这是可以实现进行过滤的
            //var requestPath = context.HttpContext.Request.Path.Value;
            //Console.WriteLine("ResourceFilter->request->url:" + requestPath);
            //var request = context.HttpContext.Request;
            //request.Body.WriteAsync()

            //if (requestPath.Contains("Values"))
            //{
            //    context.Result = new ObjectResult(new ApiResult(false, "不被允许"));
            //}
            //else
            //{
            //    await next();
            //}
            await next();



            //await 
        }
    }
}