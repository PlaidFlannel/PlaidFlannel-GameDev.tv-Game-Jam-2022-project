using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FinalLevelBeaconCounter : MonoBehaviour
{
    // Detect when final beacon is lit and light the street light above.
    // Activate a platform to access the final goal.
    // Transition to next level will be with a sensor as usual.
    public TextMeshProUGUI scoreText;
    private float nextStageDelay;
    private int score;
    [SerializeField] GameObject[] beacons;
    void Update()
    {
        if (score == beacons.Length)
        {
            Invoke("RaisePlatform", nextStageDelay);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        int beaconsCount = beacons.Length;
        score += scoreToAdd;
        scoreText.text = "Beacons lit: " + score + "/" + beaconsCount;
    }
}
