using UnityEngine;

public partial class LobbyController : MonoBehaviour 
{
	public void LoadDebugScene ()
	{
		NetworkManager.LoadScene("Debug");
	}
}
