namespace Strato.Mvvm.Commands
{
    using System.Windows.Input;

    // Todo: Find a better name.

    /// <summary>
    ///     The extended version of the <see cref="ICommand"/>.
    /// </summary>
    public interface IExtendedCommand : ICommand
    {
        /// <summary>
        ///     Determines whether the current <see cref="IExtendedCommand"/> can be executed in its current state.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the current <see cref="IExtendedCommand"/> can be executed; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute();

        /// <summary>
        ///     Executes the current <see cref="IExtendedCommand"/>.
        /// </summary>
        void Execute();
    }
}
