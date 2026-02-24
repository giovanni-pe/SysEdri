using System;
using System.Threading.Tasks;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Captures;

namespace Edri.Application.Interfaces;

public interface ICaptureService
{
    Task<Guid> CreateCaptureAsync(CreateCaptureViewModel capture);
    Task UpdateCaptureAsync(UpdateCaptureViewModel capture);
    Task DeleteCaptureAsync(Guid captureId);
    Task<CaptureViewModel?> GetCaptureByIdAsync(Guid captureId);
    Task<PagedResult<CaptureViewModel>> GetAllCapturesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}