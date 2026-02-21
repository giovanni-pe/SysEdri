using System;
using Edri.Application.ViewModels.Provinces;
using MediatR;

namespace Edri.Application.Queries.Provinces.GetProvinceById;

public sealed record GetProvinceByIdQuery(Guid ProvinceId) : IRequest<ProvinceViewModel?>;