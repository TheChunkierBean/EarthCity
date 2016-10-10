using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Camera cam;
    public int range = 10000;
    public int ammo = 10;
    public int spareAmmo = 180;
    public int clipSize = 10;
    public float reloadTime = 1.25F;
    public float equipTime = 0.4F;

    public enum FireMode {Automatic, SemiAutomatic, Burst}
    public FireMode fireMode = FireMode.Automatic; 

    [System.Serializable]
    public struct Sounds
    {
        public AudioClip[] fire;
        public AudioClip[] melee;
        public AudioClip[] reload;
        public AudioClip[] equip;
    }
    public Sounds Sound;

    [System.Serializable]
    public struct HUDElements
    {
        public Sprite crosshair;
        public Sprite thumbnail;
        public Sprite ammo;
        public Sprite scope;
    }
    public HUDElements HUD;

    [System.Serializable]
    public struct Aiming
    {
        public bool canAim;
        [Range(0, 100)]
        public int[] aimStages;
        [HideInInspector]
        public int currentAimState;
    }
    public Aiming Scoping;

    #region Events

        public delegate void WeaponEvent ();
        public WeaponEvent OnWeaponFiredEvent;
        public WeaponEvent OnWeaponAimedEvent;
        public WeaponEvent OnWeaponReloadedEvent;

    #endregion Events

    public bool IsReloading
    {
        get 
        {
            return (Time.time < _reloadTimeStamp + reloadTime) && _reloadTimeStamp > _equipTimeStamp;
        }
        set 
        {
            if (value)  // if reloading is true, set the timestamp
                _reloadTimeStamp = Time.time;
        }
    }

    public bool CanReload
    {
        get { return ammo < clipSize && spareAmmo > 0 && !IsReloading; }
    }

    public bool IsEquipped 
    {
        get { return (Time.time > _equipTimeStamp + equipTime); }
    }

    public float ScopeToFOVRatio
    {
        get 
        { 
            if (!IsAiming)
                return 100;
            else
                return Scoping.aimStages[Scoping.currentAimState-1];
         }
    }

    public bool IsAiming 
    {
        get { return (Scoping.currentAimState > 0); }
        set 
        { 
            if (value)
            {
                Scoping.currentAimState++;

                if (Scoping.currentAimState > Scoping.aimStages.Length)
                    Scoping.currentAimState = 0;
            }
            else
                Scoping.currentAimState = 0;
        }
    }

    public bool CanFire
    {
        get { return _isTriggerReleased && Time.time >= _fireTimeStamp + _fireRate && !IsReloading && ammo > 0 && IsEquipped; }
    }

    float _fireTimeStamp = -10.0F;
    float _reloadTimeStamp = -10.0F;
    float _equipTimeStamp = -10.0F;
    float _fireRate = 0.1F;
    public bool _isTriggerReleased = false;

    public void UpdateState ()
    {
        /*if (Time.time > _reloadTimeStamp + reloadTime)
            FinishReload();*/

        ammo = 1000;
        // Temp
        if (CastRay().collider != null && CastRay().collider.GetComponent<Hitbox>())
            HUDRelay.OnHitboxHitChanged(true);
        else
            HUDRelay.OnHitboxHitChanged(false);
    }

    // Is the weapon ready to shoot?
    public void TryFire ()
    {
        if (CanFire)
        {
            Fire();                             // For different firing modes, supply a method that determines the firing mode? Like a FireAuto for autos, FireSemi for semi and FireBurst for burst? Will remove code redundance
            OnWeaponFiredEvent();
            PlaySound(Sound.fire);

            _fireTimeStamp = Time.time;
            ammo--;

            if (fireMode == FireMode.SemiAutomatic)
                _isTriggerReleased = false;

            if (ammo == 0)
                BeginReload();
        }
    }

    public void BeginReload ()
    {
        if (CanReload)
        {
            IsReloading = true;

            PlaySound(Sound.reload);
            OnWeaponReloadedEvent(); 
        }
    }

    // Called when TryFire "suceedes"
    public abstract void Fire ();

    public virtual void FinishReload ()
    {
        if (IsReloading)
        {
            int temp = ammo;
            ammo += Mathf.Min((clipSize - ammo), spareAmmo);
            spareAmmo -= ammo - temp;

            Debug.Log("Weapon has reloaded");
        }
    }

    public void Aim ()
    {
        if (!Scoping.canAim)
            return;

        IsAiming = true;
        
        OnWeaponAimedEvent();
    }

    public void Dequipped ()
    {
        IsAiming = false;

        OnWeaponAimedEvent();
    }

    public void Equipped ()
    {
        if (ammo == 0)
            BeginReload();

        _equipTimeStamp = Time.time;
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
        GUI.Box(new Rect(110, 40, 150, 30), "CanReload: " + CanReload);
        GUI.Box(new Rect(110, 70, 150, 30), "IsReloading: " + IsReloading);
        GUI.Box(new Rect(110, 100, 150, 30), "IsEquipped: " + IsEquipped);
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


        /*public float recoilPower = 30;

    public Animation am;
    public AnimationClip shoot;
    public AnimationClip reloadA;

    public animManager amM;*/