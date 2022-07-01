using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Electronic_Shop.Common
{
    public class BaseController : ControllerBase
    {

        protected List<string> GetErrors()
        {
            var result = new List<string>();
            var errors = ModelState.Values.Select(e => e.Errors).ToList();
            foreach (var error in errors)
                result.AddRange(error.Select(e => e.ErrorMessage));
            return result;
        }


    }
}
