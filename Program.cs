using System;
using System.Collections.Generic;

namespace PowerSocketSolidApp
{
    public interface IPowerConsumer
    {
        string Name { get; }
        void ConsumePower();
    }
    public interface IRemoteControllable
    {
        void TurnOn();
        void TurnOff();
    }
    public class Lamp : IPowerConsumer
    {
        public string Name => "Desk lamp";

        public void ConsumePower()
        {
            Console.WriteLine($"[{Name}]: Emits bright, warm light 💡");
        }
    }
    public class AirConditioner : IPowerConsumer, IRemoteControllable
    {
        public string Name => "Smart air conditioner";
        private bool _isEnabled = false;

        public void ConsumePower()
        {
            if (_isEnabled)
            {
                Console.WriteLine($"[{Name}]: Cools the room to 20°C ❄️");
            }
            else
            {
                Console.WriteLine($"[{Name}]: In standby mode (power is supplied)...");
            }
        }

        public void TurnOn()
        {
            _isEnabled = true;
            Console.WriteLine($"[{Name}]: Turned on using the remote control.");
        }

        public void TurnOff()
        {
            _isEnabled = false;
            Console.WriteLine($"[{Name}]: Turned off using the remote control.");
        }
    }
    public class PowerSocket
    {
        private readonly List<IPowerConsumer> _connectedDevices = new List<IPowerConsumer>();

        public void PlugIn(IPowerConsumer device)
        {
            _connectedDevices.Add(device);
            Console.WriteLine($"-> Plugged into the socket: {device.Name}");
        }

        public void SupplyPower()
        {
            Console.WriteLine("\n[Power socket]: Supplying power to connected devices...");
            foreach (var device in _connectedDevices)
            {
                device.ConsumePower();
            }
        }
    }
    public class RemoteControl
    {
        public void PressPowerButton(IRemoteControllable device)
        {
            Console.WriteLine("\n[Remote control]: Power button pressed...");
            device.TurnOn();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            PowerSocket wallSocket = new PowerSocket();
            RemoteControl remote = new RemoteControl();
            Lamp deskLamp = new Lamp();
            AirConditioner conditioner = new AirConditioner();
            wallSocket.PlugIn(deskLamp);
            wallSocket.PlugIn(conditioner);
            wallSocket.SupplyPower();
            remote.PressPowerButton(conditioner);
            wallSocket.SupplyPower();
        }
    }
}