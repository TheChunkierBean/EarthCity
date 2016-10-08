using UnityEngine;
using System.Collections;

//<summary> 
// 
//</summary> 

namespace Player 
{
	public class PlayerMovement : MonoBehaviour 
	{	
		public float speed = 10.0f;
		public float gravity = 10.0f;
		public float maxVelocityChange = 10.0f;
		public bool canJump = true;
		public float jumpHeight = 2.0f;
		private bool grounded = false;

		void Awake()
		{
			GetComponent<Rigidbody>().freezeRotation = true;
			GetComponent<Rigidbody>().useGravity = false;
		}


		public void UpdateMovementState (PlayerState state, PlayerInput.Input input)
		{
			//Debug.Log("Moving. Vertical: " + input.Vertical + " Horizontal: " + input.Horizontal + "state: " + state.IsCrouched);

			if (grounded)
			{
				// Calculate how fast we should be moving
				Vector3 targetVelocity = new Vector3(input.Horizontal, 0, input.Vertical);
				targetVelocity = transform.TransformDirection(targetVelocity);
				targetVelocity *= speed;

				// Apply a force that attempts to reach our target velocity
				Vector3 velocity = GetComponent<Rigidbody>().velocity;
				Vector3 velocityChange = (targetVelocity - velocity);
				velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
				velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
				velocityChange.y = 0;
				GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);

				// Jump
				if (canJump && input.Jump)
				{
					GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
				}
			}

			// We apply gravity manually for more tuning control
			GetComponent<Rigidbody>().AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));

			grounded = false;
		}

		void OnCollisionStay ()
		{
			grounded = true;
		}

		float CalculateJumpVerticalSpeed ()
		{
			// From the jump height and gravity we deduce the upwards speed
			// for the character to reach at the apex.
			return Mathf.Sqrt(2 * jumpHeight * gravity);
		}
	}
}
