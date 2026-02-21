using System;
using Edri.Application.ViewModels.Departments;
using MediatR;

namespace Edri.Application.Queries.Departments.GetDepartmentById;

public sealed record GetDepartmentByIdQuery(Guid DepartmentId) : IRequest<DepartmentViewModel?>;