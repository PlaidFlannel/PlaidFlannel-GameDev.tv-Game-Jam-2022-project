using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeaconSwitch : MonoBehaviour
{
    [SerializeField] BeaconCounter beaconCounter;
    [SerializeField] Material beaconLit;
    [SerializeField] Material beaconUnlit;
    //[SerializeField] GameObject beacon;
    [SerializeField] GameObject beaconLightSource;
    Renderer rend;
    //public static int index;
    int i = 0;
    //public bool lit = false;
    public void Start()
    {
        rend = GetComponent<Renderer>();
        rend.sharedMaterial = beaconUnlit;
        beaconLightSource.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        changeMat();
    }
    public void changeMat()
    {
        if (i == 0)
        {
            rend.sharedMaterial = beaconLit;
            i = 1;
            //lit = true;
            beaconLightSource.SetActive(true);
            beaconCounter.UpdateScore(1);

        }
        else if (i == 1)
        {
            rend.sharedMaterial = beaconUnlit;
            i = 0;
            //lit = false;
            beaconCounter.UpdateScore(-1);
        }
    }

}
