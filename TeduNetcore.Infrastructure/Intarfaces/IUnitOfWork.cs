using System;

namespace TeduNetcore.Infrastructure.Intarfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// [Call] when save change DB Context
        /// </summary>
        void Commit();
    }
}
