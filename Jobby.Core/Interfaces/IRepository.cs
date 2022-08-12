using Ardalis.Specification;

namespace Jobby.Core.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
}
