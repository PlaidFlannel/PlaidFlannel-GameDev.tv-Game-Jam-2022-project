using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlatforms : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float movePlatformDelay = 2.0f;
    public bool moveEnabled = false;
    public bool switchEnabled = false;
    //[SerializeField] movePlatformsSwitch movementSwitch;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] movePlatformsSwitch mPS;
    private void Start()
    {
        //script = movementSwitch.GetComponent<movePlatformsSwitch>();

        if (switchEnabled == false)
        {
            Invoke("GoNow", movePlatformDelay);
        }
        /*else if (mPS.switchActivated)
        {
            Invoke("GoNow", movePlatformDelay);
        }*/
    }
    void Update()
    {
        if (moveEnabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        if (mPS.switchActivated)
        {
            Invoke("GoNow", movePlatformDelay);
        }
    }
    void GoNow()
    {
        moveEnabled = true;
    }
  
}
