using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//This is used in the last level
public class BeaconSwitchPlatformActivator : MonoBehaviour
{
    [SerializeField] Material beaconLit;
    [SerializeField] Material beaconUnlit;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject streetLight;
    [SerializeField] GameObject streetLightLight;
    [SerializeField] GameObject platform;
    private int score;
    Renderer rend;
    Renderer slRend;
    [SerializeField] int i = 0;

    void Start()
    {
        UpdateScore(3);
        rend = GetComponent<Renderer>();
        slRend = streetLight.GetComponent<Renderer>();
        platform.GetComponent<movePlatforms>().enabled = false;
        streetLightLight.SetActive(false);
        rend.sharedMaterial = beaconUnlit;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        changeMat();
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Street Lights Lit: " + score + "/4";
    }
    public void changeMat()
    {
        if (i == 0)
        {
            rend.sharedMaterial = beaconLit;
            slRend.sharedMaterial = beaconLit;
            streetLightLight.SetActive(true);
            i = 1;
            platform.GetComponent<movePlatforms>().enabled = true;
            UpdateScore(1);
        }
        else if (i == 1)
        {
            rend.sharedMaterial = beaconUnlit;
            i = 0;
            UpdateScore(-1);
        }
    }
}
