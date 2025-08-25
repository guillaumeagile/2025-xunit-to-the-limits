# A word on Dependency Injection in .NET Core

## Built-in container (default)
- Microsoft.Extensions.DependencyInjection
- Supports scopes, open generics, factories, IOptions, IHostedService.
- Missing: named/keyed services (😱), decorators, interception, child containers.  
- Good default for most apps.

*😱 GPT5 is hallucinating: Microsoft.Extensions.DependencyInjection supports since .NET 8 🙏*

## Popular third‑party containers
- Autofac — modules, keyed services, decorators, scanning, property injection. Mature.
- DryIoc — very fast, rich generics/variance, decorators, advanced rules.
- SimpleInjector — strong diagnostics, explicit composition root, decorators.
- Lamar — convention/policy-based registration, decorators (StructureMap successor).
- LightInject — lightweight, fast, decorators and interception.
- Castle Windsor — very mature, powerful interception via DynamicProxy.
*(note from me: back in the day, I used it in 2004 with MonoRail)*

- Stashbox — modern, fast, named/keyed services, decorators.
- Grace — feature-rich, performant, convention support.

Legacy/less chosen new: Unity, Ninject.

Not a container but useful with built-in: Scrutor (assembly scanning + decorators).

## When to choose what
- Stick with built-in unless you need keyed services, decorators, interception, or advanced conventions.
- Autofac / DryIoc for rich features + performance.
- SimpleInjector for diagnostics and explicit, maintainable composition.
- Lamar for convention/policy-driven registration.

---

## Quick integrations

### Autofac (ASP.NET Core, .NET 6+)
```csharp
// Program.cs
using Autofac;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// optional: builder.Services.AddControllers(); etc.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterType<MyService>().As<IMyService>().SingleInstance();
});

var app = builder.Build();
app.MapControllers();
app.Run();