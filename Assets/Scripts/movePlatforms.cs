using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlatforms : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float movePlatformDelay = 2.0f;

    [SerializeField] Vector3 targetPosition;
    [SerializeField] movePlatformsSwitch sensor;

    public bool moveEnabled = false;
    public bool switchEnabled = false;
    private void Start()
    {
        if (switchEnabled == false)
        {
            Invoke("GoNow", movePlatformDelay);
        }
    }
    void FixedUpdate()
    {
        if (moveEnabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        if (sensor.switchActivated)
        {
            Invoke("GoNow", movePlatformDelay);
        }
    }
    void GoNow()
    {
        moveEnabled = true;
    }
  
}
