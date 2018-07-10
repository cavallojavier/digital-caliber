using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace digital.caliber.Helpers
{
    public static class UrlHelperExtensions
    {
        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string email, string code, string scheme)
        {
            return urlHelper.Action(
                action: "reset-password",
                controller: "account",
                values: new { email, code },
                protocol: scheme);
        }
    }
}
