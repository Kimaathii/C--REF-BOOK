# Floor 4 — Dependency Injection, Middleware & Services

How real .NET Web APIs are wired together.

## 📚 What You'll Learn

| Concept | Description |
|---|---|
| Dependency Injection | Classes receive dependencies, never create them |
| Service Lifetimes | Singleton, Scoped, Transient — when to use each |
| Middleware | Request pipeline — how HTTP flows through .NET |
| Service Container | Built-in DI container in .NET |
| Services | Business logic layer in real Web APIs |

## 🚀 How to Run

```bash
cd Floor4-DI/DIPractice
dotnet run
```

## 🎯 Learning Goals

- **DI Container** — IoC (Inversion of Control) fundamentals
- **Three Service Lifetimes** — understanding object lifecycle
- **Middleware Pipeline** — how requests flow through layers
- **Loose Coupling** — write testable, flexible code
- **Real Web API** — apply DI in controller/service patterns

## 📋 Status

Currently in progress (🔨 under construction)

Expected to cover:
- [ ] Manual dependency injection pattern
- [ ] Built-in service container (`AddScoped`, `AddSingleton`, `AddTransient`)
- [ ] Middleware basics
- [ ] Service patterns in Web API context
- [ ] Integration with ASP.NET Core

---

*Dependency Injection separates construction from behavior. This is where .NET shines.*
