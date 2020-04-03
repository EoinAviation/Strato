// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Transaction.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Persistence.EntityFrameworkCore
{
    using System;
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Storage;

    using Strato.Persistence.Abstractions;

    /// <summary>
    ///     The <see cref="ITransaction"/> implementation for Entity Framework Core, backed by an
    ///     <see cref="IDbContextTransaction"/>.
    /// </summary>
    public class Transaction : ITransaction
    {
        /// <summary>
        ///     The <see cref="IDbContextTransaction"/>.
        /// </summary>
        private readonly IDbContextTransaction _dbContextTransaction;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <param name="transaction">
        ///     The <see cref="IDbContextTransaction"/>.
        /// </param>
        public Transaction(IDbContextTransaction transaction) =>
            _dbContextTransaction = transaction ??
                                    throw new ArgumentNullException(
                                        nameof(transaction),
                                        "The DbContext Transaction cannot be null.");

        /// <summary>
        ///     Commits all changes made to the database in the current transaction.
        /// </summary>
        public void Commit() => _dbContextTransaction.Commit();

        /// <summary>
        ///     Commits all changes made to the database in the current transaction as an asynchronous operation.
        /// </summary>
        /// <param name="cancellationToken">
        ///     The <see cref="CancellationToken"/> to observe while waiting for the <see cref="Task"/> to complete.
        /// </param>
        /// <returns>
        ///     A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task CommitAsync(CancellationToken cancellationToken = default) =>
            await _dbContextTransaction.CommitAsync(cancellationToken);

        /// <summary>
        ///     Discards all changes made to the database in the current transaction.
        /// </summary>
        public void Rollback() => _dbContextTransaction.Rollback();

        /// <summary>
        ///     Discards all changes made to the database in the current transaction asynchronously.
        /// </summary>
        /// <param name="cancellationToken">
        ///     The <see cref="CancellationToken"/> to observe while waiting for the <see cref="Task"/> to complete.
        /// </param>
        /// <returns>
        ///     A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task RollbackAsync(CancellationToken cancellationToken = default) =>
            await _dbContextTransaction.RollbackAsync(cancellationToken);

        /// <summary>
        ///     Gets the <see cref="DbTransaction"/> for the current <see cref="ITransaction"/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="DbTransaction"/>.
        /// </returns>
        public DbTransaction GetDbTransaction() => _dbContextTransaction.GetDbTransaction();

        /// <summary>
        ///     Releases the unmanaged resources used by the <see cref="Transaction"/>.
        /// </summary>
        public void Dispose() => _dbContextTransaction?.Dispose();
    }
}
