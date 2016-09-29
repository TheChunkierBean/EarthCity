﻿using UnityEngine;

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

			i.Jump = UnityEngine.Input.GetButton("Jump");
			//i.reload = UnityEngine.Input.Get
			//i.usage = UnityEngine.Input.Get
			i.Shift = UnityEngine.Input.GetKey(KeyCode.Tab);
			i.Melee = UnityEngine.Input.GetKey(KeyCode.F);
			i.Shoot = UnityEngine.Input.GetButton("Fire1");
			i.Aim = UnityEngine.Input.GetButton("Fire2");
			//i.Crouch = UnityEngine.Input.Get

			return i; 
		}
	}
}