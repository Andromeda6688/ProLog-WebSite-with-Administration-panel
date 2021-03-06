﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using MvcApplication1.Models;
using System.Web.Security;
using System.Linq;

namespace MvcApplication1.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UsersContext>(null);

                try
                {
                    using (var context = new UsersContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection("WebSiteDBConnectionString", "Users", "Id", "Email", autoCreateTables: true);

                    //we create SuperAdmin for sake of occasional delete of all other users
                    var roles = (SimpleRoleProvider)Roles.Provider;
                    var membership = (SimpleMembershipProvider)Membership.Provider;

                    if (!roles.RoleExists("Admin"))
                    {
                        roles.CreateRole("Admin");
                    }
                    if (membership.GetUser("SuperAdmin", false) == null)
                    {
                        membership.CreateUserAndAccount("SuperAdmin", "SecretSuperAdminPassword");
                    }
                    if (!roles.GetRolesForUser("SuperAdmin").Contains("Admin"))
                    {
                        roles.AddUsersToRoles(new[] { "SuperAdmin" }, new[] { "Admin" });
                    }
                    if (!roles.GetRolesForUser("SuperAdmin").Contains("Author"))
                    {
                        roles.AddUsersToRoles(new[] { "SuperAdmin" }, new[] { "Author" });
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}
