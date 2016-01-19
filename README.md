# Microsoft.AspNet.Identity.EntityFramework6
ASP.NET Identity 3.0 provider for Entity Framework 6

For a more detailed explanation of the motivation, see my blog post: http://sharepoint.bg/radi/post/Using-ASPNET-Identity-3-with-Entity-Framework-6.aspx

## Getting Started: Sample ASP.NET 5 MVC 6 solution project with ASP.NET Identity 3 and Entity Framework 6

You will find a complete sample with everything set up in the repository: https://github.com/OneBitSoftware/Microsoft.AspNet.Identity.EntityFramework6/tree/master/samples/IdentitySamples.MVC6.EF6

If you need to add this to your own MVC project, just reference Microsoft.AspNet.Identity.EntityFramework6 and place the following in the ConfigureServices method of your Startup.cs file:

```cs
//Inject ApplicationDbContext in place of IdentityDbContext and use connection string
services.AddScoped<IdentityDbContext<ApplicationUser>>(context =>
    new ApplicationDbContext(Configuration["Data:DefaultConnection:ConnectionString"]));

//Configure Identity middleware with ApplicationUser and the EF6 IdentityDbContext
services.AddIdentity<ApplicationUser, IdentityRole>(config =>
{
    config.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<IdentityDbContext<ApplicationUser>>()
.AddDefaultTokenProviders();
```

You can use ApplicationUser and IdentityDbContext to modify the model further.
This is currently not released as a Nuget package, although if there is interest we could do that on MyGet.

## Support

Please note that I've used the Microsoft.AspNet.* in the hope that Microsoft might want to include this in the Identity repository. 

At this stage Microsoft do not support this library and it is not an official Microsoft library that is part of the official ASP.NET Identity 3 for which we get great support.
