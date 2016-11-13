[System.Serializable] public class PlayerContainer 
{
	public PlayerContainer (string name, BoltConnection connection)
	{
		this.Name = name;
		this.Connection = connection;
	}

	public string Name { get; set; }
	public BoltConnection Connection { get; set; }
	public BoltEntity Player { get; set; }
	public bool IsHost { get { return Connection == null; } }
	public bool HasPlayer { get { return Player != null; } }
	public bool InGame { get; set; } // Is the player still in the game?
}
