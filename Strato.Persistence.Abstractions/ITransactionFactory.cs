// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITransactionFactory.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Persistence.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     The interface representing a class capable of creating <see cref="ITransaction"/>.
    /// </summary>
    public interface ITransactionFactory
    {
        /// <summary>
        ///     Starts a new transaction.
        /// </summary>
        /// <returns>
        ///     A <see cref="ITransaction"/> that represents the started transaction.
        /// </returns>
        ITransaction BeginTransaction();

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
        Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}
