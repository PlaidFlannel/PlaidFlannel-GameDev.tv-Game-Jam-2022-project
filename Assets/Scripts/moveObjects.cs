using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveObjects : MonoBehaviour
{
    [SerializeField] Vector3 moveSpeed;
    
    [SerializeField] GameObject crashedIndictor;
    [SerializeField] float moveObjectDelay = 3.0f;

    [SerializeField] bool triggerNextLevel;
    private bool moveEnabled = false;
    private bool crashed = false;
    private void Start()
    {
        Invoke("GoNow", moveObjectDelay);
    }
    void Update()
    {
        if (moveEnabled)
        {
            if (!crashed)
            {
                transform.Translate(moveSpeed * Time.deltaTime);
            }
            if (transform.position.y < -10)
            {
                Destroy(gameObject);
                if (triggerNextLevel)
                {
                    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                    SceneManager.LoadScene(currentSceneIndex + 1);
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            crashed = true;
            if (triggerNextLevel) 
            { 
            StartCoroutine(CrashedCountdownRoutine());
            crashedIndictor.gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sensor1"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    IEnumerator CrashedCountdownRoutine()
    {
        yield return new WaitForSeconds(2);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    void GoNow()
    {
        moveEnabled = true;
    }
}
