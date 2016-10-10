using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Camera cam;
    public int range = 10000;
    public int ammo = 10;
    public int spareAmmo = 180;
    public int clipSize = 10;

    [System.Serializable]
    public struct Sounds
    {
        public AudioClip[] fire;
        public AudioClip[] melee;
        public AudioClip[] reload;
        public AudioClip[] equip;
    }
    public Sounds sound;

    [System.Serializable]
    public struct HUDElements
    {
        public Sprite crosshair;
        public Sprite thumbnail;
        public Sprite ammo;
        public Sprite scope;
    }
    public HUDElements HUD;

    public float recoilPower = 30;

    public Animation am;
    public AnimationClip shoot;
    public AnimationClip reloadA;

    public animManager amM;

    #region Events

        public delegate void WeaponEvent ();
        public WeaponEvent OnWeaponFiredEvent;
        public WeaponEvent OnWeaponAimedEvent;
        public WeaponEvent OnWeaponReloadedEvent;

    #endregion Events

    public bool isAiming = false;

    bool _isReloading = false;

    float _fireTimeStamp = -10.0F;
    float _reloadTimeStamp = -10.0F;
    float _fireRate = 0.1F;

    public bool CanShoot
    {
        get { return Time.time >= _fireTimeStamp + _fireRate && !_isReloading && ammo > 0; }
    }

    public bool CanReload
    {
        get { return ammo < 32 && spareAmmo > 0; }
    }

    public float reloadTime = 1.25F;

    public void UpdateState ()
    {
        if (_isReloading && Time.time > _reloadTimeStamp + reloadTime)
        {
            Reload();
        }
    }

    // Is the weapon ready to shoot?
    public void TryFire ()
    {
        if (CanShoot)
        {
            Fire();
            OnWeaponFiredEvent();
            PlaySound(sound.fire);

            _fireTimeStamp = Time.time;
            ammo--;

            if (ammo == 0)
                TryReload();
        }
    }

    public void TryReload ()
    {
        if (CanReload)
        {
            OnWeaponReloadedEvent();
            PlaySound(sound.reload);

            _isReloading = true;
            _reloadTimeStamp = Time.time;
        }
    }

    // Called when TryFire "suceedes"
    public abstract void Fire ();

    public virtual void Reload ()
    {
        int temp = ammo;
        ammo += Mathf.Min((clipSize - ammo), spareAmmo);
        spareAmmo -= ammo - temp;

        _isReloading = false;

        Debug.Log("Weapon has reloaded");
    }

    public float currentFieldOfView = 60;

    public void Aim ()
    {
        Debug.Log("Weapon is aiming");
        isAiming = !isAiming;

        if (isAiming)
            currentFieldOfView = 60;
        else
            currentFieldOfView = 90;

        OnWeaponAimedEvent();
    }

    public void Dequipped ()
    {
        _isReloading = false;
        isAiming = false;

        OnWeaponAimedEvent();
    }

    public void Equipped ()
    {
        if (ammo == 0)
            TryReload();
    }

    protected RaycastHit CastRay ()
    {
        RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

		Physics.Raycast(ray, out hit, range);

        return hit;
    }

    protected void PlaySound (AudioClip[] sound)
    {
        GetComponent<AudioSource>().PlayOneShot(sound[Random.Range(0, sound.Length)]);
    }

    protected virtual string ParseAmmo ()
    {
        return ammo.ToString();
    }

    void OnGUI()
    {
        GUI.Box(new Rect(110, 10, 150, 30), "Ammo: " + ammo + " / " + spareAmmo);
    }
}




    /*public void fireShot()
    {
        if (!am.IsPlaying(reloadA.name) && ammo >= 1)
        {
            if (!am.IsPlaying(shoot.name))
            {
                am.CrossFade(shoot.name);
                ammo = ammo - 1;

                cam.transform.Rotate(Vector3.right, -recoilPower * Time.deltaTime);


            }

        }
    }*/