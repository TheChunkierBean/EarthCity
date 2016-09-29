using UnityEngine;
using System.Collections;

//<summary> 
// 
//</summary> 

namespace Player 
{
	public class PlayerMovement : MonoBehaviour 
	{
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
