# Strato
Strato is a set of .NET Core libraries developed by Strato Systems Pty. Ltd.
The aim of these libraries is primarily to reduce the amount of time spent writing boilerplate code, creating commonly used abstractions, and implementing those abstractions.

## Libraries
### Strato.Persistence.Abstractions
The `Strato.Persistence.Abstractions` library contains interfaces for abstracting database access, such as Database Contexts, transactions, etc.

### Strato.Persistence.EntityFrameworkCore
The `Strato.Persistence.EntityFrameworkCore` library contains implementations of interfaces in the `Strato.Persistence.Abstractions` library for Entity Framework Core.

### Strato.Mvvm
The `Strato.Mvvm` library contains classes and interfaces for use in an MVVM (Model-View-ViewModel) project, such as a WPF or Xamarin application.

### Strato.Analyzers
The `Strato.Analyzers` library contains a custom StyleCop analyzer configuration for providing style consistency throughout all Strato Systems Pty. Ltd. source code.
