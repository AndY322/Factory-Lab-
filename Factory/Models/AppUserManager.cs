using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;

namespace Factory.Models
{
    public class AppUserManager : UserManager<User>
    {
        public AppUserManager(IUserStore<User> store) : base(store)
        {
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options,
                                           IOwinContext context)
        {
            FactoryContext db = context.Get<FactoryContext>();
            AppUserManager manager = new AppUserManager(new UserStore<User>(db));
            return manager;
        }
    }
}