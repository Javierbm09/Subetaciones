using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Subestaciones.Models.CustomModelBinder
{

    public class DatetimeModelBinder : DefaultModelBinder
    {
        public override object BindModel(System.Web.Mvc.ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(DateTime))
            {
                HttpRequestBase request = controllerContext.HttpContext.Request;
                var modelName = bindingContext.ModelName;
                var fecha = Convert.ToDateTime(request.Form[modelName].ToString(), new CultureInfo("es-es"));
                return fecha;
            }
            else
            {
                return base.BindModel(controllerContext, bindingContext);
            }
        }
    }
}