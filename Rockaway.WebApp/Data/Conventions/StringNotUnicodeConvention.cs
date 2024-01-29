using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Rockaway.WebApp.Data.Conventions;

public sealed class StringNotUnicodeConvention : IModelFinalizingConvention
{
    public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
    {
        var entities = modelBuilder.Metadata.GetEntityTypes();
        var properties = entities.SelectMany(entityType => entityType.GetDeclaredProperties().Where(property => property.ClrType == typeof(string)));
        foreach (var property in properties)
            property.Builder.IsUnicode(false);
    }
}