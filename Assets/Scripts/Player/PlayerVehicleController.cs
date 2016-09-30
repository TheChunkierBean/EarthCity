using UnityEngine;
using System.Collections;

//<summary> 
// 
//</summary> 

namespace Player
{
	public class PlayerVehicleController : MonoBehaviour 
	{
		PlayerController controller;

		public void Initialize (PlayerController pController)
		{
			controller = pController;
		}

		public void UpdateVehicleState (PlayerInput.Input input)
		{

		}
	}
}