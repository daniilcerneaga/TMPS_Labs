# Laboratory Work No. 2: Creational Design Patterns

## Student Information

- **Student:** Daniil Cerneaga
- **Group:** FAF-243

## Objective

Study the Singleton, Builder, and Prototype creational design patterns and demonstrate their application in a C# console application set in a small RPG scenario.

## Technologies

- C#
- .NET 8
- Console application

## Application Description

The application demonstrates the creation and management of RPG characters. A shared game world stores the characters, a builder assembles the hero step by step, and enemy characters are created by cloning a prototype.

The demonstration follows these steps:

1. Retrieve the game world twice and verify that both references point to the same instance.
2. Build the hero by setting its name, role, weapon, health, and abilities.
3. Create a goblin prototype and clone it to create two enemies.
4. Add an ability to one clone and show that the other clone and the prototype remain unchanged.
5. Print the characters stored in the shared game world.

This is an in-memory console demonstration. It does not implement gameplay, combat, or persistent storage. The character builder validates that the name, role, and weapon are not empty and that health is greater than zero; abilities are optional and are not validated.

## Application Structure

The source code is organized into the following files:

| Component | Responsibility |
| --- | --- |
| `GameCharacter` | Stores character properties, creates copies with `Clone()`, and formats a character for console output. |
| `CharacterBuilder` | Collects character values through fluent methods, validates required values, and creates a `GameCharacter`. |
| `GameWorld` | Provides the shared singleton instance and stores and prints characters. |
| `Program` | Creates the world, builds the hero, clones enemies, and runs the demonstration. |

## Application of Creational Design Patterns

### Singleton

The Singleton pattern ensures that a class has one shared instance and provides a common access point to it.

`GameWorld` holds a `Lazy<GameWorld>` instance and exposes it through the static `Instance` property. Its constructor is private, so other classes cannot create a separate game world directly. `Program.Main()` retrieves the instance twice and uses `ReferenceEquals()` to demonstrate that both references point to the same object.

The singleton stores the characters added during the demonstration. The `Lazy<T>` wrapper defers instance creation until `Instance` is first accessed.

### Builder

The Builder pattern constructs an object step by step, keeping the setup of a complex object separate from its final creation.

`CharacterBuilder` provides fluent methods for setting a character's name, role, weapon, and health, and for adding abilities. Each method returns the same builder, allowing calls to be chained. `Build()` checks that the name, role, and weapon are present and that health is positive. If validation succeeds, it returns a new `GameCharacter` with a separate abilities list.

This makes the hero's setup in `Program.Main()` readable and keeps the construction and validation logic out of the demonstration code.

### Prototype

The Prototype pattern creates new objects by copying an existing object rather than building each one from scratch.

`GameCharacter.Clone(string newName)` uses `MemberwiseClone()` to copy the character's scalar properties, assigns the requested name, and creates a new list containing the prototype's abilities. Copying the list is important: without it, changing one character's abilities would also affect the prototype and its other clones.

The demonstration creates two goblins from one prototype, then adds `Battle cry` to only the first clone. The console output confirms that the second clone and the prototype retain their original abilities.

## Running the Application

Install the .NET 8 SDK, then run the following command from the `TMPS Labs` repository root:

```powershell
dotnet run --project "TMPS Lab 2/GamePatternsLab.csproj"
```

## Console Output

```text
=== RPG: Creational Patterns Demo ===

[Singleton] One game world: True

[Builder] Hero assembled step by step:
Aria | Knight | HP: 120 | Weapon: Moonsteel sword | Abilities: Shield Bash, Second Wind

[Prototype] Two enemies cloned from one template:
Goblin #1 | Enemy / Scout | HP: 35 | Weapon: Rusty dagger | Abilities: Quick stab, Dodge, Battle cry
Goblin #2 | Enemy / Scout | HP: 35 | Weapon: Rusty dagger | Abilities: Quick stab, Dodge
Prototype abilities remain unchanged: Quick stab, Dodge

[Singleton] Characters in the shared game world:
- Aria (Knight)
- Goblin #1 (Enemy / Scout)
- Goblin #2 (Enemy / Scout)
```

The application was successfully run using `dotnet run`. The output demonstrates that the game world is shared, the hero is assembled through the builder, and modifying one prototype clone does not modify the other clone or the original template.

## Conclusion

The application demonstrates three creational design patterns in an RPG scenario. Singleton provides a shared game world, Builder creates a validated character through a clear sequence of steps, and Prototype creates independent character copies from a template. Together, these patterns separate object creation from the main demonstration flow.