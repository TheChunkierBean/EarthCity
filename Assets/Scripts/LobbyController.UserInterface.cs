using UnityEngine;
using UnityEngine.UI;

public partial class LobbyController : MonoBehaviour 
{
	public Text playerCount;
	public void Update ()
	{
		int count = 0;
		foreach (var s in BoltNetwork.clients)
		{
			count++;
		}

		playerCount.text = "Player Count: " + count; 
	}
}
