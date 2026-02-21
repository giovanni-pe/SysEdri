using System;
using System.Threading.Tasks;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Tariffs;

namespace Edri.Application.Interfaces;

public interface ITariffService
{
    Task<Guid> CreateTariffAsync(CreateTariffViewModel tariff);
    Task UpdateTariffAsync(UpdateTariffViewModel tariff);
    Task DeleteTariffAsync(Guid tariffId);
    Task<TariffViewModel?> GetTariffByIdAsync(Guid tariffId);
    Task<PagedResult<TariffViewModel>> GetAllTariffsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}