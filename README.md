# Strato
Strato is a set of .NET Core libraries developed by Strato Systems Pty. Ltd.
The aim of these libraries is primarily to reduce the amount of time spent writing boilerplate code, creating and implementing commonly used abstractions, all that fun stuff.

## Libraries
### Strato.Persistence.Abstractions
The `Strato.Persistence.Abstractions` library contains interfaces for abstracting database access, such as EF Core Database Contexts.
NuGet: https://www.nuget.org/packages/Strato.Persistence.Abstractions/

### Strato.Persistence.EntityFrameworkCore
The `Strato.Persistence.EntityFrameworkCore` library contains implementations of interfaces in the `Strato.Persistence.Abstractions` library for Entity Framework Core.
NuGet: https://www.nuget.org/packages/Strato.Persistence.EntityFrameworkCore/

### Strato.EventAggregator
The `Strato.EventAggregator` library provides a simple implementation of the Event Aggregator pattern.
NuGet: https://www.nuget.org/packages/Strato.EventAggregator/

### Strato.Mvvm
The `Strato.Mvvm` library contains classes and interfaces for use in an MVVM (Model-View-ViewModel) project, such as a WPF or Xamarin application. Note that `Strato.EventAggregator` is a dependency of `Strato.Mvvm`.
NuGet: https://www.nuget.org/packages/Strato.Mvvm/

### Strato.Mvvm.Wpf
The `Strato.Mvvm.Wpf` library contains implementations of interfaces in the `Strato.Mvvm` library, and some extra tools required to bridge the gap between the `Strato.Mvvm` library, and WPF, as well as simplifying the implementation of `Strato.Mvvm` into a WPF project.
NuGet: https://www.nuget.org/packages/Strato.Mvvm.Wpf/

### Strato.Extensions
The `Strato.Extensions` library contains extension methods which just add some extra functionality to pre-existing .NET types.
E.g. `AddOrUpdate` on `Dictionary<TKey, TValue>`, `FireAndForgetSafeAsync` on `Task`.
NuGet: https://www.nuget.org/packages/Strato.Extensions/

### Strato.Analyzers
The `Strato.Analyzers` library contains a StyleCop analyzer configurations for providing style consistency throughout all Strato Systems Pty. Ltd. source code.
For simplicity with deployment, this library has been moved into a [separate repository](https://github.com/EoinAviation/Strato.Analyzers).
