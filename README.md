# Laboratory Work No. 1: SOLID Principles

## Student Information

- **Student:** [Your full name]
- **Group:** [Your group]

## Objective

Study the five SOLID principles and demonstrate their application in a C# console application using interfaces, separate responsibilities, and polymorphism.

## Technologies

- C#
- .NET 10
- Console application

## Application Description

The application simulates a power socket connected to a desk lamp and a smart air conditioner. A remote control can turn on the air conditioner.

The demonstration follows these steps:

1. Create a power socket, a remote control, and two devices.
2. Connect the lamp and the air conditioner to the socket.
3. Supply power: the lamp emits light, while the air conditioner remains in standby mode.
4. Turn on the air conditioner using the remote control.
5. Supply power again: the lamp continues to emit light, and the air conditioner cools the room.

This is a behavioral simulation; it does not control physical devices or calculate electricity consumption. Although the remote method is named `PressPowerButton`, its current implementation always calls `TurnOn()` rather than toggling the device state. The air conditioner also implements `TurnOff()`, which is not called in the demonstration.

## Application Structure

The source code is located in [Program.cs](TMPSLab1/Program.cs).

| Component | Responsibility |
| --- | --- |
| `IPowerConsumer` | Defines a device name and the `ConsumePower()` operation. |
| `IRemoteControllable` | Defines the `TurnOn()` and `TurnOff()` operations. |
| `Lamp` | Implements power consumption by displaying a lighting message. |
| `AirConditioner` | Implements power consumption and remote control, maintaining its enabled state. |
| `PowerSocket` | Stores connected power consumers and calls their `ConsumePower()` methods. |
| `RemoteControl` | Sends a turn-on command to a remotely controllable device. |
| `Program` | Creates the objects and runs the demonstration. |

## Application of SOLID Principles

### S — Single Responsibility Principle

A class should have one reason to change.

The application separates its main responsibilities: `PowerSocket` manages connected consumers and supplies power, `RemoteControl` sends a control command, and the device classes define device-specific behavior. `Program` assembles these objects and defines the demonstration sequence.

This separation allows the remote-control logic to change independently of the socket logic. In this small example, the classes also write directly to the console. A larger application could separate output formatting from device behavior to make the responsibilities more focused.

### O — Open/Closed Principle

Software components should be open for extension but closed for modification.

`PowerSocket` works with `IPowerConsumer` rather than a fixed list of concrete device types. To add a television, a developer can create a `Television` class implementing `IPowerConsumer` and connect an instance through `PlugIn()`. The socket implementation does not need to change; only the object creation and setup code needs to include the new device.

Likewise, another device implementing `IRemoteControllable` can be passed to the existing remote control.

### L — Liskov Substitution Principle

Implementations should be usable through their abstraction without breaking the behavior expected by its clients.

Both `Lamp` and `AirConditioner` can be passed to `PowerSocket.PlugIn()` as `IPowerConsumer` objects. During `SupplyPower()`, the socket calls `ConsumePower()` on every connected device without type checks or special cases.

The devices produce different output, but both support the operation expected by the socket. The air conditioner's standby behavior is compatible with this contract because the interface does not require every powered device to be actively operating.

### I — Interface Segregation Principle

Clients should not be forced to depend on operations they do not use.

Power consumption and remote control are represented by two separate interfaces:

- `IPowerConsumer` provides `Name` and `ConsumePower()`.
- `IRemoteControllable` provides `TurnOn()` and `TurnOff()`.

`Lamp` implements only `IPowerConsumer`, so it does not need empty or unsupported remote-control methods. `AirConditioner` implements both interfaces because it supports both capabilities. The socket only requires the power-consumption interface, while the remote only requires the remote-control interface.

### D — Dependency Inversion Principle

High-level behavior should depend on abstractions rather than concrete implementations.

`PowerSocket` stores a `List<IPowerConsumer>` and receives devices through `PlugIn(IPowerConsumer device)`. `RemoteControl` receives an `IRemoteControllable` through `PressPowerButton()`.

Neither class creates a `Lamp` or an `AirConditioner`, and neither needs to know their concrete types. `Program.Main()` creates the concrete objects and passes them to the appropriate components. This makes it possible to replace or extend the devices while preserving the socket and remote implementations.

## Running the Application

Install the .NET 10 SDK, then run the following command from the repository root:

```powershell
dotnet run --project TMPSLab1/TMPSLab1.csproj
```

## Console Output

```text
-> Plugged into the socket: Desk lamp
-> Plugged into the socket: Smart air conditioner

[Power socket]: Supplying power to connected devices...
[Desk lamp]: Emits bright, warm light 💡
[Smart air conditioner]: In standby mode (power is supplied)...

[Remote control]: Power button pressed...
[Smart air conditioner]: Turned on using the remote control.

[Power socket]: Supplying power to connected devices...
[Desk lamp]: Emits bright, warm light 💡
[Smart air conditioner]: Cools the room to 20°C ❄️
```

The application was successfully run using `dotnet run`. The output demonstrates that both devices receive power through the same interface and that the remote command changes the air conditioner's subsequent behavior.

## Conclusion

The application demonstrates all five SOLID principles through a small device simulation. Separate interfaces describe independent capabilities, and the socket and remote operate through those abstractions. New compatible devices can therefore be introduced without changing the existing socket or remote classes.
