using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{


    public Camera fpsCam;
    public GameObject hitPar;
    public int damage = 30;
    public int range = 10000;
    public int ammo = 10;
    public int clipSize = 10;
    public int clipCount = 5;
    public float recoilPower = 30;
    public Animation am;
    public AnimationClip shoot;
    public AnimationClip reloadA;

    public animManager amM;


    void Awake()
    {

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            fireShot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            reload();
        }
    }

    public void fireShot()
    {
        if (!am.IsPlaying(reloadA.name) && ammo >= 1)
        {
            if (!am.IsPlaying(shoot.name))
            {
                am.CrossFade(shoot.name);
                ammo = ammo - 1;

                fpsCam.transform.Rotate(Vector3.right, -recoilPower * Time.deltaTime);

                RaycastHit hit;
                Ray ray = fpsCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

                if (Physics.Raycast(ray, out hit, range))
                {
                    if (hit.transform.tag == "Player")
                    {
                        hit.transform.GetComponent<PhotonView>().RPC("applyDamage", PhotonTargets.AllBuffered, damage);
                    }
                    GameObject particleClone;
                    particleClone = PhotonNetwork.Instantiate(hitPar.name, hit.point, Quaternion.LookRotation(hit.normal), 0) as GameObject;
                    Destroy(particleClone, 2);
                    Debug.Log(hit.transform.name);

                }
            }

        }
    }

    public void reload()
    {
        if (clipCount >= 1)
        {
            am.CrossFade(reloadA.name);
            ammo = clipSize;
            clipCount = clipCount - 1;
            amM.reload();
        }

    }

    void OnGUI()
    {
        GUI.Box(new Rect(110, 10, 150, 30), "Ammo: " + ammo + "/" + clipSize + "/" + clipCount);
    }
}