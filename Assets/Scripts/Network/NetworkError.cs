public class NetworkError 
{
	public NetworkError (string name, string cause, string tip)
	{
		this.Name = name;
		this.Cause = cause;
		this.Tip = tip;

		UnityEngine.Debug.LogError("Error: " + name + " - " + cause + " - " + tip);
		BoltConsole.Write("Error: " + name + " - " + cause + " - " + tip, UnityEngine.Color.red);
	}

	public string Name { get; set; }
	public string Cause { get; set; }
	public string Tip { get; set; }
}
