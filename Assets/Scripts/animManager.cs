﻿using UnityEngine;
using System.Collections;

public class animManager : MonoBehaviour {

    public Animation am;
    public AnimationClip walk;
    public AnimationClip idle;

    public bool isTP = false;

    public PhotonView graphicsPV;
    public Animation graphicsAM;
    public AnimationClip tpRun;
    public AnimationClip tpIdle;
    public AnimationClip tpShoot;
    public AnimationClip tpRunShoot;
    public AnimationClip tpReload;

    public Rigidbody rb;

    void Update()
    {

        if (rb.velocity.magnitude >= 0.1)
        {
            if (!isTP)
            {
                playAnim(walk.name);
            }
            else
            {
                graphicsPV.RPC("playAnimPV", PhotonTargets.All, tpRun.name);
            }
        }
        else
        {
            if (!graphicsAM.IsPlaying(tpReload.name))
            {
                if (!isTP)
                {
                    playAnim(idle.name);
                }
                else
                {
                    graphicsPV.RPC("playAnimPV", PhotonTargets.All, tpIdle.name);
                }
            }
        }
    }


    public void playAnim(string animName)
    {
        if (!isTP)
        {
            am.CrossFade(animName);
        }
    }

    public void stopAnim()
    {
        am.Stop();
    }

    public void reload()
    {
        graphicsPV.RPC("playAnimPV", PhotonTargets.All, tpReload.name);
    }

    [PunRPC]
    public void playAnimPV(string animName)
    {
        if (isTP)
        {
            graphicsAM.CrossFade(animName);
        }
    }
}