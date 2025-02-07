using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Gallery.Services
{
    public class EnumConvertService : IEnumConvertService
    {
        public IEnumerable<SelectListItem> ByteEnumValuesToSelectList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                       .Cast<Enum>()
                       .Select(e => new SelectListItem
                       {
                           Value = Convert.ToByte(e).ToString(),
                           Text = e.ToString()
                       })
                       .ToList();
        }

        public IEnumerable<SelectListItem> ByteEnumAttributesToSelectList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                       .Cast<Enum>()
                       .Select(e => new SelectListItem
                       {
                           Value = Convert.ToByte(e).ToString(),
                           Text = GetDisplayName(e)
                       })
                       .ToList();
        }

        private string GetDisplayName(Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()?
                            .Name ?? enumValue.ToString();
        }
    }
}
