using UnityEngine;

namespace Player
{
	public class PlayerState : MonoBehaviour 
	{
		public enum State {Normal, InAir, InVehicle, IsCrouched, IsDead}
		public State CurrentState;

		public enum BattleStatus {Normal, Firing, UnderAttack, Recharging}
		public BattleStatus CurrentBattleStatus;

		public string Name
		{
			get { return gameObject.name; }
			set { gameObject.name = value; }
		}

		public bool IsAlive
		{
			get { return !(CurrentState == State.IsDead); }
			set { CurrentState = State.IsDead; }
		}

		public bool InVehicle
		{
			get { return CurrentState == State.InVehicle; }
			set { CurrentState = State.InVehicle; }
		}

		public bool IsCrouched
		{
			get { return CurrentState == State.IsCrouched; }
			set { CurrentState = State.IsCrouched; }
		}

		public float Health
		{
			get { return 100; }
			set { CurrentState = State.InVehicle; }
		}

		public float Shield
		{
			get { return 100; }
			set { CurrentState = State.InVehicle; }
		}

		public BattleStatus Status
		{
			get { return CurrentBattleStatus; }
			set { CurrentBattleStatus = value; }
		}

		public bool IsMine 
		{
			get 
			{
				return GetComponent<BoltEntity>().isControllerOrOwner;
			}
		}
	}
}

