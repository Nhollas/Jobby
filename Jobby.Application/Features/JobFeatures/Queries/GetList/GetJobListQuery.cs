using Jobby.Application.Contracts.Board;
using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.GetList;

public sealed record GetJobListQuery(): IRequest<List<PreviewJobDto>>;