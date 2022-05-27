using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeaconSwitch : MonoBehaviour
{
    [SerializeField] BeaconCounter beaconCounter;
    [SerializeField] Material beaconLit;
    [SerializeField] Material beaconUnlit;
    [SerializeField] GameObject beaconLightSource;
    Renderer rend;
    int i = 0;
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
            beaconLightSource.SetActive(true);
            beaconCounter.UpdateScore(1);

        }
        else if (i == 1)
        {
            rend.sharedMaterial = beaconUnlit;
            i = 0;
            beaconCounter.UpdateScore(-1);
        }
    }

}
