using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeaconSwitch : MonoBehaviour
{
    public BeaconCounter beaconCounter;
    public Material beaconLit;
    public Material beaconUnlit;
    Renderer rend;
    //public static int index;
    public int i = 0;
    public bool lit = false;
    public void Start()
    {
        rend = GetComponent<Renderer>();
        rend.sharedMaterial = beaconUnlit;
        //i = 0;
        //rend.enabled = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (i == 0)
        {
            rend.sharedMaterial = beaconLit;
            i = 1;
            lit = true;
            beaconCounter.UpdateScore(1);
        }
        else if (i == 1)
        {
            rend.sharedMaterial = beaconUnlit;
            i = 0;
            lit = false;
            beaconCounter.UpdateScore(-1);
        }
        //changeMat();
        //Debug.Log("hit");
        //Debug.Log(i);

    }
    public void changeMat()
    {
        if (i == 0)
        {
            rend.sharedMaterial = beaconLit;
            i = 1;
            lit = true;
            beaconCounter.UpdateScore(1);

        }
        else if (i == 1)
        {
            rend.sharedMaterial = beaconUnlit;
            i = 0;
            lit = false;
            beaconCounter.UpdateScore(-1);
        }
    }

}
