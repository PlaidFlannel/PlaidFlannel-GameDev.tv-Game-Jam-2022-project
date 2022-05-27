using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightBeacon : MonoBehaviour
{
    [SerializeField] GameObject beaconLight;
    [SerializeField] GameObject spotLight;
    [SerializeField] float lightBeaconDelay = 2.0f;
    public Material beaconLit;
    public Material beaconUnlit;
    Renderer rend;
    void Start()
    {
        rend = beaconLight.GetComponent<Renderer>();
        rend.sharedMaterial = beaconLit;
        Invoke("GoNow", lightBeaconDelay);
        spotLight.SetActive(true);

    }

    void GoNow()
    {
        rend.sharedMaterial = beaconUnlit;
        spotLight.SetActive(false);
    }
}
