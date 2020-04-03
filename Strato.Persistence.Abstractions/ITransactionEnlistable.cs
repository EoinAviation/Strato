// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITransactionEnlistable.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Persistence.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     The interface representing a persistence service capable of enlisting <see cref="ITransaction"/>s.
    /// </summary>
    public interface ITransactionEnlistable
    {
        /// <summary>
        ///     Sets the <see cref="ITransaction"/> to be used by persistence operations on the
        ///     <see cref="IDatabase"/>.
        /// </summary>
        /// <param name="transaction">
        ///     The <see cref="ITransaction"/> to use.
        /// </param>
        /// <returns>
        ///     A <see cref="ITransaction"/> that encapsulates the given transaction.
        /// </returns>
        ITransaction UseTransaction(ITransaction transaction);

        /// <summary>
        ///     Sets the <see cref="ITransaction"/> to be used by persistence operations on the
        ///     <see cref="IDatabase"/> as an asynchronous operation.
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
        Task<ITransaction> UseTransactionAsync(ITransaction transaction, CancellationToken cancellationToken = default);
    }
}
