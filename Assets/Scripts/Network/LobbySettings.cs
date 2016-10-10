/// <summary>
/// An object containing all the relevant settings a custom lobby can manipulate
/// </summary>

public class LobbySettings 
{
	int _playerCount;
	public int PlayerCount 
	{
		get { return _playerCount; }
		set {_playerCount = value; }
	}

	string _name;
	public string Name 
	{
		get { return _name; }
		set { _name = value; }
	}

	// Modifiers like Difficulty, skulls, etc.
}
