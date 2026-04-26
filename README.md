# 📘 C# .NET Reference Book

A personal, structured reference book for mastering C# and .NET — 
built floor by floor through hands-on constructivist practice.

Every concept is learned by breaking things first, 
understanding why they break, fixing them, and owning them completely.

---

## 🏗️ Structure

```
C--REF-BOOK/
├── Floor1-Classes/          # Classes, Objects, Constructors, Access Modifiers
├── Floor2-OOP/              # OOP Pillars — Encapsulation, Inheritance, Polymorphism, Abstraction
├── Floor3-Interfaces/       # Interfaces, Abstraction, SOLID Principles
├── Floor4-DI/               # Dependency Injection, Middleware, Services
└── README.md
```

---

## 🧱 The Floors

### Floor 1 — Classes & Fundamentals
The foundation everything else is built on.

| Concept | What It Covers |
|---|---|
| Classes & Objects | Blueprints and real instances in memory |
| Access Modifiers | public, private, protected, internal |
| Constructors | Contracts, chaining with this, base |
| Fields vs Properties | Raw storage vs controlled gateways |
| static & this | Class-level members, self-reference |
| readonly | Immutable fields after construction |

---

### Floor 2 — OOP Pillars
The four pillars that make .NET architecture make sense.

| Pillar | Core Idea |
|---|---|
| Encapsulation | Bundle data and behavior, hide internals, protect state |
| Inheritance | Child classes inherit parent — write once, reuse everywhere |
| Polymorphism | One type, many forms — runtime decides behavior |
| Abstraction | Hide complexity, expose only what matters |

---

### Floor 3 — Interfaces & SOLID *(coming)*
Where OOP gets its real superpower.

| Concept | Core Idea |
|---|---|
| Interfaces | Pure contracts with zero implementation |
| SOLID Principles | Five principles of clean, maintainable code |
| Abstraction deep dive | Abstract classes vs interfaces |

---

### Floor 4 — DI, Middleware & Services *(coming)*
How real .NET Web APIs are wired together.

| Concept | Core Idea |
|---|---|
| Dependency Injection | Classes receive dependencies, never create them |
| Service Lifetimes | Singleton, Scoped, Transient |
| Middleware | Request pipeline — how HTTP flows through .NET |
| Services | Business logic layer in real Web APIs |

---

## 🔥 How To Use This Repo

Each floor has independent console projects.
Each project runs on its own:

```bash
cd Floor2-OOP/Polymorphism
dotnet run
```

Each concept folder contains:
- `Program.cs` — entry point, wiring and test code
- Individual `.cs` files — one class per file
- Intentional breaks to demonstrate what goes wrong without each concept

---

## 🧠 Learning Philosophy

This repo is built on **constructivism** —

> You learn more from seeing things break than from reading perfect code.

Every concept follows this pattern:
1. Write it wrong — see it break
2. Understand exactly why it broke
3. Fix it properly
4. Own it completely
5. Connect it to real Web API code

---

## 🗺️ Roadmap

- [x] Floor 1 — Classes, Constructors, Access Modifiers
- [x] Floor 2 — OOP Pillars (Encapsulation, Inheritance, Polymorphism, Abstraction)
- [ ] Floor 3 — Interfaces & SOLID Principles
- [ ] Floor 4 — Dependency Injection & Middleware
- [ ] Floor 5 — Web API (Controllers, Services, DTOs, EF Core)
- [ ] Floor 6 — Authentication & Authorization (JWT, Identity)
- [ ] Floor 7 — Real Project — DevShelf API

---

## ⚙️ Tech Stack

- Language: C# 12
- Platform: .NET 8
- IDE: VS Code
- Projects: Console Apps (learning) → Web API (application)

---

*Built with intention. Every line understood. Nothing memorized.*
