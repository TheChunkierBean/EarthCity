using UnityEngine;
using UnityEngine.UI;

namespace Player
{
	public class PlayerHUD : MonoBehaviour 
	{
		public Image primaryWeapon;
		public Text primaryWeaponText;
		public Image secondaryWeapon;
		public Image crosshair;
		public Image shield;
		public Image scope;

		public void OnWeaponFired (Weapon weapon)
		{
			primaryWeaponText.text = weapon.ammo + "/" + weapon.spareAmmo;
		}

		public void OnWeaponAimed (Weapon weapon)
		{
			scope.gameObject.SetActive(weapon.IsAiming);
		}

		public void OnWeaponReloaded (Weapon weapon)
		{
			//
		}

		public void OnWeaponChanged (PlayerInventory inventory)
		{
			primaryWeapon.sprite = inventory.PrimaryWeapon.HUD.thumbnail;
			secondaryWeapon.sprite = inventory.SecondaryWeapon.HUD.thumbnail;

			crosshair.sprite = inventory.PrimaryWeapon.HUD.crosshair;
		}

		public void OnShieldChanged (float value)
		{
			shield.fillAmount = 1.0F / 100.0F * value;
		}

		public void OnHealthChanged (float value)
		{

		}

		public void OnHitboxHitChanged (bool isHit)
		{
			if (isHit)
				crosshair.color = Color.red;
			else
				crosshair.color = Color.white;
		}
	}
}