using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BeaconCounter : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    [SerializeField] GameObject[] beacons;
    [SerializeField] GameObject largeBeacon;
    [SerializeField] float loadLevelDelay = 2.5f;
    //public GameObject ready;

    private int score;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0);
        lightBeacon lightBeaconScript = largeBeacon.GetComponent<lightBeacon>();
        lightBeaconScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (score == beacons.Length)
        {
            lightBeacon lightBeaconScript = largeBeacon.GetComponent<lightBeacon>();
            lightBeaconScript.enabled = true;
            Invoke("LoadNextLevel", loadLevelDelay);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        int beaconsCount = beacons.Length;
        score += scoreToAdd;
        Debug.Log(beaconsCount);
        scoreText.text = "Beacons lit: " + score + "/" + beaconsCount;
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        lightBeacon lightBeaconScript = largeBeacon.GetComponent<lightBeacon>();
        lightBeaconScript.enabled = true;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
