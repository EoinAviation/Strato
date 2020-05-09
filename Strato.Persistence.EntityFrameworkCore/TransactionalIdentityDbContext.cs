// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransactionalIdentityDbContext.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Persistence.EntityFrameworkCore
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;

    using Strato.Persistence.Abstractions;

    /// <summary>
    ///     The abstract <see cref="IdentityDbContext"/> implementing <see cref="IDbContext"/> with more direct support
    ///     for <see cref="ITransaction"/>s.
    /// </summary>
    public abstract class TransactionalIdentityDbContext : IdentityDbContext, IDbContext, ITransactionEnlistable, ITransactionFactory
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TransactionalIdentityDbContext"/> class.
        /// </summary>
        /// <param name="options">
        ///     The <see cref="DbContextOptions"/>.
        /// </param>
        protected TransactionalIdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        ///     Gets the currently enlisted <see cref="ITransaction"/>.
        /// </summary>
        /// <returns>
        ///     The currently enlisted <see cref="ITransaction"/>. If no <see cref="ITransaction"/>, then <c>null</c>
        ///     will be returned.
        /// </returns>
        public ITransaction GetCurrentTransaction()
        {
            IDbContextTransaction currentTransaction = Database.CurrentTransaction;
            return currentTransaction == null ? null : new Transaction(currentTransaction);
        }

        /// <summary>
        ///     Starts a new transaction.
        /// </summary>
        /// <returns>
        ///     A <see cref="ITransaction"/> that represents the started transaction.
        /// </returns>
        public ITransaction BeginTransaction()
        {
            IDbContextTransaction transaction = Database.BeginTransaction();

            return new Transaction(transaction);
        }

        /// <summary>
        ///     Asynchronously starts a new transaction.
        /// </summary>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken"/> to observe while waiting for the <see cref="Task"/> to complete.
        /// </param>
        /// <returns>
        ///     A <see cref="Task{T}"/> that represents the asynchronous transaction initialization.
        ///     The <see cref="Task{T}.Result"/> contains an <see cref="ITransaction" /> that represents the started
        ///     transaction.
        /// </returns>
        public async Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            IDbContextTransaction transaction = await Database.BeginTransactionAsync(cancellationToken);

            return new Transaction(transaction);
        }

        /// <summary>
        ///     Sets the <see cref="ITransaction"/> to be used by persistence operations on the
        ///     <see cref="IDbContext"/>.
        /// </summary>
        /// <param name="transaction">
        ///     The <see cref="ITransaction"/> to use.
        /// </param>
        /// <returns>
        ///     A <see cref="ITransaction"/> that encapsulates the given transaction.
        /// </returns>
        public ITransaction UseTransaction(ITransaction transaction)
        {
            IDbContextTransaction newTransaction = Database.UseTransaction(transaction.GetDbTransaction());

            return new Transaction(newTransaction);
        }

        /// <summary>
        ///     Sets the <see cref="ITransaction"/> to be used by persistence operations on the
        ///     <see cref="IDbContext"/> as an asynchronous operation.
        /// </summary>
        /// <param name="transaction">
        ///     The <see cref="ITransaction"/> to use.
        /// </param>
        /// <param name="cancellationToken">
        ///     The <see cref="CancellationToken"/> to observe while waiting for the <see cref="Task{T}"/> to complete.
        /// </param>
        /// <returns>
        ///     A <see cref="Task{T}"/> representing the asynchronous operation.
        ///     The <see cref="Task{T}.Result"/> contains the <see cref="ITransaction"/> that encapsulates the given
        ///     transaction.
        /// </returns>
        public async Task<ITransaction> UseTransactionAsync(
            ITransaction transaction,
            CancellationToken cancellationToken = default) =>
            new Transaction(await Database.UseTransactionAsync(transaction.GetDbTransaction(), cancellationToken));
    }
}
