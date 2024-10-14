using IdentityServer8.EntityFramework.DbContexts;
using IdentityServer8.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using OAuth2Server.Configuration;

namespace OAuth2Server.Helpers;

public class DatabaseHelpers
{
    public static void InitializeDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
        serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

        var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        context.Database.Migrate();

        var allEntities = context.Clients.ToList();
        context.Clients.RemoveRange(allEntities);
        context.SaveChanges();

        if (!context.Clients.Any())
        {
            foreach (var client in InMemoryConfig.GetClients())
            {
                context.Clients.Add(client.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.IdentityResources.Any())
        {
            foreach (var resource in InMemoryConfig.GetIdentityResources())
            {
                context.IdentityResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.ApiResources.Any())
        {
            foreach (var resource in InMemoryConfig.GetApiResources())
            {
                context.ApiResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.ApiScopes.Any())
        {
            foreach (var resource in InMemoryConfig.GetApiScopes())
            {
                context.ApiScopes.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }
    }
}
