namespace GamePatternsLab;

internal sealed class CharacterBuilder
{
    private string? _name;
    private string? _role;
    private string? _weapon;
    private int _health;
    private readonly List<string> _abilities = new();

    public CharacterBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public CharacterBuilder SetRole(string role)
    {
        _role = role;
        return this;
    }

    public CharacterBuilder SetWeapon(string weapon)
    {
        _weapon = weapon;
        return this;
    }

    public CharacterBuilder SetHealth(int health)
    {
        _health = health;
        return this;
    }

    public CharacterBuilder AddAbility(string ability)
    {
        _abilities.Add(ability);
        return this;
    }

    public GameCharacter Build()
    {
        if (string.IsNullOrWhiteSpace(_name) ||
            string.IsNullOrWhiteSpace(_role) ||
            string.IsNullOrWhiteSpace(_weapon))
        {
            throw new InvalidOperationException("Name, role, and weapon must be specified.");
        }

        if (_health <= 0)
        {
            throw new InvalidOperationException("Health must be greater than zero.");
        }

        return new GameCharacter
        {
            Name = _name,
            Role = _role,
            Weapon = _weapon,
            Health = _health,
            Abilities = new List<string>(_abilities)
        };
    }
}