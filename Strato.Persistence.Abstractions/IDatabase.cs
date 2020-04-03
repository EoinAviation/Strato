// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDbContext.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Persistence.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     The interface representing a database.
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        ///     Saves all changes made in this context to the database.
        ///     This method will automatically call ChangeTracker.DetectChanges
        ///     to discover any changes to entity instances before saving to the underlying database.
        ///     This can be disabled via ChangeTracker.AutoDetectChangesEnabled.
        /// </summary>
        /// <returns>
        ///     The number of state entries written to the database.
        /// </returns>
        int SaveChanges();

        /// <summary>
        ///     Saves all changes made in this context to the database.
        ///     This method will automatically call ChangeTracker.DetectChanges
        ///     to discover any changes to entity instances before saving to the underlying database.
        ///     This can be disabled via ChangeTracker.AutoDetectChangesEnabled.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">
        ///     Indicates whether ChangeTracker.AcceptAllChanges
        ///     is called after the changes have been sent successfully to the database.
        /// </param>
        /// <returns>
        ///     The number of state entries written to the database.
        /// </returns>
        int SaveChanges(bool acceptAllChangesOnSuccess);

        /// <summary>
        ///     Saves all changes made in this context to the database.
        ///     This method will automatically call ChangeTracker.DetectChanges
        ///     to discover any changes to entity instances before saving to the underlying database.
        ///     This can be disabled via ChangeTracker.AutoDetectChangesEnabled
        ///     Multiple active operations on the same context instance are not supported. Use
        ///     'await' to ensure that any asynchronous operations have completed before calling
        ///     another method on this context.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">
        ///     Indicates whether ChangeTracker.AcceptAllChanges
        ///     is called after the changes have been sent successfully to the database.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken"/> to observe while waiting for the <see cref="Task{T}"/> to complete.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> that represents the asynchronous save operation. The task result contains the
        ///     number of state entries written to the database.
        /// </returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Saves all changes made in this context to the database.
        ///     This method will automatically call ChangeTracker.DetectChanges
        ///     to discover any changes to entity instances before saving to the underlying database.
        ///     This can be disabled via ChangeTracker.AutoDetectChangesEnabled
        ///     Multiple active operations on the same context instance are not supported. Use
        ///     'await' to ensure that any asynchronous operations have completed before calling
        ///     another method on this context.
        /// </summary>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken"/> to observe while waiting for the <see cref="Task{T}"/> to complete.
        /// </param>
        /// <returns>
        ///     A <see cref="Task{T}"/> that represents the asynchronous save operation.
        ///     The <see cref="Task{T}.Result"/> contains the number of state entries written to the database.
        /// </returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
