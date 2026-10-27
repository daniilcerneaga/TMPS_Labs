namespace GamePatternsLab;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== RPG: Creational Patterns Demo ===\n");

        var world = GameWorld.Instance;
        var sameWorld = GameWorld.Instance;
        Console.WriteLine($"[Singleton] One game world: {ReferenceEquals(world, sameWorld)}\n");

        var hero = new CharacterBuilder()
            .SetName("Aria")
            .SetRole("Knight")
            .SetWeapon("Moonsteel sword")
            .SetHealth(120)
            .AddAbility("Shield Bash")
            .AddAbility("Second Wind")
            .Build();

        world.AddCharacter(hero);
        Console.WriteLine("[Builder] Hero assembled step by step:");
        Console.WriteLine(hero);
        Console.WriteLine();

        var goblinPrototype = new GameCharacter
        {
            Name = "Goblin Prototype",
            Role = "Enemy / Scout",
            Weapon = "Rusty dagger",
            Health = 35,
            Abilities = new List<string> { "Quick stab", "Dodge" }
        };

        var goblinOne = goblinPrototype.Clone("Goblin #1");
        var goblinTwo = goblinPrototype.Clone("Goblin #2");
        goblinOne.Abilities.Add("Battle cry");
        world.AddCharacter(goblinOne);
        world.AddCharacter(goblinTwo);

        Console.WriteLine("[Prototype] Two enemies cloned from one template:");
        Console.WriteLine(goblinOne);
        Console.WriteLine(goblinTwo);
        Console.WriteLine($"Prototype abilities remain unchanged: {string.Join(", ", goblinPrototype.Abilities)}\n");

        Console.WriteLine("[Singleton] Characters in the shared game world:");
        world.PrintCharacters();
    }
}
