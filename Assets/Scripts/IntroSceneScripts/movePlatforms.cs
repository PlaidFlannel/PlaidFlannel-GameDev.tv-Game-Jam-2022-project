using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlatforms : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float movePlatformDelay = 2.0f;
    [SerializeField] GameObject beaconRight;
    [SerializeField] GameObject beaconLeft;
    private bool moveEnabled = false;
    public Material beaconLit;
    public Material beaconUnlit;
    Renderer rend;
    Renderer rend2;
    [SerializeField] Vector3 targetPosition;
    private void Start()
    {
        Invoke("GoNow", movePlatformDelay);
        rend = beaconRight.GetComponent<Renderer>();
        rend.sharedMaterial = beaconUnlit;
        rend2 = beaconLeft.GetComponent<Renderer>();
        rend2.sharedMaterial = beaconUnlit;


    }
    void Update()
    {
        if (moveEnabled)
        {
            //transform.Translate(moveSpeed, 0, 0);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            rend.sharedMaterial = beaconLit;
            rend2.sharedMaterial = beaconLit;
        }
    }
    
    
    void GoNow()
    {
        moveEnabled = true;
    }

}
