# Advanced Unit Testing Techniques: Enter the Tesseract
## When Testing Becomes Social, Time Cannot Be Escaped
### Live Coding Session for Advanced C# Developers

### 🧊 **The Tesseract Metaphor**
In physics, a tesseract is a 4-dimensional cube. In testing, the metaphor is relevant:
- **1D**: Simple method calls (solitary tests)
- **2D**: Object interactions (social tests)
- **3D**: Realistic doubles (social tests + infrastructures)
- **4D**: **TIME** - When dependencies are created, live, and die

**When unit testing becomes social, time cannot be escaped!**

### 🎯 **Learning Objectives**
By the end of this session, attendees will understand:
- How testing evolves from simple to social (1D → 4D)
- How dependency injection solves real-world problems
- How ServiceProvider works internally (the magic behind DI)
- Service lifetimes and their practical implications
- How DI enables advanced testing strategies
- **How to test service lifecycles using xUnit's time boundaries**
- **Why controlling time and lifecycle matters in realistic tests**
- Universal DI concepts that apply across frameworks (Windsor, Autofac, etc.)

---

## 📋 **Session Structure (65 minutes)**

### **Part 1: The Problem (10 minutes)**
**File: `T10_1_NoDI_TightlyCoupled.cs`**

**🎤 Talking Points:**
- "Let's start with a common scenario - an order processing system"
- "This looks familiar, right? But there are hidden problems..."
- **Live Demo:** Show the tightly coupled code
- **Problems to highlight:**
  - Cannot mock EmailService for testing
  - Cannot test in isolation
  - Hard to maintain and extend
  - Violates Single Responsibility Principle

**🔴 Key Message:** "This is why we need dependency injection!"

---

### **Part 2: The Solution - Manual DI (10 minutes)**
**File: `T10_2_ManualDI_WithInterfaces.cs`**

**🎤 Talking Points:**
- "Let's fix this with interfaces and constructor injection"
- **Live Demo:** Refactor to use interfaces
- **Benefits to highlight:**
  - Can inject mocks/stubs for testing
  - Can test in isolation
  - Follows Dependency Inversion Principle
  - More maintainable and extensible

**🟢 Key Message:** "This is the foundation of all DI frameworks!"

---

### **Part 3: How ServiceProvider Works (15 minutes)**
**File: `T11_1_SimpleServiceProvider.cs`**

**🎤 Talking Points:**
- "Now let's build our own ServiceProvider to understand how it works"
- "Every DI framework does essentially this, just more sophisticated"

**Live Coding Steps:**
1. **ServiceDescriptor** - "This holds the registration information"
2. **Registration methods** - "This is like `services.AddTransient<>`"
3. **GetService method** - "This is the heart of the container"
4. **CreateInstance with reflection** - "This is how constructor injection works"

**🔵 Key Message:** "This is what happens inside Microsoft.Extensions.DependencyInjection!"

---

### **Part 4: Service Lifetimes Deep Dive (15 minutes)**
**File: `T12_1_ScopedServiceProvider.cs`**

**🎤 Talking Points:**
- "Now let's add the most important concept: service lifetimes"
- "This is where many developers get confused"

**Live Demo Sequence:**
1. **Transient** - "New instance every time - like `new()` but managed"
2. **Singleton** - "One instance for the entire application lifetime"
3. **Scoped** - "One instance per scope (like HTTP request)"

**Real-world Examples:**
- **Transient**: Controllers, business services
- **Scoped**: DbContext, user session data
- **Singleton**: Configuration, caching services

**🟡 Key Message:** "Understanding lifetimes prevents memory leaks and threading issues!"

---

### **Part 5: Microsoft DI Comparison (5 minutes)**
**File: `T12_2_MicrosoftDI_Comparison.cs`**

**🎤 Talking Points:**
- "Let's compare our implementation with Microsoft's"
- "See? Same concepts, just more features and optimizations"

**Quick Demo:**
- Show identical behavior between custom and Microsoft DI
- Highlight that concepts are universal across frameworks

**🟣 Key Message:** "Once you understand these concepts, any DI framework makes sense!"

---

### **Part 6: Testing with DI (10 minutes)**
**File: `T13_1_TestingStrategies.cs`**

**🎤 Talking Points:**
- "This is where DI really shines - testing becomes so much easier"
- "Let me show you different testing strategies"

**Live Demo Sequence:**
1. **Unit Tests with Mocks** - "Fast, isolated, predictable"
2. **Error Scenario Testing** - "Easy to simulate failures"
3. **Integration Tests** - "Test with real services"
4. **Container-based Testing** - "Mix real and mock services"

**🟢 Key Message:** "DI transforms testing from painful to powerful!"

---

### **Part 7: Lifecycle Testing - Time Matters! (10 minutes)**
**File: `T15_1_ServiceLifecycleWithXUnit.cs`**

**🎤 Talking Points:**
- "Here's the advanced topic most developers miss - testing service lifetimes!"
- "Time and lifecycle matter in tests - we need to control when things start and end"
- "xUnit's lifecycle gives us the perfect tool to verify DI lifetimes"

**Live Demo Sequence:**
1. **Transient Lifecycle** - "New instances per resolution, even within same test"
2. **Scoped Lifecycle** - "Controlled disposal when scope ends"
3. **Singleton Lifecycle** - "Lives for entire provider lifetime"
4. **xUnit Integration** - "Test class per method vs fixture across tests"

**Real-world Impact:**
- **Memory leaks** - Wrong lifetimes cause resource leaks
- **State pollution** - Shared state between tests
- **Timing issues** - Services disposed at wrong time
- **Test isolation** - Ensuring tests don't affect each other

**🔥 Key Message:** "Mastering lifecycles prevents production bugs and flaky tests!"

---

## 🎯 **Key Takeaways for Audience**

### **Universal DI Concepts (Apply to ALL frameworks):**
1. **Constructor Injection** - Dependencies passed through constructor
2. **Service Registration** - Tell the container what to create
3. **Lifetime Management** - Control when instances are created/disposed
4. **Dependency Resolution** - Container builds the object graph
5. **Interface Segregation** - Depend on abstractions, not implementations
6. **🕒 Lifecycle Testing** - Verify services are created/disposed at correct times
7. **⏰ Time Boundaries** - Use test framework lifecycle to control service lifetimes

### **Service Lifetimes Summary:**
```csharp
// Transient: New instance every time
services.AddTransient<IEmailService, EmailService>();

// Scoped: Same instance within a scope (HTTP request)
services.AddScoped<IOrderService, OrderService>();

// Singleton: Same instance for entire application
services.AddSingleton<IConfiguration, Configuration>();
```

### **Testing Benefits:**
- ✅ **Unit Tests**: Fast, isolated, predictable
- ✅ **Integration Tests**: Test real service interactions
- ✅ **Error Scenarios**: Easy to simulate failures
- ✅ **Hybrid Tests**: Mix real and mock services
- ✅ **Lifecycle Tests**: Verify service creation/disposal timing
- ✅ **Time Control**: Use xUnit lifecycle to control test boundaries

---

## 🛠️ **Framework Comparison**

| Concept | Microsoft DI | Windsor | Autofac |
|---------|-------------|---------|---------|
| Registration | `AddTransient<>()` | `Register()` | `RegisterType<>()` |
| Lifetimes | Transient/Scoped/Singleton | Transient/Scoped/Singleton | InstancePerDependency/PerLifetimeScope/SingleInstance |
| Resolution | `GetService<>()` | `Resolve<>()` | `Resolve<>()` |
| Scopes | `CreateScope()` | `BeginScope()` | `BeginLifetimeScope()` |

**🔑 Key Point:** "The syntax differs, but the concepts are identical!"

---

## 🎤 **Presentation Tips**

### **Opening Hook:**
*"How many of you think unit testing is simple? Just input → output, right? Well, what happens when your tests become social - when objects have dependencies? Suddenly you're not in Kansas anymore... you've entered the tesseract! Today we'll explore the 4th dimension of testing: TIME. Because when testing becomes social, time cannot be escaped!"*

### **Interactive Elements:**
- Ask audience to predict what will happen before running tests
- "What lifetime would you use for a database connection?"
- "How would you test this without mocks?"

### **Closing:**
*"You've now mastered the tesseract of testing! You understand how to navigate all four dimensions - from simple methods to complex social interactions, and most importantly, how to control TIME in your tests. You've seen how service lifetimes work under the hood, and how xUnit gives you the tools to test them properly. Whether you use Microsoft DI, Windsor, Autofac, or any other container, these principles remain the same. Welcome to the 4th dimension of testing - go forth and test through time!"*

---

## 📝 **Preparation Checklist**

- [ ] Test all code examples beforehand
- [ ] Prepare backup slides in case of technical issues
- [ ] Have examples of common DI mistakes ready
- [ ] Prepare answers for common questions:
  - "When should I use Singleton vs Scoped?"
  - "How do I handle circular dependencies?"
  - "What about performance implications?"
- [ ] Set up IDE with proper syntax highlighting
- [ ] Test microphone and screen sharing

---

## 🎬 **Demo Flow Script**

### **Opening (2 minutes)**
```
"Good morning! How many of you have ever struggled with testing code that creates its own dependencies? 
[Show of hands] Perfect! Today we're going to solve that problem and understand exactly how 
dependency injection works under the hood."
```

### **Part 1 Transition (1 minute)**
```
"Let me start with some code that might look familiar..."
[Open T10_1_NoDI_TightlyCoupled.cs]
"This is an order service that sends emails. Looks reasonable, right? Let's see what happens when we try to test it..."
```

### **Part 2 Transition (1 minute)**
```
"Now let's fix this the right way..."
[Open T10_2_ManualDI_WithInterfaces.cs]
"See how we can now inject a mock? This is the foundation of every DI framework!"
```

### **Part 3 Transition (1 minute)**
```
"But manually creating all these dependencies gets tedious. What if we could automate it? 
Let's build our own ServiceProvider to see how it works..."
[Open T11_1_SimpleServiceProvider.cs]
```

### **Part 4 Transition (1 minute)**
```
"Now for the most important concept - service lifetimes. This is where many developers get confused, 
but once you understand it, everything clicks..."
[Open T12_1_ScopedServiceProvider.cs]
```

### **Part 5 Transition (30 seconds)**
```
"Let's compare our implementation with Microsoft's to see we're on the right track..."
[Open T12_2_MicrosoftDI_Comparison.cs]
```

### **Part 6 Transition (30 seconds)**
```
"Finally, let's see how all this makes testing incredibly powerful..."
[Open T13_1_TestingStrategies.cs]
```

---

## 🤔 **Common Questions & Answers**

### **Q: "When should I use Singleton vs Scoped?"**
**A:** "Singleton for stateless services that are expensive to create (like configuration). Scoped for services that should live for the duration of a request (like DbContext). Transient for lightweight, stateless services."

### **Q: "What about performance?"**
**A:** "DI containers are highly optimized. The reflection cost is only paid once per type. Modern containers use compiled expressions for near-native performance."

### **Q: "How do I handle circular dependencies?"**
**A:** "Design issue! If A needs B and B needs A, you probably need to extract shared functionality into a third service C that both depend on."

### **Q: "Can I mix different DI frameworks?"**
**A:** "Generally no - each container manages its own object graph. But you can bridge between them if absolutely necessary."

---

## 🎯 **Success Metrics**

After this session, attendees should be able to:
- [ ] Identify tightly coupled code and explain why it's problematic
- [ ] Implement constructor injection manually
- [ ] Explain how ServiceProvider resolves dependencies
- [ ] Choose appropriate service lifetimes for different scenarios
- [ ] Write effective unit tests using dependency injection
- [ ] Apply these concepts to any DI framework (Windsor, Autofac, etc.)
