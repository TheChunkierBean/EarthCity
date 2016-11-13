using UnityEngine;
using UnityEngine.UI;
using UdpKit;

public class UILobbyElement : MonoBehaviour 
{
	public Text lobbyName;

	private UdpSession _lobby;
	public UdpSession Lobby 
	{ 
		get { return _lobby; }
		set
		{
			_lobby = value;
			lobbyName.text = value.HostName;
		} 
	}

	public void JoinLobby ()
	{
		NetworkManager.JoinLobby(Lobby);
	}
}
