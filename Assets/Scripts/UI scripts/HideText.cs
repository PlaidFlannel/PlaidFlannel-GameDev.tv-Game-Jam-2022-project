using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideText : MonoBehaviour
{
    [SerializeField] float hideTextDelay = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GoNow", hideTextDelay);
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    void GoNow()
    {
        gameObject.SetActive(false);
    }
}
