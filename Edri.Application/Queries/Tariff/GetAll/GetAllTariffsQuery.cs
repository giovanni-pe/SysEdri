using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Tariffs;
using MediatR;

namespace Edri.Application.Queries.Tariffs.GetAll;

public sealed record GetAllTariffsQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<TariffViewModel>>;