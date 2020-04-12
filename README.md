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
The `Strato.Analyzers` library contains a StyleCop analyzer configurations for providing style consistency throughout all Strato Systems Pty. Ltd. source code.
For simplicity with deployment, this library has been moved into a [separate repository](https://github.com/EoinAviation/Strato.Analyzers).

## Usage
A NuGet package is available through the GitHub Package Repository [here](https://github.com/EoinAviation/Strato/packages/).
To make this source available in your IDE, add `https://nuget.pkg.github.com/eoinaviation/index.json` to your NuGet package feeds (Tutorials for [Visual Studio](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio#package-sources), [Rider](https://www.jetbrains.com/help/rider/Using_NuGet.html#sources))
Your IDE may prompt you for a username and password, use your GitHub username for the username, and you'll need a personal access token with read access to the GitHub Package registry for the password. More info [here](https://help.github.com/en/packages/using-github-packages-with-your-projects-ecosystem/configuring-dotnet-cli-for-use-with-github-packages)