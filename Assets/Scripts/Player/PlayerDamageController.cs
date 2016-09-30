using UnityEngine;
using System.Collections;

namespace Player
{
	public class PlayerDamageController : MonoBehaviour 
	{
		PlayerController controller;

		public void Initialize (PlayerController pController)
		{
			controller = pController;
		}	
	}
}

