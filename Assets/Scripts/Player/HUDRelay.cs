using UnityEngine;

namespace Player
{
	public class HUDRelay : MonoBehaviour
	{
		public PlayerHUD playerHUDPrefab;
		private static PlayerHUD playerHUD;

		public void Initialize ()
		{
			playerHUD = Instantiate(playerHUDPrefab).GetComponent<PlayerHUD>();
		}

		public static void OnWeaponFired (Weapon weapon)
		{
			playerHUD.OnWeaponFired(weapon);
		}

		public static void OnWeaponAimed (Weapon weapon)
		{
			playerHUD.OnWeaponAimed(weapon);
		}

		public static void OnWeaponReloaded (Weapon weapon)
		{
			playerHUD.OnWeaponReloaded(weapon);
		}

		public static void OnWeaponChanged (Player.PlayerInventory inventory)
		{
			playerHUD.OnWeaponChanged(inventory);
		}

		public static void OnShieldChanged (float shield)
		{
			playerHUD.OnShieldChanged(shield);
		}

		public static void OnHealthChanged (float health)
		{
			playerHUD.OnHealthChanged(health);
		}

		public static void OnHitboxHitChanged (bool isHit)
		{
			playerHUD.OnHitboxHitChanged(isHit);
		}
	}
}