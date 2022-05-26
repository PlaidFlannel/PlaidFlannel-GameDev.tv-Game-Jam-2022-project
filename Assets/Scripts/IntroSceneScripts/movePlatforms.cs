using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlatforms : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float movePlatformDelay = 2.0f;
    private bool moveEnabled = false;
    [SerializeField] Vector3 targetPosition;
    private void Start()
    {
        Invoke("GoNow", movePlatformDelay);
    }
    void Update()
    {
        if (moveEnabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
    
    
    void GoNow()
    {
        moveEnabled = true;
    }

}
