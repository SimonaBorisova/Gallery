using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gallery.Services
{
    public interface IEnumConvertService
    {
        IEnumerable<SelectListItem> ByteEnumValuesToSelectList<T>() where T : Enum;

        IEnumerable<SelectListItem> ByteEnumAttributesToSelectList<T>() where T : Enum;
    }
}
