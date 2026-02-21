using System;
using Edri.Application.ViewModels.Branches;
using MediatR;

namespace Edri.Application.Queries.Branches.GetBranchById;

public sealed record GetBranchByIdQuery(Guid BranchId) : IRequest<BranchViewModel?>;