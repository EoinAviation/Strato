// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITransaction.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Persistence.Abstractions
{
    using System;
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     The interface representing a database transaction.
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        ///     Commits all changes made to the database in the current transaction.
        /// </summary>
        void Commit();

        /// <summary>
        ///     Commits all changes made to the database in the current transaction as an asynchronous operation.
        /// </summary>
        /// <param name="cancellationToken">
        ///     The <see cref="CancellationToken"/> to observe while waiting for the <see cref="Task"/> to complete.
        /// </param>
        /// <returns>
        ///     A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Discards all changes made to the database in the current transaction.
        /// </summary>
        void Rollback();

        /// <summary>
        ///     Discards all changes made to the database in the current transaction asynchronously.
        /// </summary>
        /// <param name="cancellationToken">
        ///     The <see cref="CancellationToken"/> to observe while waiting for the <see cref="Task"/> to complete.
        /// </param>
        /// <returns>
        ///     A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        Task RollbackAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Gets the <see cref="DbTransaction"/> for the current <see cref="ITransaction"/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="DbTransaction"/>.
        /// </returns>
        DbTransaction GetDbTransaction();
    }
}
