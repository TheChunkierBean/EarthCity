using UnityEngine;

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

	public static void OnWeaponChanged (Weapon primary, Weapon secondary)
	{
		playerHUD.OnWeaponChanged(primary, secondary);
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
