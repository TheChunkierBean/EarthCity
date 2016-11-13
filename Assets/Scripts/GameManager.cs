using UnityEngine;
using Bolt;
using System.Collections.Generic;
using System.Linq;

public class GameManager : EntityBehaviour<IGameState>
{
	public List<PlayerContainer> players = new List<PlayerContainer>();
	public BoltEntity player;
	//[BoltGlobalBehaviour(BoltNetworkModes.Host)] 

	public override void Attached ()
	{
		NetworkManager.OnNetworkingPlayerConnected += PlayerConnected;
		NetworkManager.OnNetworkingPlayerDisconnected += PlayerDisconnected;
		NetworkManager.OnSceneLoadedLocally += OnSceneLoadedLocally;
		NetworkManager.OnSceneLoadedRemote += OnSceneLoadedRemote;

		if (NetworkManager.IsHost)
			PlayerConnected(null);
	}

	void OnGUI ()
	{
		if (entity.isAttached)
		{
			int i = 0;
			foreach (var a in state.GameRoster)
			{
				GUI.Label(new Rect(500, 500 + 30 * i, 200, 30), a.Name);
				i++;
			}
		}
	}

	private void PlayerConnected (BoltConnection connection)
	{
		BoltConsole.Write("PlayerConnected", Color.yellow);
		
		PlayerContainer PC = new PlayerContainer(Random.Range(1000000, 9999999).ToString(), connection);

		players.Add(PC);

		RebuildRosterList();
	}

	private void PlayerDisconnected (BoltConnection connection)
	{
		BoltConsole.Write("PlayerDisconnected", Color.yellow);

		PlayerContainer PC = players.First(x => x.Connection == connection);
		PC.InGame = false;

		if (PC.HasPlayer)
			BoltNetwork.Destroy(PC.Player);

		RebuildRosterList();
	}

	private void OnSceneLoadedLocally (string scene)
	{
		BoltConsole.Write("OnSceneLoadedLocally", Color.yellow);

		if (scene == "Debug")
		{
			BoltNetwork.Instantiate(player);
		}
	}

	private void OnSceneLoadedRemote (BoltConnection connection)
	{
		BoltConsole.Write("OnSceneLoadedRemote", Color.yellow);
	}

	private void RemoveDisconnectedPlayerContainers ()
	{		
		BoltConsole.Write("RemoveDisconnectedPlayerContainers", Color.yellow);

		players.RemoveAll(x => x.InGame == false);

		RebuildRosterList();
	}

	private void RebuildRosterList ()
	{
		for (int i = 0; i < players.Count; i++)
		{
			state.GameRoster[i].Name = players[i].Name;
			state.GameRoster[i].InGame = players[i].InGame;
		}
	}
}






/*void OnMatchEnded ()
{
	// Clear PlayerContainers // Only clear here because people might come back again after they have quit. 
	// Therefore don't destroy their information
}*/