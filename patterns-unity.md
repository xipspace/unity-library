
# Patterns

Notes from Unity's [Level up your code with game programming patterns](https://github.com/Unity-Technologies/game-programming-patterns-demo)

## KISS Principle

Keep it simple, stupid.
<br>Only add complexity if necessary.

## SOLID

### Single-Responsibility Principle

Each module, class or function is responsible for one thing and encapsulates only that part of the logic.

### Open-Closed Principle

Classes must be open for extension but closed for modification.

### Liskov Substitution Principle

Derived classes must be substitutable for their base classes.
<br>Favor composition over inheritance.

### Interface Segregation Principle

Splits interfaces that are very large into smaller and more specific ones so that clients will only have to know about the methods that are of interest to them.
<br>Such shrunken interfaces are also called role interfaces.

### Dependency Inversion Principle

When designing the interaction between a high-level module and a low-level one, the interaction should be thought of as an abstract interaction between them.

## Factory Method

Creates different products that share a common interface.

## Object Pool

The ObjectPool uses a stack to hold a collection of object instances for reuse and is not thread-safe.
<br>Unity +2021 supports its own UnityEngine.Pool API.
<br>ObjectPool also includes options for a default pool size and maximum pool size.
<br>Items exceeding the max pool size trigger an action to self-destruct, keeping memory usage in check.

## Singleton

Because the singleton often serves as an omnipresent manager script, you can benefit from making it persistent using a DontDestroyOnLoad.
<br>Using generics allows you to turn any class into a singleton.
<br>When you declare your class, simply inherit from the generic singleton.

## Command

Encapsulate action or request, giving control over timing and playback.

## State

Change behavior based on internal state.

## Observer

As one object changes state, the other objects respond automatically.
<br>Observing objects are unaware of each other and react independently.
<br>Unity also includes a separate system of UnityEvents, which uses the UnityAction delegate from the UnityEngine.Events API.
<br>Be aware that they may be slower than their equivalent events or actions from the System namespace.
<br>Event-driven architecture adds extra overhead.
<br>Large scenes and many GameObjects can hinder performance.

## Model View Controller

Model is strictly a data container.
<br>View formats and renders a graphical presentation of data.
<br>Controller processes the game data.

## Model View Presenter

Variation of MVC where the controller acts as an intermediary.
<br>View is responsible for handling user input. Presenter manipulates the Model.
<br>State-change event from the Model returns the data to Presenter, which passes the data to the View.

