using UnityEngine;

//<summary> 
// 
//</summary> 

namespace Player
{
	public class PlayerInput
	{
		public struct Input
		{
			public float Vertical; // Forward, backward
			public float Horizontal;	//Left, right

			public float VerticalLook;
			public float HorizontalLook;

			public bool Jump;
			public bool Reload;
			public bool Usage; // Pickup weapon, mount turret, mount warthog, etc.
			public bool Shift;	// Shift weapon
			public bool Melee;
			public bool Shoot;
			public bool Aim;
			public bool Crouch;
		}

		// Input type
		// Input settings (sensitivity, deadzones, etc. )
		// Button remapping?

		public static Input GetInput ()
		{
			Input i = default(Input);

			i.Vertical = UnityEngine.Input.GetAxis("Vertical");
			i.Horizontal = UnityEngine.Input.GetAxis("Horizontal");
			i.VerticalLook = UnityEngine.Input.GetAxisRaw("Mouse X");
			i.HorizontalLook = UnityEngine.Input.GetAxisRaw("Mouse Y");
			
			i.Jump = UnityEngine.Input.GetButtonDown("Jump");
			i.Reload = UnityEngine.Input.GetKeyDown(KeyCode.R);
			//i.usage = UnityEngine.Input.Get
			i.Shift = UnityEngine.Input.GetKeyDown(KeyCode.Q);
			i.Melee = UnityEngine.Input.GetKeyDown(KeyCode.F);
			i.Shoot = UnityEngine.Input.GetButton("Fire1");
			i.Aim = UnityEngine.Input.GetButtonDown("Fire2");
			//i.Crouch = UnityEngine.Input.Get

			return i; 
		}
	}
}