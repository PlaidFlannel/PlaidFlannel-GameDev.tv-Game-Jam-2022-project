using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BeaconCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject[] beacons;
    [SerializeField] float loadLevelDelay = 2.5f;

    private int score;

    void Start()
    {
        UpdateScore(0);
    }

    void Update()
    {
        if (score == beacons.Length)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex != 8)
            {
                Invoke("LoadNextLevel", loadLevelDelay);
            }
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        int beaconsCount = beacons.Length;
        score += scoreToAdd;
        scoreText.text = "Beacons lit: " + score + "/" + beaconsCount;
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
