using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlatformsSwitch : MonoBehaviour
{
    public bool switchActivated = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            switchActivated = true;
        }
        
    }
}
