namespace GamePatternsLab;

internal sealed class GameWorld
{
    private static readonly Lazy<GameWorld> LazyInstance = new(() => new GameWorld());
    private readonly List<GameCharacter> _characters = new();

    private GameWorld()
    {
    }

    public static GameWorld Instance => LazyInstance.Value;

    public void AddCharacter(GameCharacter character) => _characters.Add(character);

    public void PrintCharacters()
    {
        foreach (var character in _characters)
        {
            Console.WriteLine($"- {character.Name} ({character.Role})");
        }
    }
}