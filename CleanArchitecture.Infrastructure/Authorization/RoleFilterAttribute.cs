using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Authorization;
public sealed class RoleFilterAttribute : TypeFilterAttribute
{
    // Yetki kontrolünü [RoleFilter("...")] ile yapabilmek için
    public RoleFilterAttribute(string roler) : base(typeof(RoleAttribute))
    {
        Arguments = new object[] { roler };
    }
}
