using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace General.Core.ModelBinder
{
    /// <summary>
    /// 模型 加解密 provider
    /// </summary>
    public class EncryEntityBinderProvider: IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            //var value=context.BindingInfo.
            //if(context.Metadata.==typeof())
            //return new BinderTypeModelBinder(typeof(EncryEntityBinder)); ;
            return null;
        }
    }
}