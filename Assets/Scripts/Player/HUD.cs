﻿using UnityEngine;

public class HUD : MonoBehaviour
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
}