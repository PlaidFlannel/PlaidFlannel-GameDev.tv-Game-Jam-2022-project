using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpinner : MonoBehaviour
{
    private float spinSpeed = 0.75f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
        transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), spinSpeed, Space.Self);
        }
    }
}
