using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BeaconCounter : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    [SerializeField] GameObject[] beacons;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (score == beacons.Length)
        {
            LoadNextLevel();
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
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
