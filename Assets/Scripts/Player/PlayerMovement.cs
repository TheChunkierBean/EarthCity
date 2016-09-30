using UnityEngine;
using System.Collections;

//<summary> 
// 
//</summary> 

namespace Player 
{
	public class PlayerMovement : MonoBehaviour 
	{
		PlayerController controller;

		public void Initialize (PlayerController pController)
		{
			controller = pController;
		}
		
		public void UpdateMovementState (PlayerState state, PlayerInput.Input input)
		{
			if (input.Vertical > 0)
			{
				// Walk forward
				if (state.IsCrouched)
				{
					// Walk even slower
				}
			}
		}
	}
}
