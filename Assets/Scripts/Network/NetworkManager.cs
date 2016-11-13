using UnityEngine;
using UdpKit;
using Bolt;

[BoltGlobalBehaviour] public class NetworkManager : GlobalEventListener  
{
	public delegate void NetworkingConnecting ();
	public delegate void NetworkingConnected ();
	public delegate void NetworkingPlayerConnected (BoltConnection connection);
	public delegate void NetworkingPlayerDisconnected (BoltConnection connection);
	public delegate void NetworkingError (NetworkError error);
	public delegate void NetworkingLobbyListUpdated ();
	public delegate void NetworkingSceneLoadedLoacally (string scene);
	public delegate void NetworkingSceneLoadedRemote (BoltConnection connection);
	public static event NetworkingError OnNetworkingError;
	public static event NetworkingConnecting OnNetworkingConnecting;
	public static event NetworkingPlayerConnected OnNetworkingPlayerConnected;
	public static event NetworkingPlayerDisconnected OnNetworkingPlayerDisconnected;
	public static event NetworkingConnected OnNetworkingConnected;
	public static event NetworkingLobbyListUpdated OnNetworkingLobbyListUpdated;
	public static event NetworkingSceneLoadedLoacally OnSceneLoadedLocally;
	public static event NetworkingSceneLoadedRemote OnSceneLoadedRemote;

	
	
	public static void StartNetwork ()
	{
		if (BoltNetwork.isRunning)
		{			
			OnNetworkingError(new NetworkError("Network Start-up Failure", "A connection is already established.", ":("));
		}
		else
		{
			Debug.Log("Starting Client");
			BoltLauncher.StartClient();
		}
	}

	// Host a server
	public static void HostLobby (LobbySettings settings)
	{
		// If already connected, Bolt needs to be shutdown before joining a server
		if (IsHost || IsClient)
		{
			Debug.Log("Shutting down client...");
			BoltLauncher.Shutdown();
		}
		
		Debug.Log("...Restarting as Host");		

		BoltConfig lobbySettings = new BoltConfig();
		lobbySettings.useNetworkSimulation = settings.DEVSimulateNetworkCondition;
		lobbySettings.simulatedPingMean = settings.DEVPingMean;
		lobbySettings.simulatedPingJitter = settings.DEVPingJitter;
		lobbySettings.serverConnectionAcceptMode = BoltConnectionAcceptMode.Auto;

		BoltLauncher.StartServer(UdpEndPoint.Parse("0.0.0.0:27000"), lobbySettings);
	}

	// Join an existing Lobby
	public static void JoinLobby (UdpSession session)
	{
		if (IsClient)
		{
			Debug.Log("Connecting to session...: " + session.HostName);

			try
			{
				BoltNetwork.Connect(session);
			}
			catch (System.Exception e)
			{
				string error = "Caught a System Exception when connecting to " + session.HostName + ". Message: " + e.Message;
				OnNetworkingError(new NetworkError("Join Lobby Failure", error, "Maybe this Lobby no longer exists"));
			}
		}
		else
		{
			OnNetworkingError(new NetworkError("Client Failure", "The user is not a Bolt Client", "Try reconnecting"));
			BoltLauncher.Shutdown();
		}
	}

	public static void UpdateServerList ()
	{
		try
		{
			Debug.Log("Requesting Session List...");

			Bolt.Zeus.RequestSessionList();
		}
		catch (System.Exception e)
		{
			string error = "Caught a System Exception when requesting session list. Message: " + e.Message;
			OnNetworkingError(new NetworkError("List Update Failure", error, ":("));
		}
	}

	public static void RegisterServerToMaster ()
	{
		try 
		{
			BoltConsole.Write("Registering Lobby");

			BoltNetwork.SetHostInfo("Test Lobby"/*token.Name, token*/, null);
		}
		catch (System.Exception e) 
		{
			string error = "Caught a System Exception when registering Server. Message: " + e.Message;
			OnNetworkingError(new NetworkError("Lobby Registration Failure", error, ":("));
		}
	}

	public static void LoadScene (string scene)
	{
		BoltConsole.Write("LoadScene", Color.red);
		BoltNetwork.LoadScene(scene);
	}

	// Callback. Called on Clients when connected to a server
	// And called on Host when a Client joins 
    public override void Connected (BoltConnection connection) 
    {
		if (NetworkManager.IsHost)
		{
			Debug.Log("Player connected: " + connection);

			//OnPlayerConnectedEvent(connection);
		}
		else
			Debug.Log("Connection established!");

			OnNetworkingPlayerConnected(connection);
    }

    // Callback. Called on Clients and Host once a player disconnects from a server
	public override void Disconnected (BoltConnection connection) 
	{
		if (NetworkManager.IsHost)
			Debug.Log("Connection disconnected: " + connection);
		else
			Debug.Log("Disconnected from the Server");

		OnNetworkingPlayerDisconnected(connection);
	}

	// Move something like this to a seperate file where they are needed constantly. Like a NetworkGameManager
	public override void SceneLoadLocalDone (string scene)
	{
		OnSceneLoadedLocally(scene);
	}

	public override void SceneLoadRemoteDone (BoltConnection connection)
	{
		OnSceneLoadedRemote(connection);
	}

	public override void SessionListUpdated (Map<System.Guid, UdpSession> sessionList)
	{
		BoltConsole.Write("SessionListUpdated", Color.red);
		OnNetworkingLobbyListUpdated();
	}

	public override void BoltStartFailed ()
	{
		OnNetworkingError(new NetworkError("Network Start-up Failure", "Networking failed to start correctly", ":("));
	}

	public override void BoltStartBegin ()
	{
		OnNetworkingConnecting();
	}

  	public override void BoltStartDone ()
  	{
    	//BoltNetwork.RegisterTokenClass<LobbyToken>();

		OnNetworkingConnected();		

		if (IsHost)
		{
			RegisterServerToMaster();
			BoltNetwork.LoadScene("Lobby");
		}
  	}

	public override void ZeusConnected (UdpEndPoint endpoint)
	{
		// For some reason Zeus will connect even when you hosted/connected to a server...
		// Don't do anything if we are connected to one

		if (!InLobby)
			UpdateServerList();
	}

	public override void SessionConnectFailed (UdpSession session, Bolt.IProtocolToken token)
	{
		OnNetworkingError(new NetworkError("Lobby Connect Failed", "No clue :)", ":("));
	}

	public static bool InLobby 
	{
		get { return BoltNetwork.isConnected; }
	}

	public static bool IsClient 
	{
		get { return BoltNetwork.isClient; }
	}

	public static bool IsHost 
	{
		get { return BoltNetwork.isServer; }
	}

	public static bool IsConnectedZeus 
	{
		get { return IsClient && Bolt.Zeus.IsConnected; }
	}
}
