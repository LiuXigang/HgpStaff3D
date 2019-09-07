using System;
using System.Threading;
using System.Threading.Tasks;

namespace HgpStaff3D.Domain.SeedWork
{
    /// <summary>
    /// 统一事务操作
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// context保存
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// 发送领域事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DispatchDomainEventsAsync(CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// context保存 && 发送领域事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
