using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour 
{
	public Image primaryWeapon;
	public Image secondaryWeapon;
	public Image crosshair;

	public void OnWeaponFired (Weapon weapon)
	{

	}

	public void OnWeaponAimed (Weapon weapon)
	{

	}

	public void OnWeaponReloaded (Weapon weapon)
	{

	}

	public void OnWeaponChanged (Weapon primary, Weapon secondary)
	{
		primaryWeapon.sprite = primary.HUD.thumbnail;
		secondaryWeapon.sprite = secondary.HUD.thumbnail;

		crosshair.sprite = primary.HUD.crosshair;
	}
}
