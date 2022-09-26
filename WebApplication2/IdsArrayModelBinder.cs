using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;

namespace WebApplication2
{
    public class IdsArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException("bindingContext");
            }

            string modelName = bindingContext.ModelName;
            ValueProviderResult value = bindingContext.ValueProvider.GetValue(modelName);
            if (value == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            Type elementType = bindingContext.ModelType.GetElementType();
            try
            {
                if (elementType != null)
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(elementType);
                    object[] array = Array.ConvertAll(value.ToString()!.Split(new char[1] { ',' }, 
                        StringSplitOptions.RemoveEmptyEntries),
                        (string _) => converter.ConvertFromString((_ != null) ? _.Trim() : _)).Distinct().ToArray();

                    Array array2 = Array.CreateInstance(elementType, array.Length);
                    array.CopyTo(array2, 0);
                    bindingContext.Result = ModelBindingResult.Success(array2);
                }
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError(modelName, ex.Message);
            }

            return Task.CompletedTask;
        }


        public class IdsBinderAttribute : ModelBinderAttribute
        {
            public IdsBinderAttribute()
                : base(typeof(IdsArrayModelBinder))
            {
            }
        }
    }
}
