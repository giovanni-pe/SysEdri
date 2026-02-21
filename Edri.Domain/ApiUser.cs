using System;
using System.Linq;
using System.Security.Claims;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Edri.Domain;

public sealed class ApiUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private string? _name;
    private Guid _userId = Guid.Empty;

    public ApiUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        // 1. Si ya lo tenemos en caché local, retornarlo
        if (_userId != Guid.Empty)
        {
            return _userId;
        }

        // 2. Verificar si el contexto existe y si el usuario está autenticado.
        // Durante el Login, IsAuthenticated es false. Retornamos Empty para no romper el flujo.
        if (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated != true)
        {
            return Guid.Empty;
        }

        // 3. Intentar obtener el claim de forma segura
        var claim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        // ?? _httpContextAccessor.HttpContext.User.FindFirst("sub"); // Opcional: a veces viene como 'sub'

        if (Guid.TryParse(claim?.Value, out var userId))
        {
            _userId = userId;
            return userId;
        }

        // 4. Si no se puede parsear o no existe, retornar Empty (NUNCA lanzar excepción aquí)
        return Guid.Empty;
    }

    public UserRole GetUserRole()
    {
        if (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated != true)
        {
            // Retorna un valor por defecto si no está logueado (asegúrate que tu Enum tenga un valor default o maneja esto)
            return default;
        }

        var claim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role);

        if (Enum.TryParse(claim?.Value, out UserRole userRole))
        {
            return userRole;
        }

        // Retornar default en lugar de explotar
        return default;
    }

    public string Name
    {
        get
        {
            if (_name is not null) return _name;

            var identity = _httpContextAccessor.HttpContext?.User?.Identity;

            // Si es nulo o no autenticado
            if (identity is null || !identity.IsAuthenticated)
            {
                _name = string.Empty;
                return string.Empty;
            }

            if (!string.IsNullOrWhiteSpace(identity.Name))
            {
                _name = identity.Name;
                return identity.Name;
            }

            var claim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name);
            _name = claim?.Value ?? string.Empty;

            return _name;
        }
    }

    public string GetUserEmail()
    {
        if (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated != true)
        {
            return string.Empty;
        }

        var claim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email);
        return claim?.Value ?? string.Empty;
    }
}