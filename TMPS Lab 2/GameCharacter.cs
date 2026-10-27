namespace GamePatternsLab;

internal sealed class GameCharacter
{
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Weapon { get; set; } = string.Empty;
    public int Health { get; set; }
    public List<string> Abilities { get; set; } = new();

    public GameCharacter Clone(string newName)
    {
        var clone = (GameCharacter)MemberwiseClone();
        clone.Name = newName;
        clone.Abilities = new List<string>(Abilities);
        return clone;
    }

    public override string ToString()
    {
        return $"{Name} | {Role} | HP: {Health} | Weapon: {Weapon} | Abilities: {string.Join(", ", Abilities)}";
    }
}