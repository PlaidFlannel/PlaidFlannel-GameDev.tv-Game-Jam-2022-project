using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMovement : MonoBehaviour
{
    public Transform playerCar;
    public Transform enemyCar;

    public Transform MirrorPoint;

    private void Update()
    {
        enemyCar.position = Vector3.LerpUnclamped(playerCar.position, MirrorPoint.position, -2f);
    }
}
