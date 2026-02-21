using System;
using Edri.Domain.Entities;

namespace Edri.Domain;

public static class CacheKeyGenerator
{
    public static string GetEntityCacheKey<TEntity>(TEntity entity) where TEntity : Entity
    {
        return $"{typeof(TEntity)}-{entity.Id}";
    }

    public static string GetEntityCacheKey<TEntity>(Guid id) where TEntity : Entity
    {
        return $"{typeof(TEntity)}-{id}";
    }
}