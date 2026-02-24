using System;
using System.Threading.Tasks;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Readings;

namespace Edri.Application.Interfaces;

public interface IReadingService
{
    Task<Guid> CreateReadingAsync(CreateReadingViewModel reading);
    Task UpdateReadingAsync(UpdateReadingViewModel reading);
    Task DeleteReadingAsync(Guid readingId);
    Task<ReadingViewModel?> GetReadingByIdAsync(Guid readingId);
    Task<PagedResult<ReadingViewModel>> GetAllReadingsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}