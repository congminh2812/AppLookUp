using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppLookUp.Models.ViewModels
{
    public class InformationVM
    {
        public Information Information { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ListTypeInfo { get; set; }
    }
}
