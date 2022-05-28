using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMovement : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        float targetX = target.position.x;
        float targetZ = target.position.z;
        transform.position = new Vector3(-targetX, 0.054f, targetZ);
        var euler = target.rotation.eulerAngles;   //get target's rotation
        var rot = Quaternion.Euler(0, -euler.y, 0); //transpose values
        transform.rotation = rot;                  //set my rotation
    }

}
