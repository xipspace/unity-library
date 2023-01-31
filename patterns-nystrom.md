
# Patterns

Notes from Nystrom's book [Game Programming Patterns](https://gameprogrammingpatterns.com/)

## Patterns Revisited

### Command

Commands are an object-oriented replacement for callbacks.
<br>When you have an interface with a single method that doesn't return anything, there's a good chance it's the Command pattern.
<br>If a command object can do things, it's a small step for it to be able to undo them.

### Flyweight

When you have objects that need to be more lightweight, generally because you have too many of them.
<br>Separating out an object's data into two kinds. The first kind of data is the stuff that's not specific to a single instance of that object and can be shared across all of them.

### Observer

It lets one piece of code announce that something interesting happened without actually caring who receives the notification.
<br>The notification method is invoked by the object being observed. That object is called the subject.

### Prototype

The key idea is that an object can spawn other objects similar to itself.
<br>Each subclass provides an implementation that returns a new object identical in class and state to itself.

### Singleton

Ensure a class has one instance, and provide a global point of access to it.
<br>An assertion function is a way of embedding a contract into your code.
<br>Assertions help us track down bugs as soon as the game does something unexpected, not later when that error finally manifests as something visibly wrong to the user.

### State

Allow an object to alter its behavior when its internal state changes.
<br>With a simple state machine implementation, we have to duplicate code in each state. It would be better if we could implement once and reuse it across all of the states.
<br>This is a common structure called a hierarchical state machine. A state can have a superstate (making itself a substate). It works just like overriding inherited methods.
<br>The problem is that finite state machines have no concept of history. You know what state you are in, but have no memory of what state you were in.
<br>What we'd really like is a way to store the state she was in before firing and then recall it later. The relevant data structure is called a pushdown automaton.
<br>Even with those common extensions to state machines, they are still pretty limited. The trend these days in game AI is more toward exciting things like behavior trees and planning systems.

## Sequencing Patterns

### Double Buffer

A buffered class encapsulates a buffer: a piece of state that can be modified.
<br>When information is read from a buffer, it is always from the current buffer.
<br>When information is written to a buffer, it occurs on the next buffer.
<br>When the changes are complete, a swap operation swaps the next and current buffers.

### Game Loop

Decouple the progression of game time from user input and processor speed.
<br>It tracks the passage of time to control the rate of gameplay.

### Update Method

Once per frame, the game loop walks the collection and calls update() on each object.
<br>This gives each one a chance to perform one frame's worth of behavior.
<br>By calling it on all objects every frame, they all behave simultaneously.

## Behavioral

Behavioral design patterns are design patterns that identify common communication patterns among objects.

### Bytecode

An instruction set defines the low-level operations that can be performed.
<br>A series of instructions is encoded as a sequence of bytes.
<br>A virtual machine executes these instructions one at a time, using a stack for intermediate values.

### Subclass Sandbox

A base class defines an abstract sandbox method and several provided operations.
<br>Marking them protected makes it clear that they are for use by derived classes.
<br>With Subclass Sandbox, the method is in the derived class and the primitive operations are in the base class.

### Type object

Each typed object have a reference to type object that describes type.
<br>Able to modify or add new types without having to recompile or change code.
<br>Easy to use type objects to define type-specific data, hard to define type-specific behavior.

## Decoupling

### Components

Instead of sharing code between two classes by having them inherit from the same class, we do so by having them both own an instance of the same class.
<br>Decorations are things in the world the player sees but doesn't interact with: bushes, debris and other visual detail.
<br>Props are like decorations but can be touched: boxes, boulders, and trees.
<br>Zones are the opposite of decorations — invisible but interactive. They're useful for things like triggering a cutscene.

### Event Queue

This application style is so common, it's considered a paradigm: event-driven programming.
<br>An event or notification describes something that already happened. Other objects can respond to the event.
<br>A message or request describes an action that we want to happen in the future. You can think of this as an asynchronous API to a service.

### Service Locator

A service class defines an abstract interface to a set of operations.
<br>A concrete service provider implements this interface.
<br>A separate service locator provides access to the service by finding an appropriate provider while hiding both the provider's concrete type and the process used to locate it.
<br>Service Locator pattern is a sibling to Singleton.

## Optimization

### Data Locality

Successfully finding a piece of data in the cache is called a cache hit.
<br>If it can't find it in there and has to go to main memory, that's a cache miss.
<br>Keeping data in contiguous memory in the order that you process it.
<br>Stuff it checks and tweaks every single frame and data is only used once in the entity's lifetime.
<br>The idea is to break our data structure into two separate pieces. The first holds "the" hot data, the state we need to touch every frame. The other piece is the "cold" data, everything else that gets used less frequently.
<br>Avoid subclassing, or at least avoid it in places where you're optimizing for cache usage. Software engineer culture is drifting away from heavy use of inheritance anyway.
<br>One way to keep much of the flexibility of polymorphism without using subclassing is through the Type Object pattern.

### Dirty Flag

Decoupling changing local transforms from updating the world transforms.
<br>Change a bunch of local transforms in a single batch and then recalculate the affected world transform.
<br>We add a flag to each object. When the local transform changes, we set it.
<br>When we need the object's world transform, we check the flag. If it's set, we calculate the world transform and then clear the flag.
<br>When you use this pattern, you'll have to take care that any code that modifies the primary state also sets the dirty flag.
<br>It avoids doing calculation entirely if the result is never used.

### Object Pooling

Define a pool class that maintains a collection of reusable objects.
<br>Need when frequently create and destroy objects.

### Spatial Partition

For a set of objects, each has a position in space. Store them in a spatial data structure that organizes the objects by their positions.
<br>The goal is have a balanced partitioning where each region has roughly the same number of objects in order to get the best performance.
<br>A quadtree starts with the entire space as a single partition. If the number of objects in the space exceeds some threshold, it is sliced into four smaller squares. The boundaries of these squares are fixed.

