using UnityEngine;
using UnityEngine.UI;
using UdpKit;
using System.Collections.Generic;

public partial class MainMenuController : MonoBehaviour
{
	[System.Serializable] public class LobbyConfiguration
	{
		public Text PlayerCountText;
		[HideInInspector] public LobbySettings Settings = new LobbySettings();
	}

	public Text UIPath;

	public Transform lobbyListContent;
	public Transform lobbyElement;

	public GameObject InitializationScreen;

	public UINetworkError networkErrorScreen;

	public LobbyConfiguration LobbyConfig;

	public void OnNetworkError (NetworkError error)
	{
		networkErrorScreen.ApplyNetworkError(error);
	}

	private List<UdpSession> _lobbyList = new List<UdpSession>();
	private List<GameObject> _lobbyListElements = new List<GameObject>();

	private void OnNetworkUpdateLobbyList ()
	{	
		foreach(GameObject g in _lobbyListElements)
		{
			Destroy(g);
		}

		_lobbyListElements.Clear();
		_lobbyList.Clear();

		foreach (var session in BoltNetwork.SessionList)
			_lobbyList.Add(session.Value);

		for (int i = 0; i < _lobbyList.Count; i++)
		{
			var lobby = _lobbyList[i];

			RectTransform element = Instantiate(lobbyElement, Vector3.zero, Quaternion.identity) as RectTransform;
			_lobbyListElements.Add(element.gameObject);
			element.gameObject.GetComponent<UILobbyElement>().Lobby = lobby;
			
			element.SetParent(lobbyListContent, false);

			lobbyListContent.GetComponent<RectTransform>().sizeDelta = new Vector2(lobbyListContent.GetComponent<RectTransform>().sizeDelta.x, 50 * 26);

			element.localPosition = new Vector3(0, 0 - 50 * i + 5, 0);
		}
	}

	private void OnNetworkConnecting ()
	{
		BoltConsole.Write("OnNetworkConnecting", Color.red);
		InitializationScreen.SetActive(true);
	}

	private void OnNetworkConnected ()
	{
		BoltConsole.Write("OnNetworkConnected", Color.red);
		InitializationScreen.SetActive(false);
	}

	public void SwitchToMenuState ()
	{
		SetNextState(MenuState.Main);
	}

	public void SwitchToMultiplayerState ()
	{
		SetNextState(MenuState.Multiplayer);	
	}

	public void SwitchToSettingsState ()
	{
		SetNextState(MenuState.Settings);	
	}

	public void SwitchToConfiguringLobbyState ()
	{
		SetNextState(MenuState.ConfiguringLobby);	
	}

	public void SwitchToQuitState ()
	{
		SetNextState(MenuState.Quit);	
	}

	public void HostLobby ()
	{
		Debug.Log("UI - Host Lobby Button");
		InitializationScreen.SetActive(true);
		NetworkManager.HostLobby(LobbyConfig.Settings);
	}

	public void UpdateLobbyList ()
	{
		NetworkManager.UpdateServerList();
	}

	public void SetLobbyPlayerCount (float count)
	{
		Debug.Log("UI - Set Player Count Value: " + count);

		LobbyConfig.PlayerCountText.text = count.ToString();
		LobbyConfig.Settings.PlayerCount = (int)count;
	}

	public void SetLobbyPrivacy (bool isPrivate)
	{
		Debug.Log("UI - Set Privacy Value " + isPrivate);
		LobbyConfig.Settings.IsPrivate = isPrivate;
	}

	public void DEVSimulateNetworkConditions (bool isSimulating)
	{
		Debug.Log("UI - DEV SNC Value " + isSimulating);
		LobbyConfig.Settings.DEVSimulateNetworkCondition = isSimulating;
	}

	public void DEVSetPingMean (int pingMean)
	{
		Debug.Log("UI - DEV Set Ping Mean Value " + pingMean);
		LobbyConfig.Settings.DEVPingMean = pingMean;
	}

	public void DEVSetPingJitter (int pingJitter)
	{
		Debug.Log("UI - DEV Set Ping Jitter Value " + pingJitter);
		LobbyConfig.Settings.DEVPingJitter = pingJitter;
	}
}
