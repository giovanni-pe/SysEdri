using System;
using Edri.Application.ViewModels.Companies;
using MediatR;

namespace Edri.Application.Queries.Companies.GetCompanyById;

public sealed record GetCompanyByIdQuery(Guid CompanyId) : IRequest<CompanyViewModel?>;