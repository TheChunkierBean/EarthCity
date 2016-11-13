using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public partial class MainMenuController : MonoBehaviour 
{
	public enum MenuState {TitleScreen, Main, Multiplayer, Settings, Quit, ConfiguringLobby}
	public MenuState state;

	[System.Serializable] public class UIState
	{
		public string Name;
		public CanvasGroup Canvas;
		public MenuState State;
	}

	public List<UIState> UIStates = new List<UIState>();

	private Stack<UIState> _UIScreenStack = new Stack<UIState>();

	private string _UIPath;

	private UIState GetCurrentScreen
	{
		get { return UIStates.First(x => x.State == state); }
	}

	private UIState GetPreviousScreen
	{
		get { return _UIScreenStack.Pop(); }
	}

	private void Awake ()
	{
		NetworkManager.OnNetworkingConnecting += OnNetworkConnecting;
		NetworkManager.OnNetworkingConnected += OnNetworkConnected;
		NetworkManager.OnNetworkingError += OnNetworkError;
		NetworkManager.OnNetworkingLobbyListUpdated += OnNetworkUpdateLobbyList;

		_UIScreenStack.Push(UIStates.First(x => x.State == state));
	}

	private void OnGUI ()
	{
		_UIPath = "";

		for (int i = _UIScreenStack.Count-1; i >= 0; i--)
		{
			UIState UIS = _UIScreenStack.ElementAt(i);

			if (UIS.State == MenuState.TitleScreen)
				continue;

			_UIPath += " // " + UIS.Name;
		}

		UIPath.text = _UIPath;
	}

	private void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			SetPreviousState();
		}
	}

	public void SetPreviousState ()
	{
		if (CanSetPreviousState)
		{
			// Deactivate the previous 
			GetPreviousScreen.Canvas.gameObject.SetActive(false);

			// Set the current State
			state = _UIScreenStack.Peek().State;

			// Activate the current screen
			GetCurrentScreen.Canvas.gameObject.SetActive(true);
		}
	}

	private bool CanSetPreviousState 
	{
		get { return _UIScreenStack.Count > 2; }
	}

	private void SetNextState (MenuState newState)
	{
		// Deactivate the current screen
		GetCurrentScreen.Canvas.gameObject.SetActive(false);

		// Set the new state
		state = newState;

		// Activate the current scrren
		GetCurrentScreen.Canvas.gameObject.SetActive(true);
		
		// Add the current screen to the stack
		_UIScreenStack.Push(GetCurrentScreen);
	}
}
