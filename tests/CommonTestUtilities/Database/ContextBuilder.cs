using FCG.Catalog.Worker.Domain.Entities;
using FCG.Catalog.Worker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestUtilities.Database;

public static class ContextBuilder
{
    public static async Task<ApplicationDbContext> GenerateContext(
        List<Category> categories = null,
        List<Game> games = null,
        List<GameOrder> gamesOrders = null,
        List<Library> libraries = null
    )
    {
        categories ??= new List<Category>();
        games ??= new List<Game>();
        gamesOrders ??= new List<GameOrder>();
        libraries ??= new List<Library>();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);
        if(categories.Count > 0)
        {
            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }

        if (games.Count > 0)
        {
            await context.Games.AddRangeAsync(games);
            await context.SaveChangesAsync();
        }

        if (gamesOrders.Count > 0)
        {
            await context.GameOrders.AddRangeAsync(gamesOrders);
            await context.SaveChangesAsync();
        }

        if (libraries.Count > 0)
        {
            await context.Libraries.AddRangeAsync(libraries);
            await context.SaveChangesAsync();
        }
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
        return context;
    }
}
